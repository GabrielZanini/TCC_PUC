using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
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
    public Margin margins;



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
        float topY = camerManager.verticalSize + margins.all + margins.top;
        float righX = camerManager.horizontalSize + margins.all + margins.right;
        float leftX = camerManager.horizontalSize + margins.all + margins.left;
        float bottomY = camerManager.verticalSize + margins.all + margins.bottom;

        float horizontalX = (margins.right - margins.left) / 2f;
        float verticalY = (margins.top - margins.bottom) / 2f;

        float horizontalSize = camerManager.width + (margins.all * 2 + margins.left + margins.right);
        float verticalSize = camerManager.height + (margins.all * 2 + margins.top + margins.bottom);

        top.localPosition = new Vector3(horizontalX, topY, 0f);
        top.localScale = new Vector3(horizontalSize, 2f, 100f);

        right.localPosition = new Vector3(righX, verticalY, 0);
        right.localScale = new Vector3(2f, verticalSize, 100f);

        left.localPosition = new Vector3(-leftX, verticalY, 0);
        left.localScale = new Vector3(2f,verticalSize, 100f);

        bottom.localPosition = new Vector3(horizontalX, -bottomY, 0);
        bottom.localScale = new Vector3(horizontalSize, 2f, 100f);
    }
}
