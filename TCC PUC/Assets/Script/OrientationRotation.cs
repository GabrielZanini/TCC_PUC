using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationRotation : MonoBehaviour
{
    [Header("Portrait")]
    public Vector3 portraitPosition;
    public Vector3 portraitRotation;
    [Header("Landscape Right")]
    public Vector3 landscapeRightPosition;
    public Vector3 landscapeRightRotation;
    [Header("Landscape Left")]
    public Vector3 landscapeLeftPosition;
    public Vector3 landscapeLeftRotation;

    void Start()
    {
        
    }

    void Update()
    {
        RecalculateCoordenates();
    }

    void RecalculateCoordenates()
    {
        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
                transform.localPosition = portraitPosition;
                transform.localRotation = Quaternion.Euler(portraitRotation);
                break;
            case ScreenOrientation.LandscapeRight:
                transform.localPosition = landscapeRightPosition;
                transform.localRotation = Quaternion.Euler(landscapeRightRotation);
                break;
            case ScreenOrientation.LandscapeLeft:
                transform.localPosition = landscapeLeftPosition;
                transform.localRotation = Quaternion.Euler(landscapeLeftRotation);
                break;
            default:
                transform.localPosition = portraitPosition;
                transform.localRotation = Quaternion.Euler(portraitRotation);
                break;
        }
    }
}
