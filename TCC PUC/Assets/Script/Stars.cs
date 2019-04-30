using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public GameObject normalStarParticles;
    public GameObject reverseStarParticles;

    void Start()
    {
        TimeController.Instance.OnStartRewind.AddListener(StartRewind);
        TimeController.Instance.OnStopRewind.AddListener(StopRewind);
    }

    private void OnDestroy()
    {
        TimeController.Instance.OnStartRewind.RemoveListener(StartRewind);
        TimeController.Instance.OnStopRewind.RemoveListener(StopRewind);
    }


    void StartRewind()
    {
        normalStarParticles.SetActive(false);
        reverseStarParticles.SetActive(true);
    }

    void StopRewind()
    {
        normalStarParticles.SetActive(false);
        reverseStarParticles.SetActive(true);
    }
}
