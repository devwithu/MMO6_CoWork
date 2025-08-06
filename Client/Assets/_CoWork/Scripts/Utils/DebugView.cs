using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class DebugView : MonoBehaviour
{
    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern void OutputDebugString(string message);
    void Start() 
    {
        OutputDebugString("started...");
    }
}
