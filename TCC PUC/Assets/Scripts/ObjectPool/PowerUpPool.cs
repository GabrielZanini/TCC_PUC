using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPool : ObjectPool
{
    [Header("PowerUps Settings")]
    public List<PowerUpSpawningSetting> spawningSettings = new List<PowerUpSpawningSetting>();

    [Header("Spawning Settings")]
    public int averageBreak = 5;
    public int randomModifier = 1;
    public int counter = 0;



    protected override void Start()
    {
        base.Start();
        SetNextSpawn();
        SpawnPowerUps();
    }



    public void TrySpawn(Vector3 position)
    {
        if (counter <= 0)
        {
            SpawnRandom(position);
            SetNextSpawn();
        }
        else
        {
            counter--;
        }
    }

    void SetNextSpawn()
    {
        counter = averageBreak + Random.Range(-randomModifier, randomModifier + 1);
    }


    void SpawnPowerUps()
    {
        for (int i=0; i< spawningSettings.Count; i++)
        {
            for (int j=0; j<spawningSettings[i].weight; j++)
            {
                var timebody = CreateObject();
                var powerUp = timebody.gameObject.GetComponent<PowerUp>();

                powerUp.type = spawningSettings[i].type;
                powerUp.SetColor(spawningSettings[i].color);
            }
        }
    }
}
