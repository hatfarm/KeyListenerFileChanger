// <copyright file="Program.cs" company="PlaceholderCompany">
// Licensed under the MIT License. See the LICENSE file for more details.
// </copyright>

namespace GlobalKeyLister;

using GlobalKeyListener;
using GlobalKeyListener.Config;
using GlobalKeyListener.Serialization;
using GlobalKeyListener.Window;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

/// <summary>
/// The main program class.
/// </summary>
public static class Program
{
    /// <summary>
    /// The main function, this is where execution starts.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    [STAThread]
    public static void Main(string[] args)
    {
        try {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GlobalKeyListenerConfig config = LoadConfig();

            FileWriter fileWriter = new(config.FileCreationDirectory);

            // Show the status window
            StatusWindow statusWindow = new StatusWindow(fileWriter);
            statusWindow.Show();

            KeyListener keyListener = new(fileWriter, config, statusWindow);

            LowLevelKeyboardHook kbh = new();
            kbh.OnKeyPressed += keyListener.OnKeyPressed;
            kbh.HookKeyboard();

            Application.Run();

            kbh.UnHookKeyboard();
        } catch (Exception e)
        {
            MessageBox.Show($"An error occurred: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static void ReloadConfig(KeyListener keyListener, StatusWindow statusWindow)
    {
        GlobalKeyListenerConfig config = LoadConfig();
        keyListener.UpdateConfig(config);
    }

    private static GlobalKeyListenerConfig LoadConfig()
    {
        string jsonFilePath = "./appsettings.json";
        string jsonContent = File.ReadAllText(jsonFilePath);

        return JsonSerializer.Deserialize<GlobalKeyListenerConfig>(jsonContent)!;
    }
}
