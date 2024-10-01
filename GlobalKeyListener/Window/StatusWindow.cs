using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace GlobalKeyListener.Window;

public partial class StatusWindow : Form
{
    private readonly FileWriter fileWriter;

    public Func<Keys, Keys, bool>? TrySetNewKey { get; set; }

    public Func<Keys, string, bool>? TrySetNewKeyTarget { get; set; }

    public Func<Keys[], bool>? TryUpdateFileCreationToggleCombo { get; set; }

    public Func<Keys[], bool>? TryUpdateAppExitKeyCombo { get; set; }

    public Action WriteToJson { get; set; }

    public StatusWindow(FileWriter fileWriter)
    {
        InitializeComponent();
        this.statusPanel.BackColor = IsNotListeningColor;
        this.fileWriter = fileWriter;
        this.UpdateFileSaveLocationLabel();
    }

    private void StatusWindow_FormClosing(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void addButtonMappingButton_Click(object sender, EventArgs e)
    {
        string buttonString = Prompt.ShowDialog("Button to Add", string.Empty);
        if (string.IsNullOrWhiteSpace(buttonString))
        {
            return;
        }

        Keys button = (Keys)Enum.Parse(typeof(Keys), buttonString);
        string action = Prompt.ShowDialog("File to Update", string.Empty);

        if (string.IsNullOrWhiteSpace(action))
        {
            return;
        }

        this.TrySetNewKeyTarget?.Invoke(button, action);
    }

    private void buttonMappingsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
        {
            var cell = this.buttonMappingsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string currentValue = cell.Value?.ToString() ?? string.Empty;

            // Show an input dialog to get the new value
            string newValue = Prompt.ShowDialog("Edit Button", currentValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                return;
            }

            if (e.ColumnIndex == 0)
            {
                newValue = newValue.ToUpper();
            }

            if (!string.IsNullOrEmpty(newValue))
            {
                cell.Value = newValue;

                // Update the underlying data structure
                this.UpdateButtonMappings(e.RowIndex, e.ColumnIndex, currentValue, newValue);
            }
        }
    }

    private void UpdateButtonMappings(int rowIndex, int columnIndex, string oldValue, string newValue)
    {
        if (columnIndex == 0) // Key column
        {
            // This is the toggle key combo
            if (rowIndex == this.buttonMappingsDataGridView.Rows.Count - 1)
            {
                this.TryUpdateFileCreationToggleCombo?.Invoke(this.GetButtonComboMapping(newValue));
                return;
            }

            // This is the Exit key combo
            if (rowIndex == this.buttonMappingsDataGridView.Rows.Count - 2)
            {
                this.TryUpdateAppExitKeyCombo?.Invoke(this.GetButtonComboMapping(newValue));
                return;
            }

            Keys oldKey = (Keys)Enum.Parse(typeof(Keys), oldValue);
            Keys newKey = (Keys)Enum.Parse(typeof(Keys), newValue);

            string description = this.buttonMappingsDataGridView.Rows[rowIndex].Cells[1].Value.ToString();
            this.TrySetNewKey?.Invoke(oldKey, newKey);
        }
        else if (columnIndex == 1) // Action column
        {
            Keys key = (Keys)Enum.Parse(typeof(Keys), this.buttonMappingsDataGridView.Rows[rowIndex].Cells[0].Value.ToString());
            this.TrySetNewKeyTarget?.Invoke(key, newValue);
        }
    }

    private void modifyFileSaveLocationLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
        {
            folderDialog.Description = "Select the folder to save files";
            folderDialog.SelectedPath = Path.GetFullPath(this.fileWriter.FileSaveRoot);

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = folderDialog.SelectedPath;
                this.fileWriter.FileSaveRoot = selectedPath;
                this.UpdateFileSaveLocationLabel();

                // Optionally, save the new path to the configuration file
                this.WriteToJson();
            }
        }
    }

    private void UpdateFileSaveLocationLabel()
    {
        this.fileSaveLocationTextBox.Text = Path.GetFullPath(this.fileWriter.FileSaveRoot);
    }

    private Keys[] GetButtonComboMapping(string keys)
    {
        return keys.Split("+").Select(key => (Keys)Enum.Parse(typeof(Keys), key.Trim(), true)).ToArray();
    }
}
