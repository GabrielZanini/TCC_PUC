using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHighestUI : MonoBehaviour
{
    public Text score;


    private void Start()
    {
        AddListener();
        UpdateScore();
    }

    private void OnDestroy()
    {
        RemoveListener();
    }



    // Listeners

    void AddListener()
    {
        GameManager.Instance.Level.Score.OnNewHighest.AddListener(UpdateScore);
    }

    void RemoveListener()
    {
        GameManager.Instance.Level.Score.OnNewHighest.RemoveListener(UpdateScore);
    }



    void UpdateScore()
    {
        score.text = GameManager.Instance.Level.Score.Highest.ToString();
    }
}
