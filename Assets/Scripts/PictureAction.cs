//  file :      PictureAction.cs
//  authors:    Sarah Fremann
//  date:       17/10/23
//  desc:       script handling the interaction with
//              the picture.
// --------------------------------------------------

using UnityEngine;

// --------------------------------------------------
//  BEGINNING OF THE CLASS
// --------------------------------------------------

/// <summary>
/// script handling the interaction with the picture
/// </summary>
public class PictureAction : MonoBehaviour
{
    // --------------------------------------------------
    //  Attributes Declaration
    // --------------------------------------------------

    // serialized variables
    [SerializeField] private PeriodManager periodManager = null;
    [SerializeField] private GameObject tutoZone = null;

    // private variables
    private bool _disableTutoZone = true;

    // --------------------------------------------------
    //  Private methods
    // --------------------------------------------------

    /// <summary>
    /// Handle interaction with the picture when finishing a period
    /// </summary>
    /// <param name="other">player's collider</param>
    private void OnTriggerEnter(Collider other)
    {
        // Deactivate trigger and change period
        GetComponent<Collider>().enabled = false;
        periodManager.ChangePeriod();

        // Deactivate tutorial zone
        if (_disableTutoZone)
        {
            tutoZone.SetActive(false);
            _disableTutoZone = false;
        }
    }
}

// --------------------------------------------------
//  END OF THE FILE
// --------------------------------------------------