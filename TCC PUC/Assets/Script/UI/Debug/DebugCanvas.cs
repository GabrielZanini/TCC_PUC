using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCanvas : MonoBehaviour
{
    public Text fps;

    string text = "";

    private void Update()
    {
        text = "";
        text += "FPS: " + (1f / Time.deltaTime).ToString("0.0") + "\n";
        text += "Camera Resolution: " + Camera.main.scaledPixelWidth + "x" + Camera.main.scaledPixelHeight + "\n";
        text += "Screen Orientation: " + Screen.orientation.ToString() + "\n";
        text += "Screen Resolution: " + Screen.width + "x" + Screen.height + "\n";

        fps.text = text;
    }
}
