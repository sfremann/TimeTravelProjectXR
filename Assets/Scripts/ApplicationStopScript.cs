// --------------------------------------------------
//  file :      ApplicationStopScript.cs
//  authors:    Sarah Fremann
//  date:       17/10/23
//  desc:       script handling application quit when
//              pressing the Escape key.
// --------------------------------------------------

using UnityEngine;

// --------------------------------------------------
//  BEGINNING OF THE CLASS
// --------------------------------------------------

/// <summary>
/// Script handling application quit when pressing the Escape key
/// </summary>
public class ApplicationStopScript : MonoBehaviour
{
    // --------------------------------------------------
    //  Private methods
    // --------------------------------------------------

    /// <summary>
    /// Quit application when pressing the Escape key
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}

// --------------------------------------------------
//  END OF THE FILE
// --------------------------------------------------