using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Restart : MonoBehaviour
{
    public StatusBase player;


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            player.currentHp = player.maxHp;

            AsteroidPool.Instance.RecalculateSpawn();
        }
    }
}
