using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidUI : MonoBehaviour
{
    [SerializeField] TimeBody timebody;
    [SerializeField] StatusBase status;
    [SerializeField] Slider lifeBar;
     
    


    void Start()
    {
        lifeBar.maxValue = status.maxHp;

        status.OnLoseHp.AddListener(UpdateBar);
    }

    void OnDestroy()
    {
        status.OnLoseHp.RemoveListener(UpdateBar);
    }

    void OnEnable()
    {
        lifeBar.gameObject.SetActive(false);
    }
    



    void UpdateBar()
    {
        lifeBar.gameObject.SetActive(true);

        lifeBar.value = status.currentHp;
    }
}
