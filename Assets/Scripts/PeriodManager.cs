// --------------------------------------------------
//  file :      PeriodManager.cs
//  authors:    Sarah Fremann
//  date:       17/10/23
//  desc:       script handling periods.
// --------------------------------------------------

using UnityEngine;

// --------------------------------------------------
//  BEGINNING OF THE CLASS
// --------------------------------------------------

/// <summary>
/// Manager handling periods
/// </summary>
public class PeriodManager : MonoBehaviour
{
    // --------------------------------------------------
    //  Attributes Declaration
    // --------------------------------------------------

    // serialized variables
    [SerializeField] private HandlePictureMat handlePictureMat = null;
    [SerializeField] private GameObject[] periods;
    [SerializeField] private GameObject initPeriod;
    [SerializeField] private Collider pictureCollider = null;

    // private variables
    private GlobalVolumeManager _globalVolumeManager;
    private GameObject _currentPeriod;
    private int _currentPeriodID = -1;

    // --------------------------------------------------
    //  Private methods
    // --------------------------------------------------

    /// <summary>
    /// Set up the initial period and get GlobalVolumeManager
    /// </summary>
    private void Start()
    {
        _currentPeriod = initPeriod;
        _currentPeriod.SetActive(true);
        pictureCollider.enabled = false;

        _globalVolumeManager = transform.parent.Find("GlobalVolume").GetComponent<GlobalVolumeManager>();
    }

    // --------------------------------------------------
    // Public methods
    // --------------------------------------------------

    /// <summary>
    /// Turn off previous period's Assets and turn on next period's Asset
    /// </summary>
    public void SwitchPeriodAssets()
    {
        // Deactivate current period (we finished)
        _currentPeriod.SetActive(false);
        handlePictureMat.OnExitingCurrentPeriod();

        // Pass to next period
        _currentPeriodID++;
        if (_currentPeriodID < periods.Length) _currentPeriod = periods[_currentPeriodID];
        else _currentPeriod = initPeriod; // come back to start
        _currentPeriod.SetActive(true);

        // Play intro sound
        _currentPeriod.GetComponent<AudioSource>().enabled = true;
    }

    // --------------------------------------------------

    /// <summary>
    /// Start transition to change the period
    /// </summary>
    public void ChangePeriod()
    {
        // Start UI transition to display dates
        _globalVolumeManager.StartTransitionInOutBlur(true);
    }

    // --------------------------------------------------

    /// <summary>
    /// Send message to managers when finishing the current period
    /// </summary>
    public void FinishPeriod()
    {
        // Reactivate Picture Collider
        pictureCollider.enabled = true;

        // Update picture material to color
        handlePictureMat.OnFinishingCurrentPeriod();
    }
}

// --------------------------------------------------
//  END OF THE FILE
// --------------------------------------------------