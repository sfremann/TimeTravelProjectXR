//  file :      SaturationController.cs
//  authors:    Sarah Fremann
//  date:       17/10/23
//  desc:       script handling saturation effects.
// --------------------------------------------------

using UnityEngine;
using UnityEngine.Rendering.Universal;

// --------------------------------------------------
//  BEGINNING OF THE CLASS
// --------------------------------------------------

/// <summary>
/// Manager in each period handling saturation effects
/// </summary>
public class SaturationController : MonoBehaviour
{
    // --------------------------------------------------
    //  Attributes Declaration
    // --------------------------------------------------

    // serialized variables
    [SerializeField] private UniversalRendererData rendererData = null;
    [SerializeField] private Material _blitMat = null;
    [SerializeField] private GameObject[] zones = null;

    // private variables
    private float _saturationDelta;
    private float _saturationVal;
    private int _finishedZones = 0;
    private PeriodManager _periodManager;
    private SaturationController _saturationController;

    // const
    private const float _VAL_SATURATE = 2f;
    private const float _VAL_DESATURATE = 0f;

    // --------------------------------------------------
    //  Private methods
    // --------------------------------------------------

    /// <summary>
    /// Find managers and zones, and set up saturation delta
    /// </summary>
    private void Start()
    {
        _saturationController = GetComponent<SaturationController>();
        _periodManager = GameObject.Find("GeneralRoom").transform.Find("Managers").Find("PeriodManager").GetComponent<PeriodManager>();
        _saturationDelta = (zones.Length > 0 ? 1 / (float)zones.Length : 1f) * (_VAL_SATURATE - _VAL_DESATURATE);
        _saturationVal = _VAL_DESATURATE;

        ResetTransition(_VAL_DESATURATE);

        if (zones.Length == 0) UpdateTransition();
        else
        {
            foreach (var zone in zones)
            {
                zone.GetComponent<HandleInfoZone>().AssignSaturationController(_saturationController);
            }
        }
    }

    // --------------------------------------------------

    /// <summary>
    /// Reset the transition
    /// </summary>
    /// <param name="saturationVal">value to pass the effect material</param>
    private void ResetTransition(float saturationVal)
    {
        rendererData.SetDirty();
        _blitMat.SetFloat("_BaseSaturation", saturationVal);
    }

    // --------------------------------------------------
    // Public methods
    // --------------------------------------------------

    /// <summary>
    /// Update the effect base the saturation delta
    /// </summary>
    public void UpdateTransition()
    {
        // Saturate
        _saturationVal += _saturationDelta;
        _blitMat.SetFloat("_BaseSaturation", _saturationVal);

        // Update number of finished zones
        _finishedZones++;
        if (_finishedZones >= zones.Length)
        {
            // We finished the current period
            ResetTransition(_VAL_SATURATE);
            _periodManager.FinishPeriod();

            // Disable controller
            _saturationController.enabled = false;
        }
    }    
}

// --------------------------------------------------
//  END OF THE FILE
// --------------------------------------------------