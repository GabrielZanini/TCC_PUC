using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShipStyle : MonoBehaviour
{
    [Header("Player")]
    public PlayerManager player;

    [Header("Styles")]
    public List<ShipStyle> styles = new List<ShipStyle>();

    [Header("Selected")]
    public int styleId = 0;



    private void OnValidate()
    {
        ActiveStyle();
    }

    private void Start()
    {
        ActiveStyle();
    }


    void ActiveStyle()
    {
        ValidateId();
        player.SetStyle(styles[styleId]);
    }

    void ValidateId()
    {
        if (styleId > 0)
        {
            styleId = styleId % styles.Count;
        }
        else
        {
            while (styleId < 0)
            {
                styleId += styles.Count;
            }
        }
    }



    public void Next()
    {
        styleId += 1;
        ActiveStyle();
    }

    public void Previous()
    {
        styleId -= 1;
        ActiveStyle();
    }
}
