using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace Aster_Setup_Wizard
{
    public partial class Form1 : Form
    {
        private const string AsterRepositoryUrl = "https://github.com/7westl/Aster-Alpha-Version.git";
        private const string VscRepositoryUrl = "https://github.com/7westl/aster-vscode-src.git";

        private readonly string msys2Path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Programs", "MSYS2");

        private string installationPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Programs", "Aster");

        private bool addToPath = true;
        private bool installationCancelled;
        private bool installationRunning;
        private Process activeProcess;
        private ProgressBar installProgress;
        private Label installStatus;
        private Label installDetails;
        private RichTextBox logBox;

        // Selected components (captured on the UI thread, used by the background installer)
        private bool installSource = true;
        private bool installCompiler = true;
        private bool installPython = true;
        private bool installVsc;

        private int totalSteps;
        private int currentStep;

        private bool NeedGit => installSource || installVsc;          // needed to clone repositories
        private bool NeedMsys => installCompiler || installPython;    // needed for pacman packages
        private bool NeedBuild => installSource && installCompiler;   // building requires source + compiler
        private string VscPath => Path.Combine(installationPath, "vscode-extension");

        private readonly Color BackgroundColor = Color.FromArgb(248, 249, 251);
        private readonly Color TextColor = Color.FromArgb(35, 39, 47);
        private readonly Color SubTextColor = Color.FromArgb(110, 117, 128);
        private readonly Color BorderColor = Color.FromArgb(222, 226, 232);
        private readonly Color AccentColor = Color.FromArgb(45, 105, 220);

        private class ProcessResult
        {
            public int ExitCode;
            public string Output;
            public string Error;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SendMessageTimeout(
            IntPtr hWnd, uint Msg, UIntPtr wParam, string lParam,
            uint fuFlags, uint uTimeout, out UIntPtr lpdwResult);

        public Form1()
        {
            InitializeComponent();
            InitializeWindow();
            InitializeLogToggle();
            WireUiEvents();
            ShowWelcomePage();
        }

        private void InitializeWindow()
        {
            Text = "Aster Setup";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = BackgroundColor;
            Font = new Font("Segoe UI", 9F);
            FormClosing += (s, e) =>
            {
                if (!installationRunning) return;
                installationCancelled = true;
                try
                {
                    if (activeProcess != null && !activeProcess.HasExited)
                        activeProcess.Kill();
                }
                catch { }
            };
        }

        // ---------------------------------------------------------
        // Log expand / collapse
        // ---------------------------------------------------------

        private const bool LogExpandedByDefault = false;   // set to true to start expanded
        private Button logToggleButton;
        private bool logExpanded;

        private void InitializeLogToggle()
        {
            logToggleButton = new Button();
            logToggleButton.FlatStyle = FlatStyle.Flat;
            logToggleButton.FlatAppearance.BorderColor = BorderColor;
            logToggleButton.BackColor = Color.White;
            logToggleButton.ForeColor = TextColor;
            logToggleButton.Cursor = Cursors.Hand;
            logToggleButton.Location = new Point(45, 110);
            logToggleButton.Size = new Size(130, 26);
            logToggleButton.UseVisualStyleBackColor = false;
            logToggleButton.Click += (s, e) => SetLogExpanded(!logExpanded);
            installingPage.Controls.Add(logToggleButton);

            installingLogBox.Location = new Point(45, 144);
            installingLogBox.Size = new Size(670, 128);

            SetLogExpanded(LogExpandedByDefault);
        }

        private void SetLogExpanded(bool expanded)
        {
            logExpanded = expanded;
            installingLogBox.Visible = expanded;
            logToggleButton.Text = expanded ? "\u25BC Hide log" : "\u25B6 Show log";

            if (expanded)
            {
                installingLogBox.SelectionStart = installingLogBox.TextLength;
                installingLogBox.ScrollToCaret();
            }
        }

        // ---------------------------------------------------------
        // Log sanitizing (ANSI codes, progress bars, spinners)
        // ---------------------------------------------------------

        private static readonly Regex AnsiRegex =
            new Regex(@"\x1B\[[0-9;?]*[ -/]*[@-~]", RegexOptions.Compiled);

        private static readonly Regex ProgressOnlyRegex =
            new Regex(@"^[\d.,\s]*(KB|MB|GB)?\s*(/\s*[\d.,]+\s*(KB|MB|GB))?\s*(\d+\s*%)?$",
                      RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static string SanitizeLogLine(string line)
        {
            if (string.IsNullOrEmpty(line))
                return null;

            line = AnsiRegex.Replace(line, "");

            // Progress output redraws a line with \r; keep only the last update
            if (line.IndexOf('\r') >= 0)
            {
                string[] parts = line.Split('\r');
                line = "";
                for (int i = parts.Length - 1; i >= 0; i--)
                {
                    if (!string.IsNullOrWhiteSpace(parts[i]))
                    {
                        line = parts[i];
                        break;
                    }
                }
            }

            StringBuilder sb = new StringBuilder(line.Length);
            foreach (char c in line)
            {
                if (c == '\t') { sb.Append(' '); continue; }
                if (char.IsControl(c)) continue;                  // control characters
                if (c >= '\u2580' && c <= '\u259F') continue;      // winget progress bar blocks
                if (c == '\uFFFD') continue;                       // invalid characters
                sb.Append(c);
            }

            string result = sb.ToString().Trim();

            if (result.Length == 0) return null;
            if (result.Length == 1 && "-\\|/".IndexOf(result[0]) >= 0) return null;   // spinner
            if (ProgressOnlyRegex.IsMatch(result)) return null;                       // progress-only lines

            return result;
        }

        private void WireUiEvents()
        {
            welcomeNextButton.Click += (s, e) => ShowComponentsPage();
            componentsBackButton.Click += (s, e) => ShowWelcomePage();
            componentsNextButton.Click += (s, e) =>
            {
                installSource = sourceComponentCheckBox.Checked;
                installCompiler = compilerComponentCheckBox.Checked;
                installPython = pythonComponentCheckBox.Checked;
                installVsc = vscExtensionCheckBox.Checked;

                if (!installSource && !installCompiler && !installPython && !installVsc)
                {
                    MessageBox.Show("Please select at least one component to install.", "Aster Setup",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (installSource && !installCompiler)
                {
                    DialogResult r = MessageBox.Show(
                        "The compiler (GCC/Make) is not selected, so Aster will not be built.\n" +
                        "Only the source code will be downloaded. Continue?",
                        "Aster Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r != DialogResult.Yes) return;
                }

                ShowAdvancedOptions();
            };
            advancedBackButton.Click += (s, e) => ShowComponentsPage();
            advancedBrowseButton.Click += (s, e) => BrowseInstallationDirectory();
            advancedNextButton.Click += (s, e) =>
            {
                installationPath = pathTextBox.Text.Trim();
                addToPath = pathCheckBox.Checked;
                if (string.IsNullOrWhiteSpace(installationPath))
                {
                    MessageBox.Show("Please enter an installation directory.", "Aster Setup",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ShowInstallationPage();
            };
            readyBackButton.Click += (s, e) => ShowAdvancedOptions();
            readyInstallButton.Click += (s, e) => ShowInstallingPage();
            cancelInstallButton.Click += (s, e) => CancelInstallation();
            completedFinishButton.Click += (s, e) => Close();
            failedBackButton.Click += (s, e) => ShowAdvancedOptions();
            failedCloseButton.Click += (s, e) => Close();
            cancelledCloseButton.Click += (s, e) => Close();
        }

        private void ShowPage(Panel page, string title, string description,
            bool showBack, bool showNext, string nextText = "Next")
        {
            welcomePage.Visible = false;
            componentsPage.Visible = false;
            advancedPage.Visible = false;
            readyPage.Visible = false;
            installingPage.Visible = false;
            completedPage.Visible = false;
            failedPage.Visible = false;
            cancelledPage.Visible = false;
            page.Visible = true;
            page.BringToFront();
            pageTitleLabel.Text = title;
            pageDescriptionLabel.Text = description;
            footerPanel.Visible = true;
            welcomeNextButton.Visible = false;
            componentsBackButton.Visible = false;
            componentsNextButton.Visible = false;
            advancedBackButton.Visible = false;
            advancedNextButton.Visible = false;
            readyBackButton.Visible = false;
            readyInstallButton.Visible = false;
            completedFinishButton.Visible = false;
            failedBackButton.Visible = false;
            failedCloseButton.Visible = false;
            cancelledCloseButton.Visible = false;
            cancelInstallButton.Visible = false;
        }

        private void ShowWelcomePage()
        {
            ShowPage(welcomePage, "Install Aster Alpha 1.5.1 (x64)",
                "Install the Aster programming language on your computer.", false, true);
            welcomeNextButton.Visible = true;
            welcomeNextButton.BringToFront();
        }

        private void ShowComponentsPage()
        {
            ShowPage(componentsPage, "Optional Features", "Choose the installation components.", true, true);
            componentsBackButton.Visible = true;
            componentsNextButton.Visible = true;
        }

        private TextBox pathTextBox => advancedPathTextBox;
        private CheckBox pathCheckBox => advancedPathCheckBox;

        private void ShowAdvancedOptions()
        {
            ShowPage(advancedPage, "Advanced Options",
                "Configure the installation location and environment.", true, true);
            advancedPathTextBox.Text = installationPath;
            advancedPathCheckBox.Checked = addToPath;
            advancedBackButton.Visible = true;
            advancedNextButton.Visible = true;
        }

        private void BrowseInstallationDirectory()
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select the Aster installation directory.";
                dialog.SelectedPath = installationPath;
                if (dialog.ShowDialog() == DialogResult.OK)
                    advancedPathTextBox.Text = dialog.SelectedPath;
            }
        }

        private void ShowInstallationPage()
        {
            ShowPage(readyPage, "Ready to Install",
                "The installer is ready to prepare and build Aster.", true, true, "Install");
            readyPathLabel.Text = installationPath;

            StringBuilder ops = new StringBuilder();
            int n = 1;
            if (NeedGit)
                ops.AppendLine((n++) + ". Install Git if necessary");
            if (NeedMsys)
                ops.AppendLine((n++) + ". Install MSYS2" +
                    (installCompiler ? " + GCC/Make" : "") +
                    (installPython ? " + Python" : ""));
            if (installSource)
                ops.AppendLine((n++) + ". Download the Aster source repository");
            if (NeedBuild)
                ops.AppendLine((n++) + ". Compile Aster using Make");
            if (installVsc)
                ops.AppendLine((n++) + ". Download and install the VS Code extension");
            if (installSource)
                ops.AppendLine((n++) + ". Configure ASTER_HOME" +
                    (addToPath && NeedBuild ? " and PATH" : ""));

            readyOperationsLabel.Text = ops.ToString().TrimEnd();
            readyBackButton.Visible = true;
            readyInstallButton.Visible = true;
        }

        private void ShowInstallingPage()
        {
            ShowPage(installingPage, "Installing Aster",
                "Preparing the environment and compiling the source code.", false, false);
            installStatus = installingStatusLabel;
            installDetails = installingDetailsLabel;
            installProgress = installingProgressBar;
            logBox = installingLogBox;
            logBox.Clear();
            SetLogExpanded(LogExpandedByDefault);
            cancelInstallButton.Visible = true;
            cancelInstallButton.BringToFront();
            installationCancelled = false;
            installationRunning = true;
            _ = Task.Run(async () => await StartInstallation());
        }

        private void ShowCompletedPage()
        {
            installationRunning = false;
            ShowPage(completedPage, "Installation Complete",
                "The selected components have been installed.", false, true);
            completedExecutableLabel.Text = GetExecutablePath()
                ?? (installSource
                    ? "Not built (compiler not selected)"
                    : "Aster source not installed");
            completedFinishButton.Visible = true;
        }

        private void ShowInstallationFailedPage(string message)
        {
            installationRunning = false;
            ShowPage(failedPage, "Installation Failed",
                "A problem occurred during installation.", true, true);
            failedErrorTextBox.Text = message.Replace("\r\n", "\n").Replace("\n", "\r\n");
            failedBackButton.Visible = true;
            failedCloseButton.Visible = true;
        }

        private void ShowInstallationCancelledPage()
        {
            installationRunning = false;
            ShowPage(cancelledPage, "Installation Cancelled",
                "The installation process was cancelled.", false, true);
            cancelledCloseButton.Visible = true;
        }

        // ---------------------------------------------------------
        // Installation process
        // ---------------------------------------------------------

        private int CurrentProgress()
        {
            return totalSteps == 0 ? 0 : Math.Min(currentStep * 100 / totalSteps, 99);
        }

        private void Step(string status, string details)
        {
            SetStatus(CurrentProgress(), status, details);
            currentStep++;
        }

        private async Task StartInstallation()
        {
            bool destinationExisted = Directory.Exists(installationPath);

            // Total steps used for progress
            totalSteps = 0;
            currentStep = 0;
            if (NeedGit) totalSteps += 1;        // Git check
            if (NeedMsys) totalSteps += 3;       // check/install, update, packages
            if (installSource) totalSteps += 2;  // clone, environment
            if (NeedBuild) totalSteps += 2;      // build, verify
            if (installVsc) totalSteps += 2;     // clone, install

            try
            {
                if (string.IsNullOrWhiteSpace(installationPath))
                    throw new Exception("Installation path is empty.");

                // Check the path only when a component needs the folder (source/VSC)
                if (installSource || installVsc)
                {
                    if (Directory.Exists(installationPath) &&
                        Directory.GetFileSystemEntries(installationPath).Length > 0)
                    {
                        throw new Exception(
                            "The installation directory already exists and is not empty.\n\n" +
                            "Choose an empty directory or remove the existing contents.");
                    }

                    Directory.CreateDirectory(
                        Path.GetDirectoryName(Path.GetFullPath(installationPath)));
                }

                // 1. Git (needed to clone source or VS Code extension)
                string gitPath = null;
                if (NeedGit)
                {
                    Step("Checking Git", "Checking Git installation.");
                    gitPath = FindGitExecutable();

                    if (gitPath == null)
                    {
                        SetStatus(CurrentProgress(), "Installing Git",
                            "Git was not found. Installing Git for Windows.");
                        await InstallGit();
                        gitPath = FindGitExecutable();

                        if (gitPath == null)
                            throw new Exception("Git installation completed, but git.exe could not be found.");
                    }
                    AppendLog("Git: " + gitPath);
                }
                ThrowIfCancelled();

                // 2-4. MSYS2 (when compiler or Python is selected)
                if (NeedMsys)
                {
                    Step("Checking MSYS2", "Checking the MSYS2 UCRT64 environment.");

                    string bashPath = FindMsysBash();
                    if (bashPath == null)
                    {
                        SetStatus(CurrentProgress(), "Installing MSYS2",
                            "Installing MSYS2 into the user programs directory.");
                        await InstallMsys2();
                        bashPath = FindMsysBash();

                        if (bashPath == null)
                            throw new Exception("MSYS2 installation completed, but bash.exe could not be found.");
                    }
                    AppendLog("MSYS2: " + bashPath);

                    Step("Updating MSYS2", "Updating the package database and system packages.");

                    ProcessResult updateResult = await RunMsysCommand("pacman -Syu --noconfirm");
                    if (updateResult.ExitCode != 0)
                        AppendLog("First MSYS2 update returned a non-zero exit code. Retrying.");
                    ThrowIfCancelled();

                    // A second run handles updates that require a shell restart.
                    updateResult = await RunMsysCommand("pacman -Syu --noconfirm");
                    if (updateResult.ExitCode != 0)
                    {
                        throw new Exception("MSYS2 update failed.\n" +
                            updateResult.Error + "\n" + updateResult.Output);
                    }

                    // Add only the selected packages
                    StringBuilder packages = new StringBuilder();
                    if (installCompiler) packages.Append("mingw-w64-ucrt-x86_64-gcc make ");
                    if (installPython) packages.Append("python ");

                    string what = (installCompiler ? "GCC and Make" : "") +
                                  (installCompiler && installPython ? " and " : "") +
                                  (installPython ? "Python" : "");

                    Step("Installing build tools", "Installing " + what + ".");

                    ProcessResult packageResult = await RunMsysCommand(
                        "pacman -S --needed --noconfirm " + packages.ToString().Trim());

                    if (packageResult.ExitCode != 0)
                    {
                        throw new Exception("Failed to install build dependencies.\n" +
                            packageResult.Error + "\n" + packageResult.Output);
                    }
                }
                ThrowIfCancelled();

                // 5. Clone Aster source
                if (installSource)
                {
                    Step("Downloading Aster", "Cloning the official Aster repository.");

                    ProcessResult cloneResult = await RunProcessAsync(
                        gitPath,
                        "clone --progress --depth 1 " +
                        QuoteWindowsArgument(AsterRepositoryUrl) + " " +
                        QuoteWindowsArgument(installationPath),
                        null, false);

                    if (cloneResult.ExitCode != 0)
                    {
                        throw new Exception("Git clone failed.\n" +
                            cloneResult.Error + "\n" + cloneResult.Output);
                    }
                }
                ThrowIfCancelled();

                // 6-7. Build + verify (only when source and compiler are selected)
                if (NeedBuild)
                {
                    Step("Building Aster", "Running Make in the MSYS2 UCRT64 environment.");

                    string buildCommand = "cd " + ShellQuote(ToMsysPath(installationPath)) + " && make";
                    ProcessResult buildResult = await RunMsysCommand(buildCommand);

                    if (buildResult.ExitCode != 0)
                    {
                        throw new Exception("Aster build failed.\n\n" +
                            "Exit code: " + buildResult.ExitCode + "\n\n" +
                            buildResult.Error + "\n" + buildResult.Output);
                    }

                    Step("Verifying installation", "Checking the generated Aster executable.");

                    string executable = GetExecutablePath();
                    if (string.IsNullOrEmpty(executable) || !File.Exists(executable))
                    {
                        throw new Exception("Make completed, but aster.exe was not found.\n\n" +
                            "Expected location:\n" + Path.Combine(installationPath, "aster.exe"));
                    }
                    AppendLog("Executable found: " + executable);
                }
                ThrowIfCancelled();

                // 8. VS Code extension (clone + install)
                if (installVsc)
                {
                    Step("Downloading VS Code extension", "Cloning the Aster VS Code extension repository.");

                    ProcessResult vscResult = await RunProcessAsync(
                        gitPath,
                        "clone --progress --depth 1 " +
                        QuoteWindowsArgument(VscRepositoryUrl) + " " +
                        QuoteWindowsArgument(VscPath),
                        null, false);

                    if (vscResult.ExitCode != 0)
                    {
                        throw new Exception("VS Code extension clone failed.\n" +
                            vscResult.Error + "\n" + vscResult.Output);
                    }

                    AppendLog("VS Code extension source: " + VscPath);
                    ThrowIfCancelled();

                    Step("Installing VS Code extension", "Registering the Aster extension in VS Code.");
                    await InstallVscExtension();
                }
                ThrowIfCancelled();

                // 9. Environment variables (only when source is installed)
                if (installSource)
                {
                    Step("Configuring environment", "Registering ASTER_HOME and PATH.");
                    ConfigureEnvironmentVariables();
                }

                SetStatus(100, "Completed", "Selected components were successfully installed.");
                await Task.Delay(500);

                if (!IsDisposed && IsHandleCreated)
                    BeginInvoke(new Action(ShowCompletedPage));
            }
            catch (OperationCanceledException)
            {
                if (installSource || installVsc)
                    CleanupInstallationDirectory(destinationExisted);

                if (!IsDisposed && IsHandleCreated)
                    BeginInvoke(new Action(ShowInstallationCancelledPage));
            }
            catch (Exception ex)
            {
                AppendLog("INSTALLATION ERROR: " + ex);

                if (installSource || installVsc)
                    CleanupInstallationDirectory(destinationExisted);

                if (!IsDisposed && IsHandleCreated)
                    BeginInvoke(new Action(() => ShowInstallationFailedPage(ex.Message)));
            }
            finally
            {
                installationRunning = false;
            }
        }

        // ---------------------------------------------------------
        // VS Code extension installation
        // ---------------------------------------------------------

        private async Task InstallVscExtension()
        {
            // 1) If a .vsix exists, install it with the code CLI
            string[] vsixFiles = Directory.GetFiles(VscPath, "*.vsix", SearchOption.AllDirectories);
            string codeCli = FindVsCodeCli();

            if (vsixFiles.Length > 0 && codeCli != null)
            {
                ProcessResult r = await RunProcessAsync(
                    "cmd.exe",
                    "/c \"chcp 65001 >nul && \"" + codeCli + "\" --install-extension \"" + vsixFiles[0] + "\" --force\"",
                    null, false);

                if (r.ExitCode != 0)
                    throw new Exception("VS Code extension install failed.\n" + r.Error + "\n" + r.Output);

                AppendLog("Installed VSIX: " + vsixFiles[0]);
                return;
            }

            // 2) Otherwise copy the source into the extensions folder (needs package.json)
            string packageJson = Path.Combine(VscPath, "package.json");
            if (!File.Exists(packageJson))
                throw new Exception("No .vsix or package.json found in the VS Code extension repository.");

            string json = File.ReadAllText(packageJson);
            string publisher = ReadJsonString(json, "publisher") ?? "aster";
            string name = ReadJsonString(json, "name") ?? "aster-language";
            string version = ReadJsonString(json, "version") ?? "0.2.0";

            string extensionsDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".vscode", "extensions");
            string target = Path.Combine(extensionsDir, publisher + "." + name + "-" + version);

            Directory.CreateDirectory(extensionsDir);
            if (Directory.Exists(target)) Directory.Delete(target, true);

            CopyDirectory(VscPath, target);
            AppendLog("Copied extension to: " + target);
        }

        private string ReadJsonString(string json, string key)
        {
            Match m = Regex.Match(json, "\"" + Regex.Escape(key) + "\"\\s*:\\s*\"([^\"]+)\"");
            return m.Success ? m.Groups[1].Value : null;
        }

        private string FindVsCodeCli()
        {
            string[] candidates =
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Programs", "Microsoft VS Code", "bin", "code.cmd"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                    "Microsoft VS Code", "bin", "code.cmd")
            };

            foreach (string c in candidates)
            {
                if (File.Exists(c))
                    return c;
            }

            return FindExecutableInPath("code.cmd");
        }

        private void CopyDirectory(string source, string destination)
        {
            Directory.CreateDirectory(destination);

            foreach (string file in Directory.GetFiles(source))
                File.Copy(file, Path.Combine(destination, Path.GetFileName(file)), true);

            foreach (string dir in Directory.GetDirectories(source))
            {
                if (Path.GetFileName(dir) == ".git") continue;   // skip the .git folder
                CopyDirectory(dir, Path.Combine(destination, Path.GetFileName(dir)));
            }
        }

        // ---------------------------------------------------------
        // Git installation
        // ---------------------------------------------------------

        private async Task InstallGit()
        {
            AppendLog("Installing Git for Windows using WinGet.");

            ProcessResult result = await RunProcessAsync(
                "winget",
                "install --id Git.Git -e --source winget " +
                "--silent --accept-package-agreements " +
                "--accept-source-agreements",
                null,
                false
            );

            if (result.ExitCode != 0)
            {
                throw new Exception(
                    "Git installation failed.\n\n" +
                    "WinGet exit code: " + result.ExitCode + "\n\n" +
                    result.Error + "\n" +
                    result.Output
                );
            }
        }

        private string FindGitExecutable()
        {
            string[] candidates =
            {
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                    "Git", "cmd", "git.exe"),

                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Programs", "Git", "cmd", "git.exe"),

                @"C:\Program Files (x86)\Git\cmd\git.exe"
            };

            foreach (string candidate in candidates)
            {
                if (File.Exists(candidate))
                    return candidate;
            }

            return FindExecutableInPath("git.exe");
        }

        // ---------------------------------------------------------
        // MSYS2 installation
        // ---------------------------------------------------------

        private async Task InstallMsys2()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(msys2Path));

            AppendLog("Installing MSYS2.");

            ProcessResult result = await RunProcessAsync(
                "winget",
                "install --id MSYS2.MSYS2 -e --source winget " +
                "--silent --accept-package-agreements " +
                "--accept-source-agreements " +
                "--location " +
                QuoteWindowsArgument(msys2Path),
                null,
                false
            );

            if (result.ExitCode != 0)
            {
                throw new Exception(
                    "MSYS2 installation failed.\n\n" +
                    "WinGet exit code: " + result.ExitCode + "\n\n" +
                    result.Error + "\n" +
                    result.Output
                );
            }
        }

        private string FindMsysBash()
        {
            string[] candidates =
            {
                Path.Combine(msys2Path, "usr", "bin", "bash.exe"),
                @"C:\msys64\usr\bin\bash.exe"
            };

            foreach (string candidate in candidates)
            {
                if (File.Exists(candidate))
                    return candidate;
            }

            return null;
        }

        // ---------------------------------------------------------
        // MSYS2 commands and build
        // ---------------------------------------------------------

        private async Task<ProcessResult> RunMsysCommand(string command)
        {
            string bashPath = FindMsysBash();

            if (bashPath == null)
                throw new Exception("MSYS2 bash.exe was not found.");

            ThrowIfCancelled();

            string arguments = "-lc " + QuoteWindowsArgument(command);

            return await RunProcessAsync(bashPath, arguments, msys2Path, true);
        }

        private string ToMsysPath(string windowsPath)
        {
            string path = Path.GetFullPath(windowsPath).Replace('\\', '/');

            if (path.Length >= 2 && path[1] == ':')
            {
                path = "/" + char.ToLowerInvariant(path[0]) + path.Substring(2);
            }

            return path;
        }

        private string ShellQuote(string value)
        {
            return "'" + value.Replace("'", "'\"'\"'") + "'";
        }

        // ---------------------------------------------------------
        // Process execution
        // ---------------------------------------------------------

        private async Task<ProcessResult> RunProcessAsync(
            string fileName,
            string arguments,
            string workingDirectory,
            bool msysEnvironment)
        {
            ThrowIfCancelled();

            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.FileName = fileName;
            startInfo.Arguments = arguments;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.StandardOutputEncoding = new UTF8Encoding(false);
            startInfo.StandardErrorEncoding = new UTF8Encoding(false);

            // Force English messages from git, pacman and make
            startInfo.EnvironmentVariables["LANGUAGE"] = "en";
            startInfo.EnvironmentVariables["LC_MESSAGES"] = "C";

            if (!string.IsNullOrEmpty(workingDirectory) &&
                Directory.Exists(workingDirectory))
            {
                startInfo.WorkingDirectory = workingDirectory;
            }

            if (msysEnvironment)
            {
                startInfo.EnvironmentVariables["MSYSTEM"] = "UCRT64";
                startInfo.EnvironmentVariables["CHERE_INVOKING"] = "yes";
                startInfo.EnvironmentVariables["LANG"] = "en_US.UTF-8";
            }

            StringBuilder output = new StringBuilder();
            StringBuilder error = new StringBuilder();

            object outputLock = new object();

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;

                process.OutputDataReceived += (s, e) =>
                {
                    string line = SanitizeLogLine(e.Data);
                    if (line == null)
                        return;

                    lock (outputLock)
                    {
                        output.AppendLine(line);
                    }

                    AppendLog(line);
                };

                process.ErrorDataReceived += (s, e) =>
                {
                    string line = SanitizeLogLine(e.Data);
                    if (line == null)
                        return;

                    lock (outputLock)
                    {
                        error.AppendLine(line);
                    }

                    AppendLog(line);
                };

                try
                {
                    AppendLog("> " + fileName + " " + arguments);

                    if (!process.Start())
                    {
                        throw new Exception("Failed to start process: " + fileName);
                    }

                    activeProcess = process;

                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    await Task.Run(() => process.WaitForExit());

                    process.WaitForExit();

                    if (installationCancelled)
                    {
                        throw new OperationCanceledException();
                    }

                    ProcessResult result = new ProcessResult();

                    result.ExitCode = process.ExitCode;

                    lock (outputLock)
                    {
                        result.Output = output.ToString();
                        result.Error = error.ToString();
                    }

                    AppendLog("Process finished. Exit code: " + result.ExitCode);

                    return result;
                }
                catch (System.ComponentModel.Win32Exception ex)
                {
                    throw new Exception(
                        "Could not start required program: " +
                        fileName + "\n\n" + ex.Message
                    );
                }
                finally
                {
                    activeProcess = null;
                }
            }
        }

        private void ThrowIfCancelled()
        {
            if (installationCancelled)
                throw new OperationCanceledException();
        }

        private string QuoteWindowsArgument(string argument)
        {
            return "\"" +
                argument.Replace("\\", "\\\\")
                        .Replace("\"", "\\\"") +
                "\"";
        }

        private string FindExecutableInPath(string executable)
        {
            string path = Environment.GetEnvironmentVariable("PATH");

            if (string.IsNullOrEmpty(path))
                return null;

            foreach (string directory in path.Split(';'))
            {
                try
                {
                    string candidate = Path.Combine(
                        directory.Trim().Trim('"'),
                        executable
                    );

                    if (File.Exists(candidate))
                        return candidate;
                }
                catch
                {
                }
            }

            return null;
        }

        // ---------------------------------------------------------
        // Installation verification
        // ---------------------------------------------------------

        private string GetExecutablePath()
        {
            string[] candidates =
            {
                Path.Combine(installationPath, "aster.exe"),
                Path.Combine(installationPath, "Programs", "aster.exe")
            };

            foreach (string candidate in candidates)
            {
                if (File.Exists(candidate))
                    return candidate;
            }

            return null;
        }

        private void ConfigureEnvironmentVariables()
        {
            string executablePath = GetExecutablePath();
            bool canAddPath = addToPath && !string.IsNullOrEmpty(executablePath);

            if (addToPath && !canAddPath)
                AppendLog("aster.exe not found (not built). Skipping PATH registration.");

            string executableDirectory = canAddPath
                ? Path.GetDirectoryName(executablePath)
                : null;

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Environment", true))
            {
                if (key == null)
                {
                    throw new Exception(
                        "Could not access the current user's environment registry."
                    );
                }

                key.SetValue("ASTER_HOME", installationPath, RegistryValueKind.String);

                if (canAddPath)
                {
                    string currentPath =
                        key.GetValue(
                            "Path",
                            "",
                            RegistryValueOptions.DoNotExpandEnvironmentNames
                        ) as string ?? "";

                    bool exists = false;

                    foreach (string item in currentPath.Split(';'))
                    {
                        if (string.Equals(
                            item.Trim().Trim('"'),
                            executableDirectory,
                            StringComparison.OrdinalIgnoreCase))
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        string newPath = string.IsNullOrWhiteSpace(currentPath)
                            ? executableDirectory
                            : currentPath.TrimEnd(';') + ";" + executableDirectory;

                        RegistryValueKind kind = RegistryValueKind.ExpandString;

                        try
                        {
                            if (key.GetValue("Path") != null)
                                kind = key.GetValueKind("Path");
                        }
                        catch
                        {
                        }

                        key.SetValue("Path", newPath, kind);
                    }
                }
            }

            Environment.SetEnvironmentVariable(
                "ASTER_HOME",
                installationPath,
                EnvironmentVariableTarget.Process
            );

            if (canAddPath)
            {
                string currentPath = Environment.GetEnvironmentVariable("PATH") ?? "";

                Environment.SetEnvironmentVariable(
                    "PATH",
                    currentPath + ";" + executableDirectory,
                    EnvironmentVariableTarget.Process
                );
            }

            // Notify other applications that user environment variables changed.
            UIntPtr result;

            SendMessageTimeout(
                new IntPtr(0xffff),
                0x001A,
                UIntPtr.Zero,
                "Environment",
                0x0002,
                5000,
                out result
            );

            AppendLog("ASTER_HOME configured.");
            AppendLog("Environment variables updated.");
        }

        // ---------------------------------------------------------
        // Cleanup and cancellation
        // ---------------------------------------------------------

        private void CleanupInstallationDirectory(bool destinationExisted)
        {
            if (destinationExisted)
                return;

            try
            {
                if (Directory.Exists(installationPath))
                {
                    Directory.Delete(installationPath, true);
                    AppendLog("Removed incomplete installation directory.");
                }
            }
            catch (Exception ex)
            {
                AppendLog("Cleanup warning: " + ex.Message);
            }
        }

        private void CancelInstallation()
        {
            if (!installationRunning)
                return;

            DialogResult result = MessageBox.Show(
                "Are you sure you want to cancel the installation?",
                "Cancel installation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;

            installationCancelled = true;

            try
            {
                if (activeProcess != null && !activeProcess.HasExited)
                {
                    activeProcess.Kill();
                }
            }
            catch
            {
            }

            SetStatus(
                installProgress == null ? 0 : installProgress.Value,
                "Cancelling",
                "Stopping the current operation."
            );
        }

        // ---------------------------------------------------------
        // UI updates and logging
        // ---------------------------------------------------------

        private void SetStatus(int progress, string status, string details)
        {
            if (IsDisposed || !IsHandleCreated)
                return;

            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => SetStatus(progress, status, details)));
                return;
            }

            if (installProgress != null)
            {
                installProgress.Value =
                    Math.Max(
                        installProgress.Minimum,
                        Math.Min(installProgress.Maximum, progress)
                    );
            }

            if (installStatus != null)
                installStatus.Text = status;

            if (installDetails != null)
                installDetails.Text = details;
        }

        private void AppendLog(string message)
        {
            if (IsDisposed || !IsHandleCreated)
                return;

            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(new Action(() => AppendLog(message)));
                }
                catch
                {
                }

                return;
            }

            if (logBox == null || logBox.IsDisposed)
                return;

            logBox.AppendText(
                "[" + DateTime.Now.ToString("HH:mm:ss") + "] " +
                message + Environment.NewLine
            );

            logBox.SelectionStart = logBox.TextLength;
            logBox.ScrollToCaret();
        }
    }
}