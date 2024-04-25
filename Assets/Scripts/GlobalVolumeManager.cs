// --------------------------------------------------
//  file :      GlobalVolumeManager.cs
//  authors:    Sarah Fremann
//  date:       17/10/23
//  desc:       script handling transitions, audio
//              and visual effects.
// --------------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// --------------------------------------------------
//  BEGINNING OF THE CLASS
// --------------------------------------------------

/// <summary>
/// Manager handling transitions, audio and visual effects
/// </summary>
public class GlobalVolumeManager : MonoBehaviour
{
    // --------------------------------------------------
    //  Attributes Declaration
    // --------------------------------------------------

    // serialized variables
    [SerializeField] private float blurUITransitionTime = 3f;
    [SerializeField] private float pauseTime = 5f;

    // private variables
    private DepthOfField _depthOfField;
    private PeriodManager _periodManager;
    private AudioSource _transitionSound;

    private Coroutine _transitionInOutBlur;

    // const variables
    private const float _DOF_INIT = 5f;
    private const float _SOUND_VOLUME_MAX = 0.5f;
    private const float _SOUND_VOLUME_MIN = 0f;

    // --------------------------------------------------
    //  Private methods
    // --------------------------------------------------

    /// <summary>
    /// Get managers, volume profile and audio source
    /// </summary>
    private void Start()
    {
        // Get Depth Of Field feature and reset it
        GetComponent<Volume>().profile.TryGet<DepthOfField>(out _depthOfField);
        ResetDepthOfField();

        _periodManager = transform.parent.Find("PeriodManager").GetComponent<PeriodManager>();

        _transitionSound = GetComponent<AudioSource>();
    }

    // --------------------------------------------------

    /// <summary>
    /// Reset Depth Of Field values
    /// </summary>
    private void ResetDepthOfField()
    {
        _depthOfField.active = false;
        _depthOfField.focusDistance.value = _DOF_INIT;
    }

    // --------------------------------------------------

    /// <summary>
    /// Stop the blur transition
    /// </summary>
    /// <param name="intoBlur">true ---> make the screen blurry</param>
    private void StopTransitionInOutBlur(bool intoBlur)
    {
        StopCoroutine(_transitionInOutBlur);

        if (intoBlur) _transitionInOutBlur = StartCoroutine(TransitionBlurPause());
        else 
        {
            // Reset camera settings
            ResetDepthOfField();

            // Stop sound
            _transitionSound.Stop();
        }
    }

    // --------------------------------------------------

    /// <summary>
    /// Stop the pause phase of the blur transition
    /// </summary>
    private void StopTransitionBlurPause()
    {
        StopCoroutine(_transitionInOutBlur);

        // Second part of transition
        _transitionInOutBlur = StartCoroutine(TransitionInOutBlur(false)); 
    }

    // --------------------------------------------------
    //  Coroutines
    // --------------------------------------------------

    /// <summary>
    /// Transition between periods
    /// </summary>
    /// <param name="intoBlur">true ---> make the screen blurry</param>
    /// <returns></returns>
    private IEnumerator TransitionInOutBlur(bool intoBlur)
    {
        // Enable features
        // --- Depth of Field
        _depthOfField.active = true;
        float dofVal = -_DOF_INIT / blurUITransitionTime;

        // --- Sound
        float volumeVal = _SOUND_VOLUME_MAX / blurUITransitionTime;
        if (intoBlur) 
        {
            _transitionSound.Play();
            _transitionSound.volume = _SOUND_VOLUME_MIN;
        }

        float coeff;

        if (intoBlur)
        { 
            // Diminish depth of field
            while (_depthOfField.focusDistance.value > _depthOfField.focusDistance.min)
            {
                coeff = Time.deltaTime;
                _depthOfField.focusDistance.value += dofVal * coeff;
                _transitionSound.volume += volumeVal * coeff;
                yield return null;
            }
        }
        else
        {
            // Reestablish camera settings
            dofVal *= (-1);
            volumeVal *= (-1);
            while (_depthOfField.focusDistance.value < _DOF_INIT)
            {
                coeff = Time.deltaTime;
                _depthOfField.focusDistance.value += dofVal * coeff;
                _transitionSound.volume += volumeVal * coeff;
                yield return null;
            }
        }

        StopTransitionInOutBlur(intoBlur);
        yield return null;
    }

    // --------------------------------------------------

    /// <summary>
    /// Pause the transition
    /// </summary>
    /// <returns></returns>
    private IEnumerator TransitionBlurPause()
    {
        float goalTime = Time.timeSinceLevelLoad + pauseTime;

        _transitionSound.volume = _SOUND_VOLUME_MAX;
        _periodManager.SwitchPeriodAssets();

        // Just wait
        while (Time.timeSinceLevelLoad < goalTime) yield return new WaitForSeconds(pauseTime);

        StopTransitionBlurPause();
        yield return null;
    }

    // --------------------------------------------------
    // Public methods
    // --------------------------------------------------

    /// <summary>
    /// Start the transition between periods
    /// </summary>
    /// <param name="intoBlur">true ---> make the screen blurry</param>
    public void StartTransitionInOutBlur(bool intoBlur)
    {
        _transitionInOutBlur = StartCoroutine(TransitionInOutBlur(intoBlur));
    }
}

// --------------------------------------------------
//  END OF THE FILE
// --------------------------------------------------