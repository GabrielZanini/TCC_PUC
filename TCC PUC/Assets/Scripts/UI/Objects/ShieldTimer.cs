using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldTimer : MonoBehaviour
{
    public Text timer;
    public ShieldShip shield;



    private void Update()
    {
        if (shield.timer > 0f)
        {
            timer.text = shield.timer.ToString("00.00");

            if (shield.timer < 1)
            {
                timer.color = Color.red;
            }
            else
            {
                timer.color = Color.white;
            }
        }
        else
        {
            timer.text = "";
        }
    }

}
