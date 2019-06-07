using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceBodyUI : MonoBehaviour
{
    [SerializeField] HeathShip health;
    [SerializeField] Slider lifeBar;

    public bool hideLifeAtMax = true;
    public bool hideBeforeStart = false;


    void Start()
    {
        lifeBar.maxValue = health.MaxHp;

        AddListeners();

        UpdateBar();
    }

    void OnDestroy()
    {
        RemoveListeners();
    }

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            UpdateBar();
        }
    }


    // Listener

    void AddListeners()
    {
        health.OnChangeHp.AddListener(UpdateBar);
        GameManager.Instance.Level.OnMenu.AddListener(UpdateBar);
        GameManager.Instance.Level.OnStart.AddListener(UpdateBar);
    }

    void RemoveListeners()
    {
        health.OnChangeHp.RemoveListener(UpdateBar);
        GameManager.Instance.Level.OnMenu.RemoveListener(UpdateBar);
        GameManager.Instance.Level.OnStart.RemoveListener(UpdateBar);
    }



    void UpdateBar()
    {
        lifeBar.value = health.CurrentHp;

        bool hideMax = hideLifeAtMax && lifeBar.value == lifeBar.maxValue;
        bool hideStart = hideBeforeStart && GameManager.Instance.Level.State == LevelState.Menu;

        if (hideMax || hideStart)
        {
            lifeBar.gameObject.SetActive(false);
        }
        else
        {
            lifeBar.gameObject.SetActive(true);
        }
    }
}
