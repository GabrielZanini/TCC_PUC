using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
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
        GameManager.Instance.Score.OnChange.AddListener(UpdateScore);
    }

    void RemoveListener()
    {
        GameManager.Instance.Score.OnChange.RemoveListener(UpdateScore);
    }



    void UpdateScore()
    {
        score.text = GameManager.Instance.Score.Current.ToString();
    }
}
