using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGambiarra : MonoBehaviour
{
    public LevelManager level;
    public GameObject spawners;
    public EnemyManager Boss;
    public HeathShip BossHealth;
    public GameCanvas canvas;

    public float levelTimer = 100f;
    public float bossTimer = 3f;

    public bool bossDied = false;

    private void Awake()
    {
        level.OnStart.AddListener(Level);
        level.OnMenu.AddListener(Menu);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Boss.gameObject.activeInHierarchy)
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
        level.OnMenu.RemoveListener(Menu);
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


    void Menu()
    {
        spawners.SetActive(true);
        bossDied = false;
        Boss.gameObject.SetActive(true);
    }


    IEnumerator LevelFlow()
    {
        spawners.SetActive(true);
        bossDied = false;
        Boss.gameObject.SetActive(false);

        yield return new WaitForSeconds(levelTimer);

        spawners.SetActive(false);

        yield return new WaitForSeconds(bossTimer);

        Boss.gameObject.SetActive(true);
    }

    IEnumerator Victory()
    {
        Boss.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        canvas.VictoryGame();
        yield return new WaitForSeconds(3);
        canvas.ClearUI();
        yield return new WaitForSeconds(1);

        spawners.SetActive(true);
        level.Menu();
    }

}
