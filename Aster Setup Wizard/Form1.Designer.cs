namespace Aster_Setup_Wizard
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            brandLabel = new Label();
            pageTitleLabel = new Label();
            pageDescriptionLabel = new Label();
            headerLine = new Panel();
            welcomePage = new Panel();
            welcomeAsterLabel = new Label();
            welcomeTaglineLabel = new Label();
            welcomeDescriptionLabel = new Label();
            componentsPage = new Panel();
            sourceComponentCheckBox = new CheckBox();
            compilerComponentCheckBox = new CheckBox();
            pythonComponentCheckBox = new CheckBox();
            vscExtensionCheckBox = new CheckBox();
            advancedPage = new Panel();
            advancedPathCaptionLabel = new Label();
            advancedPathTextBox = new TextBox();
            advancedBrowseButton = new Button();
            advancedPathCheckBox = new CheckBox();
            advancedNoteLabel = new Label();
            readyPage = new Panel();
            readyPathCaptionLabel = new Label();
            readyPathLabel = new Label();
            readyOperationsCaptionLabel = new Label();
            readyOperationsLabel = new Label();
            installingPage = new Panel();
            installingStatusLabel = new Label();
            installingDetailsLabel = new Label();
            installingProgressBar = new ProgressBar();
            installingLogBox = new RichTextBox();
            completedPage = new Panel();
            completedMessageLabel = new Label();
            completedExecutableCaptionLabel = new Label();
            completedExecutableLabel = new Label();
            completedHintLabel = new Label();
            failedPage = new Panel();
            failedMessageLabel = new Label();
            failedErrorTextBox = new TextBox();
            cancelledPage = new Panel();
            cancelledMessageLabel = new Label();
            footerPanel = new Panel();
            welcomeNextButton = new Button();
            componentsNextButton = new Button();
            advancedNextButton = new Button();
            readyInstallButton = new Button();
            cancelInstallButton = new Button();
            completedFinishButton = new Button();
            failedCloseButton = new Button();
            cancelledCloseButton = new Button();
            failedBackButton = new Button();
            readyBackButton = new Button();
            advancedBackButton = new Button();
            componentsBackButton = new Button();
            welcomePage.SuspendLayout();
            componentsPage.SuspendLayout();
            advancedPage.SuspendLayout();
            readyPage.SuspendLayout();
            installingPage.SuspendLayout();
            completedPage.SuspendLayout();
            failedPage.SuspendLayout();
            cancelledPage.SuspendLayout();
            footerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // brandLabel
            // 
            brandLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            brandLabel.ForeColor = Color.FromArgb(45, 105, 220);
            brandLabel.Location = new Point(35, 25);
            brandLabel.Name = "brandLabel";
            brandLabel.Size = new Size(300, 25);
            brandLabel.TabIndex = 0;
            brandLabel.Text = "ASTER SETUP";
            // 
            // pageTitleLabel
            // 
            pageTitleLabel.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            pageTitleLabel.ForeColor = Color.FromArgb(35, 39, 47);
            pageTitleLabel.Location = new Point(35, 75);
            pageTitleLabel.Name = "pageTitleLabel";
            pageTitleLabel.Size = new Size(680, 40);
            pageTitleLabel.TabIndex = 1;
            // 
            // pageDescriptionLabel
            // 
            pageDescriptionLabel.Font = new Font("Segoe UI", 9F);
            pageDescriptionLabel.ForeColor = Color.FromArgb(110, 117, 128);
            pageDescriptionLabel.Location = new Point(35, 120);
            pageDescriptionLabel.Name = "pageDescriptionLabel";
            pageDescriptionLabel.Size = new Size(680, 45);
            pageDescriptionLabel.TabIndex = 2;
            // 
            // headerLine
            // 
            headerLine.BackColor = Color.FromArgb(222, 226, 232);
            headerLine.Location = new Point(35, 170);
            headerLine.Name = "headerLine";
            headerLine.Size = new Size(690, 1);
            headerLine.TabIndex = 3;
            // 
            // welcomePage
            // 
            welcomePage.BackColor = Color.FromArgb(248, 249, 251);
            welcomePage.Controls.Add(welcomeAsterLabel);
            welcomePage.Controls.Add(welcomeTaglineLabel);
            welcomePage.Controls.Add(welcomeDescriptionLabel);
            welcomePage.Location = new Point(0, 180);
            welcomePage.Name = "welcomePage";
            welcomePage.Size = new Size(760, 285);
            welcomePage.TabIndex = 4;
            // 
            // welcomeAsterLabel
            // 
            welcomeAsterLabel.BackColor = Color.Transparent;
            welcomeAsterLabel.Font = new Font("Segoe UI", 31F, FontStyle.Bold);
            welcomeAsterLabel.ForeColor = Color.FromArgb(45, 105, 220);
            welcomeAsterLabel.Location = new Point(65, 40);
            welcomeAsterLabel.Name = "welcomeAsterLabel";
            welcomeAsterLabel.Size = new Size(500, 55);
            welcomeAsterLabel.TabIndex = 0;
            welcomeAsterLabel.Text = "Aster";
            // 
            // welcomeTaglineLabel
            // 
            welcomeTaglineLabel.BackColor = Color.Transparent;
            welcomeTaglineLabel.Font = new Font("Segoe UI", 11F);
            welcomeTaglineLabel.ForeColor = Color.FromArgb(110, 117, 128);
            welcomeTaglineLabel.Location = new Point(67, 95);
            welcomeTaglineLabel.Name = "welcomeTaglineLabel";
            welcomeTaglineLabel.Size = new Size(600, 30);
            welcomeTaglineLabel.TabIndex = 1;
            welcomeTaglineLabel.Text = "A modern general-purpose programming language.";
            // 
            // welcomeDescriptionLabel
            // 
            welcomeDescriptionLabel.BackColor = Color.Transparent;
            welcomeDescriptionLabel.Font = new Font("Segoe UI", 10F);
            welcomeDescriptionLabel.ForeColor = Color.FromArgb(35, 39, 47);
            welcomeDescriptionLabel.Location = new Point(67, 150);
            welcomeDescriptionLabel.Name = "welcomeDescriptionLabel";
            welcomeDescriptionLabel.Size = new Size(610, 55);
            welcomeDescriptionLabel.TabIndex = 2;
            welcomeDescriptionLabel.Text = "The installer will automatically prepare the required tools, download the source code and compile Aster.";
            // 
            // componentsPage
            // 
            componentsPage.BackColor = Color.FromArgb(248, 249, 251);
            componentsPage.Controls.Add(sourceComponentCheckBox);
            componentsPage.Controls.Add(compilerComponentCheckBox);
            componentsPage.Controls.Add(pythonComponentCheckBox);
            componentsPage.Controls.Add(vscExtensionCheckBox);
            componentsPage.Location = new Point(0, 180);
            componentsPage.Name = "componentsPage";
            componentsPage.Size = new Size(760, 285);
            componentsPage.TabIndex = 5;
            componentsPage.Visible = false;
            // 
            // sourceComponentCheckBox
            // 
            sourceComponentCheckBox.Checked = true;
            sourceComponentCheckBox.CheckState = CheckState.Checked;
            sourceComponentCheckBox.Enabled = true;
            sourceComponentCheckBox.Location = new Point(65, 30);
            sourceComponentCheckBox.Name = "sourceComponentCheckBox";
            sourceComponentCheckBox.Size = new Size(600, 30);
            sourceComponentCheckBox.TabIndex = 0;
            sourceComponentCheckBox.Text = "Aster source code";
            // 
            // compilerComponentCheckBox
            // 
            compilerComponentCheckBox.Checked = true;
            compilerComponentCheckBox.CheckState = CheckState.Checked;
            compilerComponentCheckBox.Enabled = true;
            compilerComponentCheckBox.Location = new Point(65, 70);
            compilerComponentCheckBox.Name = "compilerComponentCheckBox";
            compilerComponentCheckBox.Size = new Size(600, 30);
            compilerComponentCheckBox.TabIndex = 1;
            compilerComponentCheckBox.Text = "GCC compiler and Make build system";
            // 
            // pythonComponentCheckBox
            // 
            pythonComponentCheckBox.Checked = true;
            pythonComponentCheckBox.CheckState = CheckState.Checked;
            pythonComponentCheckBox.Enabled = true;
            pythonComponentCheckBox.Location = new Point(65, 110);
            pythonComponentCheckBox.Name = "pythonComponentCheckBox";
            pythonComponentCheckBox.Size = new Size(600, 30);
            pythonComponentCheckBox.TabIndex = 2;
            pythonComponentCheckBox.Text = "Python build support";
            //
            // vscExtensionCheckBox
            //
            vscExtensionCheckBox.Checked = true;
            vscExtensionCheckBox.CheckState = CheckState.Unchecked;
            vscExtensionCheckBox.Enabled = true;
            vscExtensionCheckBox.Location = new Point(65, 150);
            vscExtensionCheckBox.Name = "vscExtensionCheckBox";
            vscExtensionCheckBox.Size = new Size(600, 30);
            vscExtensionCheckBox.TabIndex = 3;
            vscExtensionCheckBox.Text = "Vsc Extension for Aster";
            //
            // advancedPage
            // 
            advancedPage.BackColor = Color.FromArgb(248, 249, 251);
            advancedPage.Controls.Add(advancedPathCaptionLabel);
            advancedPage.Controls.Add(advancedPathTextBox);
            advancedPage.Controls.Add(advancedBrowseButton);
            advancedPage.Controls.Add(advancedPathCheckBox);
            advancedPage.Controls.Add(advancedNoteLabel);
            advancedPage.Location = new Point(0, 180);
            advancedPage.Name = "advancedPage";
            advancedPage.Size = new Size(760, 285);
            advancedPage.TabIndex = 6;
            advancedPage.Visible = false;
            // 
            // advancedPathCaptionLabel
            // 
            advancedPathCaptionLabel.BackColor = Color.Transparent;
            advancedPathCaptionLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            advancedPathCaptionLabel.ForeColor = Color.FromArgb(35, 39, 47);
            advancedPathCaptionLabel.Location = new Point(65, 30);
            advancedPathCaptionLabel.Name = "advancedPathCaptionLabel";
            advancedPathCaptionLabel.Size = new Size(300, 25);
            advancedPathCaptionLabel.TabIndex = 0;
            advancedPathCaptionLabel.Text = "Installation directory";
            // 
            // advancedPathTextBox
            // 
            advancedPathTextBox.Location = new Point(65, 60);
            advancedPathTextBox.Name = "advancedPathTextBox";
            advancedPathTextBox.Size = new Size(500, 23);
            advancedPathTextBox.TabIndex = 1;
            // 
            // advancedBrowseButton
            // 
            advancedBrowseButton.BackColor = Color.White;
            advancedBrowseButton.Cursor = Cursors.Hand;
            advancedBrowseButton.FlatAppearance.BorderColor = Color.FromArgb(222, 226, 232);
            advancedBrowseButton.FlatStyle = FlatStyle.Flat;
            advancedBrowseButton.ForeColor = Color.FromArgb(35, 39, 47);
            advancedBrowseButton.Location = new Point(575, 58);
            advancedBrowseButton.Name = "advancedBrowseButton";
            advancedBrowseButton.Size = new Size(100, 30);
            advancedBrowseButton.TabIndex = 2;
            advancedBrowseButton.Text = "Browse";
            advancedBrowseButton.UseVisualStyleBackColor = false;
            // 
            // advancedPathCheckBox
            // 
            advancedPathCheckBox.Checked = true;
            advancedPathCheckBox.CheckState = CheckState.Checked;
            advancedPathCheckBox.Location = new Point(65, 115);
            advancedPathCheckBox.Name = "advancedPathCheckBox";
            advancedPathCheckBox.Size = new Size(500, 30);
            advancedPathCheckBox.TabIndex = 3;
            advancedPathCheckBox.Text = "Add Aster to PATH environment variable";
            // 
            // advancedNoteLabel
            // 
            advancedNoteLabel.BackColor = Color.Transparent;
            advancedNoteLabel.Font = new Font("Segoe UI", 9F);
            advancedNoteLabel.ForeColor = Color.FromArgb(110, 117, 128);
            advancedNoteLabel.Location = new Point(65, 165);
            advancedNoteLabel.Name = "advancedNoteLabel";
            advancedNoteLabel.Size = new Size(630, 50);
            advancedNoteLabel.TabIndex = 4;
            advancedNoteLabel.Text = "Git, MSYS2 and the required compiler tools will be installed automatically if they are not already available.";
            // 
            // readyPage
            // 
            readyPage.BackColor = Color.FromArgb(248, 249, 251);
            readyPage.Controls.Add(readyPathCaptionLabel);
            readyPage.Controls.Add(readyPathLabel);
            readyPage.Controls.Add(readyOperationsCaptionLabel);
            readyPage.Controls.Add(readyOperationsLabel);
            readyPage.Location = new Point(0, 180);
            readyPage.Name = "readyPage";
            readyPage.Size = new Size(760, 285);
            readyPage.TabIndex = 7;
            readyPage.Visible = false;
            // 
            // readyPathCaptionLabel
            // 
            readyPathCaptionLabel.BackColor = Color.Transparent;
            readyPathCaptionLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            readyPathCaptionLabel.ForeColor = Color.FromArgb(35, 39, 47);
            readyPathCaptionLabel.Location = new Point(65, 30);
            readyPathCaptionLabel.Name = "readyPathCaptionLabel";
            readyPathCaptionLabel.Size = new Size(250, 25);
            readyPathCaptionLabel.TabIndex = 0;
            readyPathCaptionLabel.Text = "Installation directory";
            // 
            // readyPathLabel
            // 
            readyPathLabel.BackColor = Color.Transparent;
            readyPathLabel.Font = new Font("Segoe UI", 9F);
            readyPathLabel.ForeColor = Color.FromArgb(110, 117, 128);
            readyPathLabel.Location = new Point(65, 60);
            readyPathLabel.Name = "readyPathLabel";
            readyPathLabel.Size = new Size(620, 30);
            readyPathLabel.TabIndex = 1;
            // 
            // readyOperationsCaptionLabel
            // 
            readyOperationsCaptionLabel.BackColor = Color.Transparent;
            readyOperationsCaptionLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            readyOperationsCaptionLabel.ForeColor = Color.FromArgb(35, 39, 47);
            readyOperationsCaptionLabel.Location = new Point(65, 110);
            readyOperationsCaptionLabel.Name = "readyOperationsCaptionLabel";
            readyOperationsCaptionLabel.Size = new Size(600, 25);
            readyOperationsCaptionLabel.TabIndex = 2;
            readyOperationsCaptionLabel.Text = "The following operations will be performed:";
            // 
            // readyOperationsLabel
            // 
            readyOperationsLabel.BackColor = Color.Transparent;
            readyOperationsLabel.Font = new Font("Segoe UI", 9F);
            readyOperationsLabel.ForeColor = Color.FromArgb(35, 39, 47);
            readyOperationsLabel.Location = new Point(75, 145);
            readyOperationsLabel.Name = "readyOperationsLabel";
            readyOperationsLabel.Size = new Size(600, 110);
            readyOperationsLabel.TabIndex = 3;
            readyOperationsLabel.Text = "1. Install Git if necessary\n2. Install MSYS2 and build dependencies\n3. Download the Aster source repository\n4. Compile Aster using Make\n5. Verify the executable and configure PATH";
            // 
            // installingPage
            // 
            installingPage.BackColor = Color.FromArgb(248, 249, 251);
            installingPage.Controls.Add(installingStatusLabel);
            installingPage.Controls.Add(installingDetailsLabel);
            installingPage.Controls.Add(installingProgressBar);
            installingPage.Controls.Add(installingLogBox);
            installingPage.Location = new Point(0, 180);
            installingPage.Name = "installingPage";
            installingPage.Size = new Size(760, 285);
            installingPage.TabIndex = 8;
            installingPage.Visible = false;
            // 
            // installingStatusLabel
            // 
            installingStatusLabel.BackColor = Color.Transparent;
            installingStatusLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            installingStatusLabel.ForeColor = Color.FromArgb(35, 39, 47);
            installingStatusLabel.Location = new Point(45, 10);
            installingStatusLabel.Name = "installingStatusLabel";
            installingStatusLabel.Size = new Size(670, 30);
            installingStatusLabel.TabIndex = 0;
            installingStatusLabel.Text = "Preparing installation...";
            // 
            // installingDetailsLabel
            // 
            installingDetailsLabel.BackColor = Color.Transparent;
            installingDetailsLabel.Font = new Font("Segoe UI", 9F);
            installingDetailsLabel.ForeColor = Color.FromArgb(110, 117, 128);
            installingDetailsLabel.Location = new Point(45, 40);
            installingDetailsLabel.Name = "installingDetailsLabel";
            installingDetailsLabel.Size = new Size(670, 25);
            installingDetailsLabel.TabIndex = 1;
            installingDetailsLabel.Text = "Please wait.";
            // 
            // installingProgressBar
            // 
            installingProgressBar.Location = new Point(45, 80);
            installingProgressBar.Name = "installingProgressBar";
            installingProgressBar.Size = new Size(670, 22);
            installingProgressBar.Style = ProgressBarStyle.Continuous;
            installingProgressBar.TabIndex = 2;
            // 
            // installingLogBox
            // 
            installingLogBox.BackColor = Color.FromArgb(30, 33, 40);
            installingLogBox.BorderStyle = BorderStyle.None;
            installingLogBox.Font = new Font("Consolas", 8.5F);
            installingLogBox.ForeColor = Color.FromArgb(225, 230, 240);
            installingLogBox.Location = new Point(45, 120);
            installingLogBox.Name = "installingLogBox";
            installingLogBox.ReadOnly = true;
            installingLogBox.Size = new Size(670, 150);
            installingLogBox.TabIndex = 3;
            installingLogBox.Text = "";
            // 
            // completedPage
            // 
            completedPage.BackColor = Color.FromArgb(248, 249, 251);
            completedPage.Controls.Add(completedMessageLabel);
            completedPage.Controls.Add(completedExecutableCaptionLabel);
            completedPage.Controls.Add(completedExecutableLabel);
            completedPage.Controls.Add(completedHintLabel);
            completedPage.Location = new Point(0, 180);
            completedPage.Name = "completedPage";
            completedPage.Size = new Size(760, 285);
            completedPage.TabIndex = 9;
            completedPage.Visible = false;
            // 
            // completedMessageLabel
            // 
            completedMessageLabel.BackColor = Color.Transparent;
            completedMessageLabel.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            completedMessageLabel.ForeColor = Color.FromArgb(45, 105, 220);
            completedMessageLabel.Location = new Point(65, 45);
            completedMessageLabel.Name = "completedMessageLabel";
            completedMessageLabel.Size = new Size(600, 35);
            completedMessageLabel.TabIndex = 0;
            completedMessageLabel.Text = "Aster was installed successfully.";
            // 
            // completedExecutableCaptionLabel
            // 
            completedExecutableCaptionLabel.BackColor = Color.Transparent;
            completedExecutableCaptionLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            completedExecutableCaptionLabel.ForeColor = Color.FromArgb(35, 39, 47);
            completedExecutableCaptionLabel.Location = new Point(65, 105);
            completedExecutableCaptionLabel.Name = "completedExecutableCaptionLabel";
            completedExecutableCaptionLabel.Size = new Size(200, 25);
            completedExecutableCaptionLabel.TabIndex = 1;
            completedExecutableCaptionLabel.Text = "Executable:";
            // 
            // completedExecutableLabel
            // 
            completedExecutableLabel.BackColor = Color.Transparent;
            completedExecutableLabel.Font = new Font("Segoe UI", 9F);
            completedExecutableLabel.ForeColor = Color.FromArgb(110, 117, 128);
            completedExecutableLabel.Location = new Point(65, 135);
            completedExecutableLabel.Name = "completedExecutableLabel";
            completedExecutableLabel.Size = new Size(630, 45);
            completedExecutableLabel.TabIndex = 2;
            // 
            // completedHintLabel
            // 
            completedHintLabel.BackColor = Color.Transparent;
            completedHintLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            completedHintLabel.ForeColor = Color.FromArgb(35, 39, 47);
            completedHintLabel.Location = new Point(65, 200);
            completedHintLabel.Name = "completedHintLabel";
            completedHintLabel.Size = new Size(600, 30);
            completedHintLabel.TabIndex = 3;
            completedHintLabel.Text = "You can now open a terminal and run: aster";
            // 
            // failedPage
            // 
            failedPage.BackColor = Color.FromArgb(248, 249, 251);
            failedPage.Controls.Add(failedMessageLabel);
            failedPage.Controls.Add(failedErrorTextBox);
            failedPage.Location = new Point(0, 180);
            failedPage.Name = "failedPage";
            failedPage.Size = new Size(760, 285);
            failedPage.TabIndex = 10;
            failedPage.Visible = false;
            // 
            // failedMessageLabel
            // 
            failedMessageLabel.BackColor = Color.Transparent;
            failedMessageLabel.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            failedMessageLabel.ForeColor = Color.FromArgb(190, 55, 55);
            failedMessageLabel.Location = new Point(65, 25);
            failedMessageLabel.Name = "failedMessageLabel";
            failedMessageLabel.Size = new Size(600, 35);
            failedMessageLabel.TabIndex = 0;
            failedMessageLabel.Text = "Aster could not be installed.";
            // 
            // failedErrorTextBox
            // 
            failedErrorTextBox.Font = new Font("Consolas", 8.5F);
            failedErrorTextBox.Location = new Point(65, 75);
            failedErrorTextBox.Multiline = true;
            failedErrorTextBox.Name = "failedErrorTextBox";
            failedErrorTextBox.ReadOnly = true;
            failedErrorTextBox.ScrollBars = ScrollBars.Vertical;
            failedErrorTextBox.Size = new Size(625, 185);
            failedErrorTextBox.TabIndex = 1;
            // 
            // cancelledPage
            // 
            cancelledPage.BackColor = Color.FromArgb(248, 249, 251);
            cancelledPage.Controls.Add(cancelledMessageLabel);
            cancelledPage.Location = new Point(0, 180);
            cancelledPage.Name = "cancelledPage";
            cancelledPage.Size = new Size(760, 285);
            cancelledPage.TabIndex = 11;
            cancelledPage.Visible = false;
            // 
            // cancelledMessageLabel
            // 
            cancelledMessageLabel.BackColor = Color.Transparent;
            cancelledMessageLabel.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            cancelledMessageLabel.ForeColor = Color.FromArgb(35, 39, 47);
            cancelledMessageLabel.Location = new Point(65, 60);
            cancelledMessageLabel.Name = "cancelledMessageLabel";
            cancelledMessageLabel.Size = new Size(600, 35);
            cancelledMessageLabel.TabIndex = 0;
            cancelledMessageLabel.Text = "Installation cancelled.";
            // 
            // footerPanel
            // 
            footerPanel.BackColor = Color.White;
            footerPanel.BorderStyle = BorderStyle.FixedSingle;
            footerPanel.Controls.Add(welcomeNextButton);
            footerPanel.Controls.Add(componentsBackButton);
            footerPanel.Controls.Add(componentsNextButton);
            footerPanel.Controls.Add(advancedBackButton);
            footerPanel.Controls.Add(advancedNextButton);
            footerPanel.Controls.Add(readyBackButton);
            footerPanel.Controls.Add(readyInstallButton);
            footerPanel.Controls.Add(cancelInstallButton);
            footerPanel.Controls.Add(completedFinishButton);
            footerPanel.Controls.Add(failedBackButton);
            footerPanel.Controls.Add(failedCloseButton);
            footerPanel.Controls.Add(cancelledCloseButton);
            footerPanel.Location = new Point(0, 465);
            footerPanel.Name = "footerPanel";
            footerPanel.Size = new Size(760, 75);
            footerPanel.TabIndex = 12;
            // 
            // welcomeNextButton
            // 
            welcomeNextButton.BackColor = Color.FromArgb(45, 105, 220);
            welcomeNextButton.Cursor = Cursors.Hand;
            welcomeNextButton.FlatAppearance.BorderColor = Color.FromArgb(222, 226, 232);
            welcomeNextButton.FlatStyle = FlatStyle.Flat;
            welcomeNextButton.ForeColor = Color.White;
            welcomeNextButton.Location = new Point(610, 18);
            welcomeNextButton.Name = "welcomeNextButton";
            welcomeNextButton.Size = new Size(115, 35);
            welcomeNextButton.TabIndex = 0;
            welcomeNextButton.Text = "Next";
            welcomeNextButton.UseVisualStyleBackColor = false;
            // 
            // componentsNextButton
            // 
            componentsNextButton.BackColor = Color.FromArgb(45, 105, 220);
            componentsNextButton.Cursor = Cursors.Hand;
            componentsNextButton.FlatAppearance.BorderColor = Color.FromArgb(222, 226, 232);
            componentsNextButton.FlatStyle = FlatStyle.Flat;
            componentsNextButton.ForeColor = Color.White;
            componentsNextButton.Location = new Point(610, 18);
            componentsNextButton.Name = "componentsNextButton";
            componentsNextButton.Size = new Size(115, 35);
            componentsNextButton.TabIndex = 2;
            componentsNextButton.Text = "Next";
            componentsNextButton.UseVisualStyleBackColor = false;
            componentsNextButton.Visible = false;
            // 
            // advancedNextButton
            // 
            advancedNextButton.BackColor = Color.FromArgb(45, 105, 220);
            advancedNextButton.Cursor = Cursors.Hand;
            advancedNextButton.FlatAppearance.BorderColor = Color.FromArgb(222, 226, 232);
            advancedNextButton.FlatStyle = FlatStyle.Flat;
            advancedNextButton.ForeColor = Color.White;
            advancedNextButton.Location = new Point(610, 18);
            advancedNextButton.Name = "advancedNextButton";
            advancedNextButton.Size = new Size(115, 35);
            advancedNextButton.TabIndex = 4;
            advancedNextButton.Text = "Next";
            advancedNextButton.UseVisualStyleBackColor = false;
            advancedNextButton.Visible = false;
            // 
            // readyInstallButton
            // 
            readyInstallButton.BackColor = Color.FromArgb(45, 105, 220);
            readyInstallButton.Cursor = Cursors.Hand;
            readyInstallButton.FlatAppearance.BorderColor = Color.FromArgb(222, 226, 232);
            readyInstallButton.FlatStyle = FlatStyle.Flat;
            readyInstallButton.ForeColor = Color.White;
            readyInstallButton.Location = new Point(610, 18);
            readyInstallButton.Name = "readyInstallButton";
            readyInstallButton.Size = new Size(115, 35);
            readyInstallButton.TabIndex = 6;
            readyInstallButton.Text = "Install";
            readyInstallButton.UseVisualStyleBackColor = false;
            readyInstallButton.Visible = false;
            // 
            // cancelInstallButton
            // 
            cancelInstallButton.BackColor = Color.White;
            cancelInstallButton.Cursor = Cursors.Hand;
            cancelInstallButton.FlatAppearance.BorderColor = Color.FromArgb(222, 226, 232);
            cancelInstallButton.FlatStyle = FlatStyle.Flat;
            cancelInstallButton.ForeColor = Color.FromArgb(35, 39, 47);
            cancelInstallButton.Location = new Point(610, 18);
            cancelInstallButton.Name = "cancelInstallButton";
            cancelInstallButton.Size = new Size(115, 35);
            cancelInstallButton.TabIndex = 7;
            cancelInstallButton.Text = "Cancel";
            cancelInstallButton.UseVisualStyleBackColor = false;
            cancelInstallButton.Visible = false;
            // 
            // completedFinishButton
            // 
            completedFinishButton.BackColor = Color.White;
            completedFinishButton.Cursor = Cursors.Hand;
            completedFinishButton.FlatAppearance.BorderColor = Color.FromArgb(222, 226, 232);
            completedFinishButton.FlatStyle = FlatStyle.Flat;
            completedFinishButton.ForeColor = Color.FromArgb(35, 39, 47);
            completedFinishButton.Location = new Point(610, 18);
            completedFinishButton.Name = "completedFinishButton";
            completedFinishButton.Size = new Size(115, 35);
            completedFinishButton.TabIndex = 8;
            completedFinishButton.Text = "Finish";
            completedFinishButton.UseVisualStyleBackColor = false;
            completedFinishButton.Visible = false;
            // 
            // failedCloseButton
            // 
            failedCloseButton.BackColor = Color.White;
            failedCloseButton.Cursor = Cursors.Hand;
            failedCloseButton.FlatAppearance.BorderColor = Color.FromArgb(222, 226, 232);
            failedCloseButton.FlatStyle = FlatStyle.Flat;
            failedCloseButton.ForeColor = Color.FromArgb(35, 39, 47);
            failedCloseButton.Location = new Point(610, 18);
            failedCloseButton.Name = "failedCloseButton";
            failedCloseButton.Size = new Size(115, 35);
            failedCloseButton.TabIndex = 10;
            failedCloseButton.Text = "Close";
            failedCloseButton.UseVisualStyleBackColor = false;
            failedCloseButton.Visible = false;
            // 
            // cancelledCloseButton
            // 
            cancelledCloseButton.BackColor = Color.White;
            cancelledCloseButton.Cursor = Cursors.Hand;
            cancelledCloseButton.FlatAppearance.BorderColor = Color.FromArgb(222, 226, 232);
            cancelledCloseButton.FlatStyle = FlatStyle.Flat;
            cancelledCloseButton.ForeColor = Color.FromArgb(35, 39, 47);
            cancelledCloseButton.Location = new Point(610, 18);
            cancelledCloseButton.Name = "cancelledCloseButton";
            cancelledCloseButton.Size = new Size(115, 35);
            cancelledCloseButton.TabIndex = 11;
            cancelledCloseButton.Text = "Close";
            cancelledCloseButton.UseVisualStyleBackColor = false;
            cancelledCloseButton.Visible = false;
            // 
            // failedBackButton
            // 
            failedBackButton.BackColor = Color.White;
            failedBackButton.Cursor = Cursors.Hand;
            failedBackButton.FlatAppearance.BorderColor = Color.FromArgb(222, 226, 232);
            failedBackButton.FlatStyle = FlatStyle.Flat;
            failedBackButton.ForeColor = Color.FromArgb(35, 39, 47);
            failedBackButton.Location = new Point(35, 18);
            failedBackButton.Name = "failedBackButton";
            failedBackButton.Size = new Size(100, 35);
            failedBackButton.TabIndex = 9;
            failedBackButton.Text = "Back";
            failedBackButton.UseVisualStyleBackColor = false;
            failedBackButton.Visible = false;
            // 
            // readyBackButton
            // 
            readyBackButton.BackColor = Color.White;
            readyBackButton.Cursor = Cursors.Hand;
            readyBackButton.FlatAppearance.BorderColor = Color.FromArgb(222, 226, 232);
            readyBackButton.FlatStyle = FlatStyle.Flat;
            readyBackButton.ForeColor = Color.FromArgb(35, 39, 47);
            readyBackButton.Location = new Point(35, 18);
            readyBackButton.Name = "readyBackButton";
            readyBackButton.Size = new Size(100, 35);
            readyBackButton.TabIndex = 5;
            readyBackButton.Text = "Back";
            readyBackButton.UseVisualStyleBackColor = false;
            readyBackButton.Visible = false;
            // 
            // advancedBackButton
            // 
            advancedBackButton.BackColor = Color.White;
            advancedBackButton.Cursor = Cursors.Hand;
            advancedBackButton.FlatAppearance.BorderColor = Color.FromArgb(222, 226, 232);
            advancedBackButton.FlatStyle = FlatStyle.Flat;
            advancedBackButton.ForeColor = Color.FromArgb(35, 39, 47);
            advancedBackButton.Location = new Point(35, 18);
            advancedBackButton.Name = "advancedBackButton";
            advancedBackButton.Size = new Size(100, 35);
            advancedBackButton.TabIndex = 3;
            advancedBackButton.Text = "Back";
            advancedBackButton.UseVisualStyleBackColor = false;
            advancedBackButton.Visible = false;
            // 
            // componentsBackButton
            // 
            componentsBackButton.BackColor = Color.White;
            componentsBackButton.Cursor = Cursors.Hand;
            componentsBackButton.FlatAppearance.BorderColor = Color.FromArgb(222, 226, 232);
            componentsBackButton.FlatStyle = FlatStyle.Flat;
            componentsBackButton.ForeColor = Color.FromArgb(35, 39, 47);
            componentsBackButton.Location = new Point(35, 18);
            componentsBackButton.Name = "componentsBackButton";
            componentsBackButton.Size = new Size(100, 35);
            componentsBackButton.TabIndex = 1;
            componentsBackButton.Text = "Back";
            componentsBackButton.UseVisualStyleBackColor = false;
            componentsBackButton.Visible = false;
            // 
            // Form1
            // 
            ClientSize = new Size(760, 540);
            Controls.Add(brandLabel);
            Controls.Add(pageTitleLabel);
            Controls.Add(pageDescriptionLabel);
            Controls.Add(headerLine);
            Controls.Add(welcomePage);
            Controls.Add(componentsPage);
            Controls.Add(advancedPage);
            Controls.Add(readyPage);
            Controls.Add(installingPage);
            Controls.Add(completedPage);
            Controls.Add(failedPage);
            Controls.Add(cancelledPage);
            Controls.Add(footerPanel);
            MaximumSize = new Size(776, 579);
            MinimumSize = new Size(776, 579);
            Name = "Form1";
            Text = "Aster Setup";
            welcomePage.ResumeLayout(false);
            componentsPage.ResumeLayout(false);
            advancedPage.ResumeLayout(false);
            advancedPage.PerformLayout();
            readyPage.ResumeLayout(false);
            installingPage.ResumeLayout(false);
            completedPage.ResumeLayout(false);
            failedPage.ResumeLayout(false);
            failedPage.PerformLayout();
            cancelledPage.ResumeLayout(false);
            footerPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label brandLabel;
        private System.Windows.Forms.Label pageTitleLabel;
        private System.Windows.Forms.Label pageDescriptionLabel;
        private System.Windows.Forms.Panel headerLine;
        private System.Windows.Forms.Panel welcomePage;
        private System.Windows.Forms.Panel componentsPage;
        private System.Windows.Forms.Panel advancedPage;
        private System.Windows.Forms.Panel readyPage;
        private System.Windows.Forms.Panel installingPage;
        private System.Windows.Forms.Panel completedPage;
        private System.Windows.Forms.Panel failedPage;
        private System.Windows.Forms.Panel cancelledPage;
        private System.Windows.Forms.Label welcomeAsterLabel;
        private System.Windows.Forms.Label welcomeTaglineLabel;
        private System.Windows.Forms.Label welcomeDescriptionLabel;
        private System.Windows.Forms.CheckBox sourceComponentCheckBox;
        private System.Windows.Forms.CheckBox compilerComponentCheckBox;
        private System.Windows.Forms.CheckBox pythonComponentCheckBox;
        private System.Windows.Forms.CheckBox vscExtensionCheckBox;
        private System.Windows.Forms.Label advancedPathCaptionLabel;
        private System.Windows.Forms.TextBox advancedPathTextBox;
        private System.Windows.Forms.Button advancedBrowseButton;
        private System.Windows.Forms.CheckBox advancedPathCheckBox;
        private System.Windows.Forms.Label advancedNoteLabel;
        private System.Windows.Forms.Label readyPathCaptionLabel;
        private System.Windows.Forms.Label readyPathLabel;
        private System.Windows.Forms.Label readyOperationsCaptionLabel;
        private System.Windows.Forms.Label readyOperationsLabel;
        private System.Windows.Forms.Label installingStatusLabel;
        private System.Windows.Forms.Label installingDetailsLabel;
        private System.Windows.Forms.ProgressBar installingProgressBar;
        private System.Windows.Forms.RichTextBox installingLogBox;
        private System.Windows.Forms.Label completedMessageLabel;
        private System.Windows.Forms.Label completedExecutableCaptionLabel;
        private System.Windows.Forms.Label completedExecutableLabel;
        private System.Windows.Forms.Label completedHintLabel;
        private System.Windows.Forms.Label failedMessageLabel;
        private System.Windows.Forms.TextBox failedErrorTextBox;
        private System.Windows.Forms.Label cancelledMessageLabel;
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.Button welcomeNextButton;
        private System.Windows.Forms.Button componentsNextButton;
        private System.Windows.Forms.Button advancedNextButton;
        private System.Windows.Forms.Button readyInstallButton;
        private System.Windows.Forms.Button cancelInstallButton;
        private System.Windows.Forms.Button completedFinishButton;
        private System.Windows.Forms.Button failedCloseButton;
        private System.Windows.Forms.Button cancelledCloseButton;
        private Button componentsBackButton;
        private Button advancedBackButton;
        private Button readyBackButton;
        private Button failedBackButton;
    }
}
