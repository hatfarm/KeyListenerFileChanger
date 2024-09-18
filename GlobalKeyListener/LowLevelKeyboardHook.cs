// <copyright file="LowLevelKeyboardHook.cs" company="PlaceholderCompany">
// Licensed under the MIT License. See the LICENSE file for more details.
// </copyright>

namespace GlobalKeyListener;

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/// <summary>
/// This file is MOSTLY lifted from this source: https://stackoverflow.com/a/46014022/2616890 .
/// </summary>
#pragma warning disable SA1310 // Field names should not contain underscore
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1201 // Elements should be declared in the correct order
public class LowLevelKeyboardHook
{
    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;
    private const int WM_SYSKEYDOWN = 0x0104;
    private const int WM_KEYUP = 0x101;
    private const int WM_SYSKEYUP = 0x105;

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern nint SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, nint hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(nint hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern nint CallNextHookEx(nint hhk, int nCode, nint wParam, nint lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern nint GetModuleHandle(string lpModuleName);

    public delegate nint LowLevelKeyboardProc(int nCode, nint wParam, nint lParam);

    /// <summary>
    /// The events to call when key is pressed.
    /// </summary>
    public event EventHandler<Keys>? OnKeyPressed;

    /// <summary>
    /// The events to call when key is unpressed.
    /// </summary>
    public event EventHandler<Keys>? OnKeyUnpressed;

    private LowLevelKeyboardProc proc;
    private nint hookID = nint.Zero;

    public LowLevelKeyboardHook()
    {
        this.proc = this.HookCallback;
    }

    public void HookKeyboard()
    {
        this.hookID = this.SetHook(this.proc);
    }

    public void UnHookKeyboard()
    {
        UnhookWindowsHookEx(this.hookID);
    }

    private nint SetHook(LowLevelKeyboardProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule!.ModuleName), 0);
        }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
    }

    private nint HookCallback(int nCode, nint wParam, nint lParam)
    {
        if ((nCode >= 0 && wParam == WM_KEYDOWN) || wParam == WM_SYSKEYDOWN)
        {
            int vkCode = Marshal.ReadInt32(lParam);

            this.OnKeyPressed?.Invoke(this, (Keys)vkCode);
        }
        else if ((nCode >= 0 && wParam == WM_KEYUP) || wParam == WM_SYSKEYUP)
        {
            int vkCode = Marshal.ReadInt32(lParam);

            this.OnKeyUnpressed?.Invoke(this, (Keys)vkCode);
        }

        return CallNextHookEx(this.hookID, nCode, wParam, lParam);
    }
}
#pragma warning restore SA1310 // Field names should not contain underscore