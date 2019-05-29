using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPool : ObjectPool
{
    [Header("PowerUps Settings")]
    public List<int> powerUpsIds = new List<int>();
    public List<PowerUpSpawningSetting> spawningSettings = new List<PowerUpSpawningSetting>();

    [Header("Spawning Settings")]
    public int averageBreak = 5;
    public int randomModifier = 1;
    public int counter = 0;
    public int othersCount = 1;



    protected override void Start()
    {
        base.Start();
        SetNextSpawn();
        SetupPowerUps();
    }
    

    protected override void AddListeners()
    {
        base.AddListeners();
        Manager.GameManager.Level.OnStart.AddListener(SetupPowerUps);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        Manager.GameManager.Level.OnStart.RemoveListener(SetupPowerUps);
    }
    


    public void TrySpawn(Vector3 position)
    {
        if (powerUpsIds.Count > 0)
        {
            if (counter <= 0)
            {
                var timebody = Spawn(position);
                SetRandomPowerUp(timebody.GetComponent<PowerUp>());
                SetNextSpawn();
            }
            else
            {
                counter--;
            }
        }
    }

    void SetNextSpawn()
    {
        counter = averageBreak + Random.Range(-randomModifier, randomModifier + 1);
    }
    
    void SetupPowerUps()
    {
        powerUpsIds.Clear();

        for (int i=0; i< spawningSettings.Count; i++)
        {
            if (spawningSettings[i].max > 0)
            {
                for (int j = 0; j < spawningSettings[i].max; j++)
                {
                    powerUpsIds.Add(i);
                }
            }
            else
            {
                for (int j = 0; j < othersCount; j++)
                {
                    powerUpsIds.Add(i);
                }
            }
            
        }
    }

    void SetRandomPowerUp(PowerUp powerUp)
    {
        int i = powerUpsIds[Random.Range(0, powerUpsIds.Count)];

        powerUp.type = spawningSettings[i].type;
        powerUp.SetColor(spawningSettings[i].color);
        powerUp.SetSprite(spawningSettings[i].sprite);
    }

    public void RemovePowerUpByType(PowerUpType type)
    {
        for (int i = 0; i < spawningSettings.Count; i++)
        {
            if (spawningSettings[i].type == type)
            {
                if (spawningSettings[i].max > 0)
                {
                    powerUpsIds.Remove(i);
                }
                
                break;
            }
        }
    }
}
