using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausecursor 
{
    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


    }
    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

}

