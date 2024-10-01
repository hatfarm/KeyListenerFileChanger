using System.Windows.Forms;

namespace GlobalKeyListener.Window
{
    partial class StatusWindow
    {
        private static Color IsListeningColor = System.Drawing.Color.Goldenrod;
        private static Color IsNotListeningColor = System.Drawing.Color.CornflowerBlue;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            statusPanel = new Panel();
            indicatorLabel = new Label();
            buttonMappingLabel = new Label();
            indicatorKeyLabel = new Label();
            buttonMappingsDataGridView = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            addButtonMappingButton = new Button();
            fileSaveLocationLabel = new Label();
            modifyFileSaveLocationLink = new LinkLabel();
            fileSaveLocationTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)buttonMappingsDataGridView).BeginInit();
            SuspendLayout();
            // 
            // statusPanel
            // 
            statusPanel.BorderStyle = BorderStyle.FixedSingle;
            statusPanel.Location = new Point(223, 210);
            statusPanel.Name = "statusPanel";
            statusPanel.Size = new Size(50, 50);
            statusPanel.TabIndex = 1;
            // 
            // indicatorLabel
            // 
            indicatorLabel.AutoSize = true;
            indicatorLabel.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            indicatorLabel.Location = new Point(19, 207);
            indicatorLabel.Name = "indicatorLabel";
            indicatorLabel.Size = new Size(198, 30);
            indicatorLabel.TabIndex = 2;
            indicatorLabel.Text = "Currently Listening:";
            // 
            // buttonMappingLabel
            // 
            buttonMappingLabel.AutoSize = true;
            buttonMappingLabel.Location = new Point(12, 17);
            buttonMappingLabel.Name = "buttonMappingLabel";
            buttonMappingLabel.Size = new Size(102, 15);
            buttonMappingLabel.TabIndex = 3;
            buttonMappingLabel.Text = "Button Mappings:";
            // 
            // indicatorKeyLabel
            // 
            indicatorKeyLabel.AutoSize = true;
            indicatorKeyLabel.Location = new Point(58, 237);
            indicatorKeyLabel.Name = "indicatorKeyLabel";
            indicatorKeyLabel.Size = new Size(123, 15);
            indicatorKeyLabel.TabIndex = 4;
            indicatorKeyLabel.Text = "(Yellow: Yes, Blue: No)";
            // 
            // buttonMappingsDataGridView
            // 
            buttonMappingsDataGridView.AllowUserToAddRows = false;
            buttonMappingsDataGridView.AllowUserToDeleteRows = false;
            buttonMappingsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            buttonMappingsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            buttonMappingsDataGridView.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2 });
            buttonMappingsDataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            buttonMappingsDataGridView.Location = new Point(12, 40);
            buttonMappingsDataGridView.MultiSelect = false;
            buttonMappingsDataGridView.Name = "buttonMappingsDataGridView";
            buttonMappingsDataGridView.RowHeadersVisible = false;
            buttonMappingsDataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            buttonMappingsDataGridView.Size = new Size(360, 164);
            buttonMappingsDataGridView.TabIndex = 0;
            buttonMappingsDataGridView.CellDoubleClick += buttonMappingsDataGridView_CellDoubleClick;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Button";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Description";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // addButtonMappingButton
            // 
            addButtonMappingButton.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            addButtonMappingButton.Location = new Point(120, 12);
            addButtonMappingButton.Name = "addButtonMappingButton";
            addButtonMappingButton.Size = new Size(23, 22);
            addButtonMappingButton.TabIndex = 5;
            addButtonMappingButton.Text = "+";
            addButtonMappingButton.UseVisualStyleBackColor = true;
            addButtonMappingButton.Click += addButtonMappingButton_Click;
            // 
            // fileSaveLocationLabel
            // 
            fileSaveLocationLabel.AutoSize = true;
            fileSaveLocationLabel.Location = new Point(12, 265);
            fileSaveLocationLabel.Name = "fileSaveLocationLabel";
            fileSaveLocationLabel.Size = new Size(70, 15);
            fileSaveLocationLabel.TabIndex = 6;
            fileSaveLocationLabel.Text = "Save Folder:";
            // 
            // modifyFileSaveLocationLink
            // 
            modifyFileSaveLocationLink.AutoSize = true;
            modifyFileSaveLocationLink.Location = new Point(12, 280);
            modifyFileSaveLocationLink.Name = "modifyFileSaveLocationLink";
            modifyFileSaveLocationLink.Size = new Size(45, 15);
            modifyFileSaveLocationLink.TabIndex = 8;
            modifyFileSaveLocationLink.TabStop = true;
            modifyFileSaveLocationLink.Text = "Modify";
            modifyFileSaveLocationLink.LinkClicked += modifyFileSaveLocationLink_LinkClicked;
            // 
            // fileSaveLocationTextBox
            // 
            fileSaveLocationTextBox.Location = new Point(100, 262);
            fileSaveLocationTextBox.Name = "fileSaveLocationTextBox";
            fileSaveLocationTextBox.ReadOnly = true;
            fileSaveLocationTextBox.ScrollBars = ScrollBars.Horizontal;
            fileSaveLocationTextBox.Size = new Size(272, 23);
            fileSaveLocationTextBox.TabIndex = 7;
            fileSaveLocationTextBox.WordWrap = false;
            // 
            // StatusWindow
            // 
            ClientSize = new Size(384, 298);
            Controls.Add(addButtonMappingButton);
            Controls.Add(buttonMappingsDataGridView);
            Controls.Add(indicatorKeyLabel);
            Controls.Add(buttonMappingLabel);
            Controls.Add(indicatorLabel);
            Controls.Add(statusPanel);
            Controls.Add(fileSaveLocationLabel);
            Controls.Add(fileSaveLocationTextBox);
            Controls.Add(modifyFileSaveLocationLink);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "StatusWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hatfarm's Twitch Key Listener";
            FormClosing += StatusWindow_FormClosing;
            ((System.ComponentModel.ISupportInitialize)buttonMappingsDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel statusPanel;

        public void SetButtonMappings(IReadOnlyDictionary<Keys, string> buttonToFilenameMap, Keys[] appExitKeyCombo, Keys[] fileCreationToggleCombo)
        {
            this.buttonMappingsDataGridView.Rows.Clear();
            foreach (var mapping in buttonToFilenameMap)
            {
                this.buttonMappingsDataGridView.Rows.Add(mapping.Key.ToString(), $"Updates {mapping.Value}");
            }
            this.buttonMappingsDataGridView.Rows.Add(string.Join(" + ", appExitKeyCombo), "Exit Application");
            this.buttonMappingsDataGridView.Rows.Add(string.Join(" + ", fileCreationToggleCombo), "Toggle Listening To Keys");
        }

        public void UpdateListeningStatus(bool isListening)
        {
            this.statusPanel.BackColor = isListening ? IsListeningColor : IsNotListeningColor;
        }

        private Label indicatorLabel;
        private Label buttonMappingLabel;
        private Label indicatorKeyLabel;
        private Label fileSaveLocationLabel;
        private TextBox fileSaveLocationTextBox;
        private LinkLabel modifyFileSaveLocationLink;
        private DataGridView buttonMappingsDataGridView;
        private Button addButtonMappingButton;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}