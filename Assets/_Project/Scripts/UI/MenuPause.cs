using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MenuState
{
    public static bool IsMenuShowing { get; private set; }
    public static bool IsOptionShowing { get; private set; }
    public static void MenuOpen()
    {
        IsMenuShowing = true;
    }
    public static void MenuClosed()
    {
        IsMenuShowing = false;
    }

    public static void OptionOpen()
    {
        IsOptionShowing = true;
    }
    public static void OptionClosed()
    {
        IsOptionShowing = false;
    }
}