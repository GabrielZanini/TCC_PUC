using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Backgrounds : MonoBehaviour
{
    [Header("Camera")]
    public CameraManager camerManager;

    [Header("Backgrond")]
    public List<ScrollBackground> backgronds = new List<ScrollBackground>();

    public int currentId = 0;



    void Reset()
    {
        GetBackground();
    }

    private void Awake()
    {
        AddListeners();
    }

    void Start()
    {
        GetBackground();
    }

    void OnValidate()
    {
        GetBackground();
    }

    void OnDestroy()
    {
        RemoveListeners();
    }
    

    void AddListeners()
    {
        if (camerManager != null)
        {
            camerManager.OnChange.AddListener(GetBackground);
        }
    }

    void RemoveListeners()
    {
        if (camerManager != null)
        {
            camerManager.OnChange.RemoveListener(GetBackground);
        }
    }



    void GetBackground()
    {
        var bg = GetComponentsInChildren<ScrollBackground>(true);

        backgronds.Clear();

        for (int i = 0; i < bg.Length; i++)
        {
            backgronds.Add(bg[i]);
        }

        SetCamera();
        ActiveBackground();
    }

    void SetCamera()
    {
        if (camerManager != null)
        {
            for (int i = 0; i < backgronds.Count; i++)
            {
                backgronds[i].camerManager = camerManager;
            }
        }
    }
    
    void DisableAll()
    {
        for (int i = 0; i < backgronds.Count; i++)
        {
            backgronds[i].gameObject.SetActive(false);
        }
    }

    void ActiveBackground()
    {
        DisableAll();
        ValidateId();
        backgronds[currentId].gameObject.SetActive(true);
    }

    void ValidateId()
    {
        if (currentId > 0)
        {
            currentId = currentId % backgronds.Count;
        }
        else
        {
            while (currentId < 0 )
            {
                currentId += backgronds.Count;
            }
        }
    }



    public void Next()
    {
        currentId += 1;
        ActiveBackground();
    }

    public void Previous()
    {
        currentId -= 1;
        ActiveBackground();
    }
}
