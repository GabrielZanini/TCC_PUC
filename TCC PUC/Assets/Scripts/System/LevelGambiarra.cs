using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGambiarra : MonoBehaviour
{
    public LevelManager level;
    public GameObject spawners;
    public GameObject Boss;
    public HeathShip BossHealth;
    public GameCanvas canvas;

    public float levelTimer = 100f;
    public float bossTimer = 3f;

    private bool bossDied = false;

    private void Awake()
    {
        level.OnStart.AddListener(Level);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Boss.activeInHierarchy)
        {
            if (BossHealth.CurrentHp <= 0f && !bossDied)
            {
                bossDied = true;
                EndLevel();
            }
        }
    }

    private void OnDestroy()
    {
        level.OnStart.RemoveListener(Level);
    }

    private void Level()
    {
        StopAllCoroutines();
        StartCoroutine(LevelFlow());
    }

    private void EndLevel()
    {
        StopAllCoroutines();
        StartCoroutine(Victory());
    }


    IEnumerator LevelFlow()
    {
        bossDied = false;
        Boss.SetActive(false);

        yield return new WaitForSeconds(levelTimer);

        spawners.SetActive(false);

        yield return new WaitForSeconds(bossTimer);

        Boss.SetActive(true);
    }

    IEnumerator Victory()
    {
        yield return new WaitForSeconds(1);
        canvas.VictoryGame();
        yield return new WaitForSeconds(3);
        canvas.ClearUI();
        yield return new WaitForSeconds(1);

        spawners.SetActive(true);
        level.Menu();
    }

}
