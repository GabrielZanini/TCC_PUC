using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceBodyUI : MonoBehaviour
{
    [SerializeField] StatusBase status;
    [SerializeField] Slider lifeBar;

    public bool hideLifeAtMax = true;
    public bool hideBeforeStart = false;


    void Start()
    {
        lifeBar.maxValue = status.MaxHp;

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
        status.OnChangeHp.AddListener(UpdateBar);
        GameManager.Instance.Level.OnBeforeStart.AddListener(UpdateBar);
        GameManager.Instance.Level.OnStart.AddListener(UpdateBar);
    }

    void RemoveListeners()
    {
        status.OnChangeHp.RemoveListener(UpdateBar);
        GameManager.Instance.Level.OnBeforeStart.RemoveListener(UpdateBar);
        GameManager.Instance.Level.OnStart.RemoveListener(UpdateBar);
    }



    void UpdateBar()
    {
        lifeBar.value = status.CurrentHp;

        bool hideMax = hideLifeAtMax && lifeBar.value == lifeBar.maxValue;
        bool hideStart = hideBeforeStart && GameManager.Instance.Level.State == LevelState.BeforeStart;

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
