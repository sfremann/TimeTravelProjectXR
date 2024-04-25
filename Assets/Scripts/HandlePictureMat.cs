// --------------------------------------------------
//  file :      HandlePictureMat.cs
//  authors:    Sarah Fremann
//  date:       17/10/23
//  desc:       script handling the picture's
//              material.
// --------------------------------------------------

using UnityEngine;

// --------------------------------------------------
//  BEGINNING OF THE CLASS
// --------------------------------------------------

/// <summary>
/// Manager handling the picture's material given the period
/// </summary>
public class HandlePictureMat : MonoBehaviour
{
    // --------------------------------------------------
    //  Attributes Declaration
    // --------------------------------------------------

    // serialized variables
    [SerializeField] private GameObject picture = null;
    [SerializeField] private Material[] pictureMatRVB;
    [SerializeField] private Material[] pictureMatBW;

    // private variables
    private int _currentPictureID = 0;
    private Renderer _pictureRenderer;

    // --------------------------------------------------
    //  Private methods
    // --------------------------------------------------

    /// <summary>
    /// Get picture's Renderer
    /// </summary>
    private void Start()
    {
        _pictureRenderer = picture.GetComponent<Renderer>();
    }

    // --------------------------------------------------

    /// <summary>
    /// Update the picture's material according to the current period
    /// </summary>
    private void UpdatePictureMaterial()
    {
        _pictureRenderer.materials[1].CopyPropertiesFromMaterial(pictureMatBW[_currentPictureID]);
    }

    // --------------------------------------------------
    
    /// <summary>
    /// Saturate the picture's material
    /// </summary>
    private void SaturatePictureMaterial()
    {
        _pictureRenderer.materials[1].CopyPropertiesFromMaterial(pictureMatRVB[_currentPictureID]);
    }

    // --------------------------------------------------
    // Public methods
    // --------------------------------------------------

    /// <summary>
    /// When finishing a period, saturate the picture's material and update the current period ID
    /// </summary>
    public void OnFinishingCurrentPeriod()
    {
        SaturatePictureMaterial();
        _currentPictureID++;
    }

    // --------------------------------------------------

    /// <summary>
    /// When exiting a period, update the picture's material
    /// </summary>
    public void OnExitingCurrentPeriod()
    {
        if (_currentPictureID < pictureMatBW.Length && _currentPictureID < pictureMatRVB.Length) UpdatePictureMaterial();
        else
        {
            // Reset Material and disable this script
            _pictureRenderer.materials[1].CopyPropertiesFromMaterial(pictureMatBW[0]);
            GetComponent<HandlePictureMat>().enabled = false;
        }
    }
}

// --------------------------------------------------
//  END OF THE FILE
// --------------------------------------------------