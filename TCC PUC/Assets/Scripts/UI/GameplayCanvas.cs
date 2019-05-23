using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvas : MonoBehaviour
{
    [Header("Start")]
    public GameObject startPanel;

    [Header("Gameplay")]
    public GameObject gameplayPanel;

    [Header("Pause")]
    public GameObject pausePanel;

    [Header("Settings")]
    public GameObject settingsPanel;

    [Header("Gameover")]
    public GameObject gameoverPanel;

    [Header("Scores")]
    public GameObject scoresPanel;


    private void OnEnable()
    {
        AddListener();
    }

    private void OnDisable()
    {
        RemoveListener();
    }
    

    void AddListener()
    {
        GameManager.Instance.Level.OnBeforeStart.AddListener(BeforeStartGame);
        GameManager.Instance.Level.OnStart.AddListener(StartGame);
        GameManager.Instance.Level.OnPause.AddListener(PauseGame);
        GameManager.Instance.Level.OnContinue.AddListener(ContinueGame);
        GameManager.Instance.Level.OnStop.AddListener(StopGame);
    }

    void RemoveListener()
    {
        GameManager.Instance.Level.OnBeforeStart.RemoveListener(BeforeStartGame);
        GameManager.Instance.Level.OnStart.RemoveListener(StartGame);
        GameManager.Instance.Level.OnPause.RemoveListener(PauseGame);
        GameManager.Instance.Level.OnContinue.RemoveListener(ContinueGame);
        GameManager.Instance.Level.OnStop.RemoveListener(StopGame);
    }

    

    void BeforeStartGame()
    {
        ClearUI();
        startPanel.SetActive(true);
    }

    void StartGame()
    {
        ClearUI();        
        gameplayPanel.SetActive(true);
    }

    void StopGame()
    {
        StartCoroutine(ShowgameGameover());
    }

    void GameOver()
    {
        ClearUI();
        gameoverPanel.SetActive(true);
        scoresPanel.SetActive(true);
    }

    public void PauseGame()
    {
        ClearUI();
        pausePanel.SetActive(true);
        scoresPanel.SetActive(true);
    }

    void ContinueGame()
    {
        ClearUI();
        gameplayPanel.SetActive(true);
    }

    public void SettingsGame()
    {
        ClearUI();
        settingsPanel.SetActive(true);
    }

    public void SettingsBack()
    {
        if (GameManager.Instance.Level.State == LevelState.Playing)
        {
            PauseGame();
        }
        else
        {
            GameOver();
        }
    }



    void ClearUI()
    {
        startPanel.SetActive(false);
        gameplayPanel.SetActive(false);
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        gameoverPanel.SetActive(false);
        scoresPanel.SetActive(false);
    }


    IEnumerator ShowgameGameover()
    {
        ClearUI();

        yield return new WaitForSeconds(1);

        GameOver();
    }
}
