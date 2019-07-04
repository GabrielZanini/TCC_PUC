using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    [Header("Menu")]
    public GameObject menuPanel;

    [Header("Gameplay")]
    public GameObject gameplayPanel;

    [Header("Pause")]
    public GameObject pausePanel;

    [Header("Settings")]
    public GameObject settingsPanel;

    [Header("Store")]
    public GameObject storePanel;

    [Header("Gameover")]
    public GameObject gameoverPanel;

    [Header("Scores")]
    public GameObject scoresPanel;

    [Header("Victory")]
    public GameObject victoryPanel;


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
        GameManager.Instance.Level.OnMenu.AddListener(MainMenu);
        GameManager.Instance.Level.OnStart.AddListener(StartGame);
        GameManager.Instance.Level.OnPause.AddListener(PauseGame);
        GameManager.Instance.Level.OnContinue.AddListener(ContinueGame);
        GameManager.Instance.Level.OnStop.AddListener(StopGame);
    }

    void RemoveListener()
    {
        GameManager.Instance.Level.OnMenu.AddListener(MainMenu);
        GameManager.Instance.Level.OnStart.RemoveListener(StartGame);
        GameManager.Instance.Level.OnPause.RemoveListener(PauseGame);
        GameManager.Instance.Level.OnContinue.RemoveListener(ContinueGame);
        GameManager.Instance.Level.OnStop.RemoveListener(StopGame);
    }

    

    void MainMenu()
    {
        ClearUI();
        menuPanel.SetActive(true);
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

    public void StoreGame()
    {
        ClearUI();
        storePanel.SetActive(true);
    }

    public void VictoryGame()
    {
        ClearUI();
        victoryPanel.SetActive(true);
    }


    public void SettingsBack()
    {
        if (GameManager.Instance.Level.State == LevelState.Paused)
        {
            PauseGame();
        }
        else if (GameManager.Instance.Level.State == LevelState.Stoped)
        {
            GameOver();
        }
        else if (GameManager.Instance.Level.State == LevelState.Menu)
        {
            MainMenu();
        }
    }



    public void ClearUI()
    {
        menuPanel.SetActive(false);
        gameplayPanel.SetActive(false);
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        storePanel.SetActive(false);
        gameoverPanel.SetActive(false);
        scoresPanel.SetActive(false);
        victoryPanel.SetActive(false);
    }


    IEnumerator ShowgameGameover()
    {
        ClearUI();

        yield return new WaitForSeconds(1);

        GameOver();
    }
}
