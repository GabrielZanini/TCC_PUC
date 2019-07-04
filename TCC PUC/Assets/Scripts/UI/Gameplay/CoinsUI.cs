using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsUI : MonoBehaviour
{
    public Text coins;
    public PlayerManager player;
    

    private void Update()
    {
        UpdateScore();
    }

    
    void UpdateScore()
    {
        coins.text = player.coins.ToString();
    }
}
