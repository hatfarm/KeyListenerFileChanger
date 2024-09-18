// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GlobalKeyLister;

using GlobalKeyListener;

/// <summary>
/// The main program class.
/// </summary>
public class Program
{
    /// <summary>
    /// The main function, this is where execution starts.
    /// </summary>
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // This is where you'd put the path to the folder that will hold your files.
        FileWriter fileWriter = new ("C:\\Users\\vossc\\OneDrive\\Desktop\\testingStuff");

        KeyListener keyListener = new (fileWriter, GetButtonToFileMap());

        LowLevelKeyboardHook kbh = new ();
        kbh.OnKeyPressed += keyListener.OnKeyPressed;
        kbh.OnKeyUnpressed += keyListener.OnKeyUnPressed;
        kbh.HookKeyboard();

        Application.Run();

        kbh.UnHookKeyboard();
    }

    /// <summary>
    /// Setup the button mapping to the files we'll save to.
    /// </summary>
    /// <returns>The dictionary of what we want.</returns>
    private static Dictionary<Keys, string> GetButtonToFileMap()
    {
        Dictionary<Keys, string> buttonToFileMap = new ()
        {
            [Keys.R] = "reloadaction.txt",
            [Keys.V] = "somethingelse.txt",
        };
        return buttonToFileMap;
    }
}