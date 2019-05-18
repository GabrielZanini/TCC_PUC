using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceBodyUI : MonoBehaviour
{
    [SerializeField] StatusBase status;
    [SerializeField] Slider lifeBar;

    public bool hideLifeAtMax = true;


    void Start()
    {
        lifeBar.maxValue = status.MaxHp;

        status.OnChangeHp.AddListener(UpdateBar);

        UpdateBar();
    }

    void OnDestroy()
    {
        status.OnChangeHp.RemoveListener(UpdateBar);
    }

    private void OnEnable()
    {
        UpdateBar();
    }



    void UpdateBar()
    {
        lifeBar.value = status.CurrentHp;


        if (hideLifeAtMax && lifeBar.value == lifeBar.maxValue)
        {
            lifeBar.gameObject.SetActive(false);
        }
        else
        {
            lifeBar.gameObject.SetActive(true);
        }
    }
}
