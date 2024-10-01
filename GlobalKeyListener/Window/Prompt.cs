// <copyright file="Prompt.cs" company="PlaceholderCompany">
// Licensed under the MIT License. See the LICENSE file for more details.
// </copyright>

using System.Windows.Input;

namespace GlobalKeyListener.Window;

public static class Prompt
{
    public static string ShowDialog(string title, string defaultValue)
    {
        Form prompt = new Form()
        {
            Width = 300,
            Height = 150,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            Text = title,
            StartPosition = FormStartPosition.CenterScreen
        };
        Label textLabel = new Label() { Left = 10, Top = 20, Text = title };
        TextBox textBox = new TextBox() { Left = 10, Top = 50, Width = 260, Text = defaultValue };
        Button confirmation = new Button() { Text = "Ok", Left = 200, Width = 70, Top = 80, DialogResult = DialogResult.OK };
        confirmation.Click += (sender, e) => { prompt.Close(); };
        prompt.Controls.Add(textBox);
        prompt.Controls.Add(confirmation);
        prompt.Controls.Add(textLabel);
        prompt.AcceptButton = confirmation;

        return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : defaultValue;
    }
}
