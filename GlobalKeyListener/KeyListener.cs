// <copyright file="KeyListener.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GlobalKeyListener;

using System.Collections.Concurrent;

/// <summary>
/// The class responsible for listening to key presses and connecting them to the action desired.
/// </summary>
internal class KeyListener
{
    private readonly IReadOnlyDictionary<Keys, string> buttonToFilenameMap;

#pragma warning disable SA1010 // Opening square brackets should be spaced correctly
    private readonly Keys[] appExitKeyCombo = [Keys.RControlKey, Keys.F3, Keys.Escape];

    // We'll need to trigger these keys in order to get the file creation to start.
    // I imagine we'll want to use Streamerbot to push these keys when the redeem is hit.
    private readonly Keys[] fileCreationToggleCombo = [Keys.RControlKey, Keys.F4, Keys.F7, Keys.RShiftKey];
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly

    private readonly FileWriter fileWriter;
    private readonly ConcurrentDictionary<Keys, bool> keysPressed = GetDefaultKeysPressed();

    // We use this to toggle whether or not we want to create our files.
    // We'll use the magic combo to toggle this.
    private bool isFileCreatorListening = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyListener"/> class.
    /// </summary>
    /// <param name="fileWriter">The file writer we're going to use to create our files.</param>
    /// <param name="buttonToFilenameMap">The mapping of the button that we're looking for, and the file we save to if the button is pressed.</param>
    public KeyListener(FileWriter fileWriter, IReadOnlyDictionary<Keys, string> buttonToFilenameMap)
    {
        this.fileWriter = fileWriter;
        this.buttonToFilenameMap = buttonToFilenameMap;
    }

    /// <summary>
    /// Handles when a key has been pressed.
    /// </summary>
    /// <param name="sender">Optional, the sending object, generally you just ignore this.</param>
    /// <param name="keys">The keys being sent (seems to only ever be singular, but technically can be multiple keys).</param>
    public void OnKeyPressed(object? sender, Keys keys)
    {
        this.keysPressed[keys] = true;

        this.ProcessKeyActions();
    }

    /// <summary>
    /// Handles when a key has been unpressed.
    /// </summary>
    /// <param name="sender">Optional, the sending object, generally you just ignore this.</param>
    /// <param name="keys">The keys being sent (seems to only ever be singular, but technically can be multiple keys).</param>
    public void OnKeyUnPressed(object? sender, Keys keys)
    {
        this.keysPressed[keys] = false;
    }

    private static ConcurrentDictionary<Keys, bool> GetDefaultKeysPressed()
    {
        ConcurrentDictionary<Keys, bool> keysPressed = new ConcurrentDictionary<Keys, bool>();

        foreach (Keys key in Enum.GetValues(typeof(Keys)))
        {
            keysPressed[key] = false;
        }

        return keysPressed;
    }

    private void ProcessKeyActions()
    {
        if (this.IsComboPressed(this.appExitKeyCombo))
        {
            Application.Exit();
        }

        if (this.IsComboPressed(this.fileCreationToggleCombo))
        {
            this.isFileCreatorListening = !this.isFileCreatorListening;
        }

        if (!this.isFileCreatorListening)
        {
            // Below here is only file creation listeners, so we can return early if we're not listening.
            return;
        }

        foreach (KeyValuePair<Keys, string> buttonToFile in this.buttonToFilenameMap)
        {
            if (this.keysPressed[buttonToFile.Key])
            {
                this.fileWriter.WriteFileWithNewContents(buttonToFile.Value);
            }
        }
    }

    private bool IsComboPressed(Keys[] keys)
    {
        return keys.All(key => this.keysPressed[key]);
    }
}
