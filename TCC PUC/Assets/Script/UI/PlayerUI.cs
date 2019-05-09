using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    public StatusShip statusPlayer;

    public Slider timeBar;
    public Slider lifeBar;

    private void Start()
    {
        timeBar.maxValue = TimeController.Instance.maxTimeRewind;
        lifeBar.maxValue = statusPlayer.maxHp;
    }

    void FixedUpdate()
    {
        timeBar.value = TimeController.Instance.rewindCounter;
        lifeBar.value = statusPlayer.currentHp;
    }
}
