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

    [Header("Stars")]
    public List<ScrollStarts> stars = new List<ScrollStarts>();

    [Header("Selected")]
    public int backgroundId = 0;
    public int starsId = 0;



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
        var sts = GetComponentsInChildren<ScrollStarts>(true);

        backgronds.Clear();
        stars.Clear();

        for (int i = 0; i < bg.Length; i++)
        {
            backgronds.Add(bg[i]);
        }

        for (int i = 0; i < sts.Length; i++)
        {
            stars.Add(sts[i]);
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

            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].camerManager = camerManager;
            }
        }
    }
    
    void DisableAll()
    {
        for (int i = 0; i < backgronds.Count; i++)
        {
            backgronds[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < stars.Count; i++)
        {
            stars[i].gameObject.SetActive(false);
        }
    }

    void ActiveBackground()
    {
        DisableAll();
        ValidateBgId();
        backgronds[backgroundId].gameObject.SetActive(true);
        ActiveStars();
    }

    void ActiveStars()
    {
        if (!backgronds[backgroundId].hasStars)
        {
            ValidateStarsId();
            stars[starsId].gameObject.SetActive(true);
        }
    }

    void ValidateBgId()
    {
        if (backgroundId > 0)
        {
            backgroundId = backgroundId % backgronds.Count;
        }
        else
        {
            while (backgroundId < 0)
            {
                backgroundId += backgronds.Count;
            }
        }
    }

    void ValidateStarsId()
    {
        if (starsId > 0)
        {
            starsId = starsId % stars.Count;
        }
        else
        {
            while (starsId < 0)
            {
                starsId += stars.Count;
            }
        }
    }



    public void NextBG()
    {
        backgroundId += 1;
        ActiveBackground();
    }

    public void PreviousBG()
    {
        backgroundId -= 1;
        ActiveBackground();
    }


    public void NextStars()
    {
        backgroundId += 1;
        ActiveBackground();
    }

    public void PreviousStars()
    {
        backgroundId -= 1;
        ActiveBackground();
    }
}
