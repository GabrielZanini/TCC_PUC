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

    [Header("Gameover")]
    public GameObject gameoverPanel;


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
        startPanel.SetActive(true);
        gameplayPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameoverPanel.SetActive(false);
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

    void PauseGame()
    {
        startPanel.SetActive(false);
        gameplayPanel.SetActive(false);
        pausePanel.SetActive(true);
        gameoverPanel.SetActive(false);
    }

    void ContinueGame()
    {
        startPanel.SetActive(false);
        gameplayPanel.SetActive(true);
        pausePanel.SetActive(false);
        gameoverPanel.SetActive(false);
    }


    void ClearUI()
    {
        startPanel.SetActive(false);
        gameplayPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameoverPanel.SetActive(false);
    }


    IEnumerator ShowgameGameover()
    {
        ClearUI();

        yield return new WaitForSeconds(1);

        gameoverPanel.SetActive(true);
    }
}
