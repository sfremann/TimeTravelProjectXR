// --------------------------------------------------
//  file :      HandleInfoZone.cs
//  authors:    Sarah Fremann
//  date:       17/10/23
//  desc:       script handling info zones for a
//              given period.
// --------------------------------------------------

using System.Collections;
using UnityEngine;

// --------------------------------------------------
//  BEGINNING OF THE CLASS
// --------------------------------------------------

/// <summary>
/// Script handling info zones for a given period
/// </summary>
public class HandleInfoZone : MonoBehaviour
{
    // --------------------------------------------------
    //  Attributes Declaration
    // --------------------------------------------------

    // serialized variables
    [SerializeField] private GameObject glowingCircleWaiting = null;
    [SerializeField] private GameObject glowingCircleFound = null;
    [SerializeField] private GameObject glowingCircleFinished = null;

    // private variables
    private SaturationController _saturationController = null;
    private AudioSource _audioSource;
    private AudioSource _introAudioSource;

    private bool _pause = false;

    private Coroutine _waitCoroutine;

    // --------------------------------------------------
    //  Private methods
    // --------------------------------------------------

    /// <summary>
    /// Get audio source and set up color circles
    /// </summary>
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _introAudioSource = transform.parent.parent.GetComponent<AudioSource>();

        glowingCircleWaiting.SetActive(true);
        glowingCircleFound.SetActive(false);
        glowingCircleFinished.SetActive(false);
    }

    // --------------------------------------------------

    /// <summary>
    /// Handle the zone and its audio source when the player enters the zone
    /// </summary>
    /// <param name="other">player's collider</param>
    private void OnTriggerEnter(Collider other)
    {
        _pause = false;
        if (!_audioSource.isPlaying)
        {
            // Play the audio
            _waitCoroutine = StartCoroutine(WaitEndOfIntro());

            // If first time in the zone
            if (glowingCircleWaiting.activeInHierarchy)
            {
                // Change Circle Color
                glowingCircleWaiting.SetActive(false);
                glowingCircleFound.SetActive(true);

                // Increment color
                _saturationController.UpdateTransition();
            }
        }
    }

    // --------------------------------------------------

    /// <summary>
    /// Handle the zone and its audio source when the player exits the zone
    /// </summary>
    /// <param name="other">player's collider</param>
    private void OnTriggerExit(Collider other)
    {
        _pause = true;
        if (_audioSource.isPlaying) _audioSource.Pause();
        else if (!_introAudioSource.isPlaying)
        {
            // Change Circle Color
            glowingCircleWaiting.SetActive(false);
            glowingCircleFound.SetActive(false);
            glowingCircleFinished.SetActive(true);

            // Disable collider and trigger zone
            GetComponent<Collider>().enabled = false;
        }
    }

    // --------------------------------------------------

    /// <summary>
    /// Play the audio source
    /// </summary>
    private void PlayAudioSource()
    {
        // Check for conflicts with room's introduction audio
        if (_waitCoroutine != null) StopCoroutine(_waitCoroutine);
        if (!_pause) _audioSource.Play();
    }

    // --------------------------------------------------
    // Coroutines
    // --------------------------------------------------

    /// <summary>
    /// Wait the end of the room's introduction audio if it is still playing
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitEndOfIntro()
    {
        while (_introAudioSource.isPlaying) yield return null;
        PlayAudioSource();
        yield return null;
    }

    // --------------------------------------------------
    // Public methods
    // --------------------------------------------------

    /// <summary>
    /// Assign [_saturationController] value
    /// </summary>
    /// <param name="saturationController">new [_saturationController] value</param>
    public void AssignSaturationController(SaturationController saturationController)
    {
        _saturationController = saturationController;
    }
}

// --------------------------------------------------
//  END OF THE FILE
// --------------------------------------------------