using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvas : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject rewindSlider;



    void Start()
    {
        GameManager.Instance.Level.OnStart.AddListener(ContinueGame);
        GameManager.Instance.Level.OnStop.AddListener(StopGame);
    }

    private void OnDestroy()
    {
        GameManager.Instance.Level.OnStart.RemoveListener(ContinueGame);
        GameManager.Instance.Level.OnStop.RemoveListener(StopGame);
    }



    void StopGame()
    {
        gameOver.SetActive(true);
        rewindSlider.SetActive(false);
    }

    void ContinueGame()
    {
        gameOver.SetActive(false);
        rewindSlider.SetActive(true);
    }
}
