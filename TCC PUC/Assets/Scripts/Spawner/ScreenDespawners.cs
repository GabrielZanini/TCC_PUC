using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDespawners : MonoBehaviour
{
    [Header("Camera")]
    public CameraManager camerManager;

    [Header("Objects")]
    public Transform top;
    public Transform left;
    public Transform right;
    public Transform bottom;

    [Header("Margins")]
    public int margin = 0;
    public int topMargin = 0;
    public int leftMargin = 0;
    public int rightMargin = 0;
    public int bottomMargin = 0;



    void Awake()
    {
        AddListeners();
    }

    void Start()
    {
        SetPositionAndScale();
    }

    private void OnValidate()
    {
        SetPositionAndScale();
    }

    void OnDestroy()
    {
        RemoveListeners();
    }



    void AddListeners()
    {
        if (camerManager != null)
        {
            camerManager.OnChange.AddListener(SetPositionAndScale);
        }
    }

    void RemoveListeners()
    {
        if (camerManager != null)
        {
            camerManager.OnChange.RemoveListener(SetPositionAndScale);
        }
    }



    void SetPositionAndScale()
    {
        float topY = camerManager.verticalSize + margin + topMargin;
        float righX = camerManager.horizontalSize + margin + rightMargin;
        float leftX = camerManager.horizontalSize + margin + leftMargin;
        float bottomY = camerManager.verticalSize + margin + bottomMargin;

        float horizontalX = (rightMargin - leftMargin) / 2f;
        float verticalY = (topMargin - bottomMargin) / 2f;

        float horizontalSize = camerManager.width + (margin * 2 + leftMargin + rightMargin);
        float verticalSize = camerManager.height + (margin * 2 + topMargin + bottomMargin);

        top.localPosition = new Vector3(horizontalX, topY, 0f);
        top.localScale = new Vector3(horizontalSize, 2f, 2f);

        right.localPosition = new Vector3(righX, verticalY, 0);
        right.localScale = new Vector3(2f, verticalSize, 2f);

        left.localPosition = new Vector3(-leftX, verticalY, 0);
        left.localScale = new Vector3(2f,verticalSize, 2f);

        bottom.localPosition = new Vector3(horizontalX, -bottomY, 0);
        bottom.localScale = new Vector3(horizontalSize, 2f, 2f);
    }
}
