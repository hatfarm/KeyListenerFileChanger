// <copyright file="KeyListener.cs" company="PlaceholderCompany">
// Licensed under the MIT License. See the LICENSE file for more details.
// </copyright>

namespace GlobalKeyListener;

using GlobalKeyListener.Config;
using GlobalKeyListener.Window;
using GlobalKeyLister;
using System.Collections.Concurrent;
using System.IO;
using System.Text.Json;

/// <summary>
/// The class responsible for listening to key presses and connecting them to the action desired.
/// </summary>
public class KeyListener
{
    private readonly List<SequenceDetector> keySequenceDetectors = [];
    private readonly StatusWindow statusWindow;
    private ConcurrentDictionary<Keys, string> buttonToFilenameMap;
    private Keys[] fileCreationToggleCombo;
    private Keys[] appExitKeyCombo;

    private readonly FileWriter fileWriter;

    // We use this to toggle whether or not we want to create our files.
    // We'll use the magic combo to toggle this.
    private bool isFileCreatorListening = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyListener"/> class.
    /// </summary>
    /// <param name="fileWriter">The file writer we're going to use to create our files.</param>
    /// <param name="globalKeyListenerConfig">Mostly tells us what buttons do what.</param>
    /// <param name="statusWindow">The window that we will update with our key mappings.</param>
    public KeyListener(FileWriter fileWriter, GlobalKeyListenerConfig globalKeyListenerConfig, StatusWindow statusWindow)
    {
        this.fileWriter = fileWriter;
        this.statusWindow = statusWindow;
        statusWindow.TrySetNewKey = this.TryUpdateKey;
        statusWindow.TrySetNewKeyTarget = this.TryUpdateKeyTarget;
        statusWindow.TryUpdateFileCreationToggleCombo = this.TryUpdateFileCreationToggleCombo;
        statusWindow.TryUpdateAppExitKeyCombo = this.TryUpdateAppExitKeyCombo;
        statusWindow.WriteToJson = this.WriteToJson;

        this.UpdateConfig(globalKeyListenerConfig);
    }

    public void UpdateConfig(GlobalKeyListenerConfig newConfig)
    {
        this.buttonToFilenameMap = new ConcurrentDictionary<Keys, string>(newConfig.ButtonToFileMap);
        this.fileCreationToggleCombo = newConfig.FileCreationToggleCombo;
        this.appExitKeyCombo = newConfig.AppExitKeyCombo;

        this.keySequenceDetectors.Clear();
        this.keySequenceDetectors.Add(new SequenceDetector(newConfig.AppExitKeyCombo, Application.Exit));
        this.keySequenceDetectors.Add(new SequenceDetector(newConfig.FileCreationToggleCombo, this.ToggleListening));

        // Initialize the status window with button mappings
        this.statusWindow.SetButtonMappings(this.buttonToFilenameMap, newConfig.AppExitKeyCombo, newConfig.FileCreationToggleCombo);
    }

    /// <summary>
    /// Handles when a key has been pressed.
    /// </summary>
    /// <param name="sender">Optional, the sending object, generally you just ignore this.</param>
    /// <param name="keys">The keys being sent (seems to only ever be singular, but technically can be multiple keys).</param>
    public void OnKeyPressed(object? sender, Keys keys)
    {
        this.keySequenceDetectors.ForEach(detector => detector.OnKeyPressed(keys));

        if (!this.isFileCreatorListening)
        {
            // Below here is only file creation listeners, so we can return early if we're not listening.
            return;
        }

        foreach (KeyValuePair<Keys, string> buttonToFile in this.buttonToFilenameMap)
        {
            if (buttonToFile.Key == keys)
            {
                this.fileWriter.WriteFileWithNewContents(buttonToFile.Value);
            }
        }
    }

    private bool TryUpdateKey(Keys oldKey, Keys newKey)
    {
        if (this.buttonToFilenameMap.ContainsKey(newKey))
        {
            return false;
        }

        string target = this.buttonToFilenameMap[oldKey];
        this.buttonToFilenameMap.TryRemove(oldKey, out _);
        this.buttonToFilenameMap[newKey] = target;
        this.WriteToJson();
        Program.ReloadConfig(this, this.statusWindow);
        return true;
    }

    private bool TryUpdateKeyTarget(Keys key, string newTarget)
    {
        this.buttonToFilenameMap[key] = newTarget;
        this.WriteToJson();
        Program.ReloadConfig(this, this.statusWindow);
        return true;
    }

    private bool TryUpdateFileCreationToggleCombo(Keys[] newCombo)
    {
        this.fileCreationToggleCombo = newCombo;
        this.WriteToJson();
        Program.ReloadConfig(this, this.statusWindow);
        return true;
    }

    private bool TryUpdateAppExitKeyCombo(Keys[] newCombo)
    {
        this.appExitKeyCombo = newCombo;
        this.WriteToJson();
        Program.ReloadConfig(this, this.statusWindow);
        return true;
    }

    private void ToggleListening()
    {
        this.isFileCreatorListening = !this.isFileCreatorListening;
        this.statusWindow.UpdateListeningStatus(this.isFileCreatorListening);
    }

    private void WriteToJson()
    {
        // This is where we update the GlobalKeyListenerConfig with the new button mappings.
        // And then save it.
        var config = new GlobalKeyListenerConfig
        {
            FileCreationDirectory = this.fileWriter.FileSaveRoot,
            ButtonToFileMap = new Dictionary<Keys, string>(this.buttonToFilenameMap),
            AppExitKeyCombo = this.appExitKeyCombo,
            FileCreationToggleCombo = this.fileCreationToggleCombo,
        };

        string jsonContent = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("./appsettings.json", jsonContent);
    }
}
