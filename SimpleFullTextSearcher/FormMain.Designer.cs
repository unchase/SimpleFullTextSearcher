namespace SimpleFullTextSearcher
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.sfsStatusStrip = new System.Windows.Forms.StatusStrip();
            this.sfsToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.sfsSearchCriteriaGroupBox = new System.Windows.Forms.GroupBox();
            this.searchInImagescheckBox = new System.Windows.Forms.CheckBox();
            this.searchInZipArchiveCheckBox = new System.Windows.Forms.CheckBox();
            this.sfsSearchTextClearButton = new System.Windows.Forms.Button();
            this.sfsFileNamePatternClearButton = new System.Windows.Forms.Button();
            this.sfsInitialDirectoryClearButton = new System.Windows.Forms.Button();
            this.sfsInitialDirectorySelectButton = new System.Windows.Forms.Button();
            this.sfsSearchTextTextBox = new System.Windows.Forms.TextBox();
            this.sfsSearchTextLabel = new System.Windows.Forms.Label();
            this.sfsFileNamePatternTextBox = new System.Windows.Forms.TextBox();
            this.sfsFileNamePatternLabel = new System.Windows.Forms.Label();
            this.sfsInitialDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.sfsInitialDirectoryLabel = new System.Windows.Forms.Label();
            this.sfsSearchLabel = new System.Windows.Forms.Label();
            this.sfsSearchStartButton = new System.Windows.Forms.Button();
            this.sfsSearchPauseButton = new System.Windows.Forms.Button();
            this.sfsSearchStopButton = new System.Windows.Forms.Button();
            this.sfsSearchResultsGroupBox = new System.Windows.Forms.GroupBox();
            this.sfsSearchResultsTreeView = new System.Windows.Forms.TreeView();
            this.sfsTreeViewImageList = new System.Windows.Forms.ImageList(this.components);
            this.sfsAboutButton = new System.Windows.Forms.Button();
            this.menuStripMainForm = new System.Windows.Forms.MenuStrip();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ruLangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enLangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sfsStatusStrip.SuspendLayout();
            this.sfsSearchCriteriaGroupBox.SuspendLayout();
            this.sfsSearchResultsGroupBox.SuspendLayout();
            this.menuStripMainForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // sfsStatusStrip
            // 
            this.sfsStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sfsToolStripStatusLabel});
            this.sfsStatusStrip.Location = new System.Drawing.Point(0, 562);
            this.sfsStatusStrip.Name = "sfsStatusStrip";
            this.sfsStatusStrip.Size = new System.Drawing.Size(536, 22);
            this.sfsStatusStrip.TabIndex = 0;
            // 
            // sfsToolStripStatusLabel
            // 
            this.sfsToolStripStatusLabel.Name = "sfsToolStripStatusLabel";
            this.sfsToolStripStatusLabel.Size = new System.Drawing.Size(122, 17);
            this.sfsToolStripStatusLabel.Text = "Ready to the first start";
            // 
            // sfsSearchCriteriaGroupBox
            // 
            this.sfsSearchCriteriaGroupBox.Controls.Add(this.searchInImagescheckBox);
            this.sfsSearchCriteriaGroupBox.Controls.Add(this.searchInZipArchiveCheckBox);
            this.sfsSearchCriteriaGroupBox.Controls.Add(this.sfsSearchTextClearButton);
            this.sfsSearchCriteriaGroupBox.Controls.Add(this.sfsFileNamePatternClearButton);
            this.sfsSearchCriteriaGroupBox.Controls.Add(this.sfsInitialDirectoryClearButton);
            this.sfsSearchCriteriaGroupBox.Controls.Add(this.sfsInitialDirectorySelectButton);
            this.sfsSearchCriteriaGroupBox.Controls.Add(this.sfsSearchTextTextBox);
            this.sfsSearchCriteriaGroupBox.Controls.Add(this.sfsSearchTextLabel);
            this.sfsSearchCriteriaGroupBox.Controls.Add(this.sfsFileNamePatternTextBox);
            this.sfsSearchCriteriaGroupBox.Controls.Add(this.sfsFileNamePatternLabel);
            this.sfsSearchCriteriaGroupBox.Controls.Add(this.sfsInitialDirectoryTextBox);
            this.sfsSearchCriteriaGroupBox.Controls.Add(this.sfsInitialDirectoryLabel);
            this.sfsSearchCriteriaGroupBox.Location = new System.Drawing.Point(12, 28);
            this.sfsSearchCriteriaGroupBox.Name = "sfsSearchCriteriaGroupBox";
            this.sfsSearchCriteriaGroupBox.Size = new System.Drawing.Size(512, 129);
            this.sfsSearchCriteriaGroupBox.TabIndex = 1;
            this.sfsSearchCriteriaGroupBox.TabStop = false;
            this.sfsSearchCriteriaGroupBox.Text = "Search criteria";
            // 
            // searchInImagescheckBox
            // 
            this.searchInImagescheckBox.AutoSize = true;
            this.searchInImagescheckBox.Location = new System.Drawing.Point(6, 106);
            this.searchInImagescheckBox.Name = "searchInImagescheckBox";
            this.searchInImagescheckBox.Size = new System.Drawing.Size(155, 17);
            this.searchInImagescheckBox.TabIndex = 4;
            this.searchInImagescheckBox.Text = "Search in image files (OCR)";
            this.searchInImagescheckBox.UseVisualStyleBackColor = true;
            // 
            // searchInZipArchiveCheckBox
            // 
            this.searchInZipArchiveCheckBox.AutoSize = true;
            this.searchInZipArchiveCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.searchInZipArchiveCheckBox.Location = new System.Drawing.Point(376, 106);
            this.searchInZipArchiveCheckBox.Name = "searchInZipArchiveCheckBox";
            this.searchInZipArchiveCheckBox.Size = new System.Drawing.Size(130, 17);
            this.searchInZipArchiveCheckBox.TabIndex = 3;
            this.searchInZipArchiveCheckBox.Text = "Search in zip-archives";
            this.searchInZipArchiveCheckBox.UseVisualStyleBackColor = true;
            this.searchInZipArchiveCheckBox.Visible = false;
            // 
            // sfsSearchTextClearButton
            // 
            this.sfsSearchTextClearButton.Image = ((System.Drawing.Image)(resources.GetObject("sfsSearchTextClearButton.Image")));
            this.sfsSearchTextClearButton.Location = new System.Drawing.Point(482, 72);
            this.sfsSearchTextClearButton.Name = "sfsSearchTextClearButton";
            this.sfsSearchTextClearButton.Size = new System.Drawing.Size(24, 23);
            this.sfsSearchTextClearButton.TabIndex = 2;
            this.sfsSearchTextClearButton.UseVisualStyleBackColor = true;
            this.sfsSearchTextClearButton.Click += new System.EventHandler(this.sfsSearchTextClearButton_Click);
            // 
            // sfsFileNamePatternClearButton
            // 
            this.sfsFileNamePatternClearButton.Image = ((System.Drawing.Image)(resources.GetObject("sfsFileNamePatternClearButton.Image")));
            this.sfsFileNamePatternClearButton.Location = new System.Drawing.Point(482, 46);
            this.sfsFileNamePatternClearButton.Name = "sfsFileNamePatternClearButton";
            this.sfsFileNamePatternClearButton.Size = new System.Drawing.Size(24, 23);
            this.sfsFileNamePatternClearButton.TabIndex = 2;
            this.sfsFileNamePatternClearButton.UseVisualStyleBackColor = true;
            this.sfsFileNamePatternClearButton.Click += new System.EventHandler(this.sfsFileNamePatternClearButton_Click);
            // 
            // sfsInitialDirectoryClearButton
            // 
            this.sfsInitialDirectoryClearButton.Image = ((System.Drawing.Image)(resources.GetObject("sfsInitialDirectoryClearButton.Image")));
            this.sfsInitialDirectoryClearButton.Location = new System.Drawing.Point(482, 20);
            this.sfsInitialDirectoryClearButton.Name = "sfsInitialDirectoryClearButton";
            this.sfsInitialDirectoryClearButton.Size = new System.Drawing.Size(24, 23);
            this.sfsInitialDirectoryClearButton.TabIndex = 2;
            this.sfsInitialDirectoryClearButton.UseVisualStyleBackColor = true;
            this.sfsInitialDirectoryClearButton.Click += new System.EventHandler(this.sfsInitialDirectoryClearButton_Click);
            // 
            // sfsInitialDirectorySelectButton
            // 
            this.sfsInitialDirectorySelectButton.Image = ((System.Drawing.Image)(resources.GetObject("sfsInitialDirectorySelectButton.Image")));
            this.sfsInitialDirectorySelectButton.Location = new System.Drawing.Point(452, 20);
            this.sfsInitialDirectorySelectButton.Name = "sfsInitialDirectorySelectButton";
            this.sfsInitialDirectorySelectButton.Size = new System.Drawing.Size(24, 23);
            this.sfsInitialDirectorySelectButton.TabIndex = 2;
            this.sfsInitialDirectorySelectButton.UseVisualStyleBackColor = true;
            this.sfsInitialDirectorySelectButton.Click += new System.EventHandler(this.sfsInitialDirectorySelectButton_Click);
            // 
            // sfsSearchTextTextBox
            // 
            this.sfsSearchTextTextBox.Location = new System.Drawing.Point(178, 74);
            this.sfsSearchTextTextBox.Name = "sfsSearchTextTextBox";
            this.sfsSearchTextTextBox.Size = new System.Drawing.Size(298, 20);
            this.sfsSearchTextTextBox.TabIndex = 1;
            // 
            // sfsSearchTextLabel
            // 
            this.sfsSearchTextLabel.AutoSize = true;
            this.sfsSearchTextLabel.Location = new System.Drawing.Point(6, 77);
            this.sfsSearchTextLabel.Name = "sfsSearchTextLabel";
            this.sfsSearchTextLabel.Size = new System.Drawing.Size(76, 13);
            this.sfsSearchTextLabel.TabIndex = 0;
            this.sfsSearchTextLabel.Text = "Searched text:";
            // 
            // sfsFileNamePatternTextBox
            // 
            this.sfsFileNamePatternTextBox.Location = new System.Drawing.Point(178, 48);
            this.sfsFileNamePatternTextBox.Name = "sfsFileNamePatternTextBox";
            this.sfsFileNamePatternTextBox.Size = new System.Drawing.Size(298, 20);
            this.sfsFileNamePatternTextBox.TabIndex = 1;
            // 
            // sfsFileNamePatternLabel
            // 
            this.sfsFileNamePatternLabel.AutoSize = true;
            this.sfsFileNamePatternLabel.Location = new System.Drawing.Point(6, 51);
            this.sfsFileNamePatternLabel.Name = "sfsFileNamePatternLabel";
            this.sfsFileNamePatternLabel.Size = new System.Drawing.Size(91, 13);
            this.sfsFileNamePatternLabel.TabIndex = 0;
            this.sfsFileNamePatternLabel.Text = "File-name pattern:";
            // 
            // sfsInitialDirectoryTextBox
            // 
            this.sfsInitialDirectoryTextBox.Location = new System.Drawing.Point(178, 22);
            this.sfsInitialDirectoryTextBox.Name = "sfsInitialDirectoryTextBox";
            this.sfsInitialDirectoryTextBox.ReadOnly = true;
            this.sfsInitialDirectoryTextBox.Size = new System.Drawing.Size(268, 20);
            this.sfsInitialDirectoryTextBox.TabIndex = 1;
            // 
            // sfsInitialDirectoryLabel
            // 
            this.sfsInitialDirectoryLabel.AutoSize = true;
            this.sfsInitialDirectoryLabel.Location = new System.Drawing.Point(6, 25);
            this.sfsInitialDirectoryLabel.Name = "sfsInitialDirectoryLabel";
            this.sfsInitialDirectoryLabel.Size = new System.Drawing.Size(124, 13);
            this.sfsInitialDirectoryLabel.TabIndex = 0;
            this.sfsInitialDirectoryLabel.Text = "Starting search directory:";
            // 
            // sfsSearchLabel
            // 
            this.sfsSearchLabel.AutoSize = true;
            this.sfsSearchLabel.Location = new System.Drawing.Point(167, 168);
            this.sfsSearchLabel.Name = "sfsSearchLabel";
            this.sfsSearchLabel.Size = new System.Drawing.Size(44, 13);
            this.sfsSearchLabel.TabIndex = 2;
            this.sfsSearchLabel.Text = "Search:";
            // 
            // sfsSearchStartButton
            // 
            this.sfsSearchStartButton.Image = ((System.Drawing.Image)(resources.GetObject("sfsSearchStartButton.Image")));
            this.sfsSearchStartButton.Location = new System.Drawing.Point(215, 163);
            this.sfsSearchStartButton.Name = "sfsSearchStartButton";
            this.sfsSearchStartButton.Size = new System.Drawing.Size(99, 23);
            this.sfsSearchStartButton.TabIndex = 3;
            this.sfsSearchStartButton.Text = "Start";
            this.sfsSearchStartButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sfsSearchStartButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.sfsSearchStartButton.UseVisualStyleBackColor = true;
            this.sfsSearchStartButton.Click += new System.EventHandler(this.sfsSearchStartButton_Click);
            // 
            // sfsSearchPauseButton
            // 
            this.sfsSearchPauseButton.Enabled = false;
            this.sfsSearchPauseButton.Image = ((System.Drawing.Image)(resources.GetObject("sfsSearchPauseButton.Image")));
            this.sfsSearchPauseButton.Location = new System.Drawing.Point(320, 163);
            this.sfsSearchPauseButton.Name = "sfsSearchPauseButton";
            this.sfsSearchPauseButton.Size = new System.Drawing.Size(99, 23);
            this.sfsSearchPauseButton.TabIndex = 3;
            this.sfsSearchPauseButton.Text = "Pause";
            this.sfsSearchPauseButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sfsSearchPauseButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.sfsSearchPauseButton.UseVisualStyleBackColor = true;
            this.sfsSearchPauseButton.Click += new System.EventHandler(this.sfsSearchPauseButton_Click);
            // 
            // sfsSearchStopButton
            // 
            this.sfsSearchStopButton.Enabled = false;
            this.sfsSearchStopButton.Image = ((System.Drawing.Image)(resources.GetObject("sfsSearchStopButton.Image")));
            this.sfsSearchStopButton.Location = new System.Drawing.Point(425, 163);
            this.sfsSearchStopButton.Name = "sfsSearchStopButton";
            this.sfsSearchStopButton.Size = new System.Drawing.Size(99, 23);
            this.sfsSearchStopButton.TabIndex = 3;
            this.sfsSearchStopButton.Text = "Stop";
            this.sfsSearchStopButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sfsSearchStopButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.sfsSearchStopButton.UseVisualStyleBackColor = true;
            this.sfsSearchStopButton.Click += new System.EventHandler(this.sfsSearchStopButton_Click);
            // 
            // sfsSearchResultsGroupBox
            // 
            this.sfsSearchResultsGroupBox.Controls.Add(this.sfsSearchResultsTreeView);
            this.sfsSearchResultsGroupBox.Location = new System.Drawing.Point(12, 193);
            this.sfsSearchResultsGroupBox.Name = "sfsSearchResultsGroupBox";
            this.sfsSearchResultsGroupBox.Size = new System.Drawing.Size(512, 366);
            this.sfsSearchResultsGroupBox.TabIndex = 4;
            this.sfsSearchResultsGroupBox.TabStop = false;
            this.sfsSearchResultsGroupBox.Text = "Search results";
            // 
            // sfsSearchResultsTreeView
            // 
            this.sfsSearchResultsTreeView.ImageIndex = 0;
            this.sfsSearchResultsTreeView.ImageList = this.sfsTreeViewImageList;
            this.sfsSearchResultsTreeView.Location = new System.Drawing.Point(7, 20);
            this.sfsSearchResultsTreeView.Name = "sfsSearchResultsTreeView";
            this.sfsSearchResultsTreeView.SelectedImageIndex = 0;
            this.sfsSearchResultsTreeView.Size = new System.Drawing.Size(499, 376);
            this.sfsSearchResultsTreeView.TabIndex = 0;
            // 
            // sfsTreeViewImageList
            // 
            this.sfsTreeViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("sfsTreeViewImageList.ImageStream")));
            this.sfsTreeViewImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.sfsTreeViewImageList.Images.SetKeyName(0, "folder.png");
            this.sfsTreeViewImageList.Images.SetKeyName(1, "file.png");
            // 
            // sfsAboutButton
            // 
            this.sfsAboutButton.Image = ((System.Drawing.Image)(resources.GetObject("sfsAboutButton.Image")));
            this.sfsAboutButton.Location = new System.Drawing.Point(12, 163);
            this.sfsAboutButton.Name = "sfsAboutButton";
            this.sfsAboutButton.Size = new System.Drawing.Size(113, 23);
            this.sfsAboutButton.TabIndex = 3;
            this.sfsAboutButton.Text = "About";
            this.sfsAboutButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sfsAboutButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.sfsAboutButton.UseVisualStyleBackColor = true;
            this.sfsAboutButton.Click += new System.EventHandler(this.sfsAboutButton_Click);
            // 
            // menuStripMainForm
            // 
            this.menuStripMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.languageToolStripMenuItem});
            this.menuStripMainForm.Location = new System.Drawing.Point(0, 0);
            this.menuStripMainForm.Name = "menuStripMainForm";
            this.menuStripMainForm.Size = new System.Drawing.Size(536, 24);
            this.menuStripMainForm.TabIndex = 5;
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ruLangToolStripMenuItem,
            this.enLangToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.languageToolStripMenuItem.Text = "Language";
            // 
            // ruLangToolStripMenuItem
            // 
            this.ruLangToolStripMenuItem.CheckOnClick = true;
            this.ruLangToolStripMenuItem.Name = "ruLangToolStripMenuItem";
            this.ruLangToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.ruLangToolStripMenuItem.Text = "Русский";
            this.ruLangToolStripMenuItem.Click += new System.EventHandler(this.RuLangToolStripMenuItem_Click);
            // 
            // enLangToolStripMenuItem
            // 
            this.enLangToolStripMenuItem.CheckOnClick = true;
            this.enLangToolStripMenuItem.Name = "enLangToolStripMenuItem";
            this.enLangToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.enLangToolStripMenuItem.Text = "English";
            this.enLangToolStripMenuItem.Click += new System.EventHandler(this.EnLangToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 584);
            this.Controls.Add(this.sfsSearchResultsGroupBox);
            this.Controls.Add(this.sfsSearchStopButton);
            this.Controls.Add(this.sfsSearchPauseButton);
            this.Controls.Add(this.sfsAboutButton);
            this.Controls.Add(this.sfsSearchStartButton);
            this.Controls.Add(this.sfsSearchLabel);
            this.Controls.Add(this.sfsSearchCriteriaGroupBox);
            this.Controls.Add(this.sfsStatusStrip);
            this.Controls.Add(this.menuStripMainForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMainForm;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple Full-Text Searcher v 1.1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.sfsStatusStrip.ResumeLayout(false);
            this.sfsStatusStrip.PerformLayout();
            this.sfsSearchCriteriaGroupBox.ResumeLayout(false);
            this.sfsSearchCriteriaGroupBox.PerformLayout();
            this.sfsSearchResultsGroupBox.ResumeLayout(false);
            this.menuStripMainForm.ResumeLayout(false);
            this.menuStripMainForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip sfsStatusStrip;
        private System.Windows.Forms.GroupBox sfsSearchCriteriaGroupBox;
        private System.Windows.Forms.Button sfsSearchTextClearButton;
        private System.Windows.Forms.Button sfsFileNamePatternClearButton;
        private System.Windows.Forms.Button sfsInitialDirectoryClearButton;
        private System.Windows.Forms.Button sfsInitialDirectorySelectButton;
        private System.Windows.Forms.TextBox sfsSearchTextTextBox;
        private System.Windows.Forms.Label sfsSearchTextLabel;
        private System.Windows.Forms.TextBox sfsFileNamePatternTextBox;
        private System.Windows.Forms.Label sfsFileNamePatternLabel;
        private System.Windows.Forms.TextBox sfsInitialDirectoryTextBox;
        private System.Windows.Forms.Label sfsInitialDirectoryLabel;
        private System.Windows.Forms.Label sfsSearchLabel;
        private System.Windows.Forms.Button sfsSearchStartButton;
        private System.Windows.Forms.Button sfsSearchPauseButton;
        private System.Windows.Forms.Button sfsSearchStopButton;
        private System.Windows.Forms.GroupBox sfsSearchResultsGroupBox;
        private System.Windows.Forms.TreeView sfsSearchResultsTreeView;
        private System.Windows.Forms.ToolStripStatusLabel sfsToolStripStatusLabel;
        private System.Windows.Forms.ImageList sfsTreeViewImageList;
        private System.Windows.Forms.Button sfsAboutButton;
        private System.Windows.Forms.CheckBox searchInZipArchiveCheckBox;
        private System.Windows.Forms.CheckBox searchInImagescheckBox;
        private System.Windows.Forms.MenuStrip menuStripMainForm;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ruLangToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enLangToolStripMenuItem;
    }
}

