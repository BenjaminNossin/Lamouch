using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Mode : MonoBehaviour
{
    public static bool withScan;

    public void ActiteScan()
    {
        withScan = true;
        Debug.Log("Scan Activer");
    }

    public void DesactiveScan()
    {
        withScan = false;
        Debug.Log("Scan désactiver");
    }
}
