using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : ObjectPool
{
    [Header("Spawning Settings")]
    public int averageBreak = 5;
    public int randomModifier = 1;
    public int counter = 0;



    protected override void Start()
    {
        base.Start();
        SetNextSpawn();
    }


    protected override void AddListeners()
    {
        base.AddListeners();
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
    }



    public void TrySpawn(Vector3 position)
    {
        if (counter <= 0)
        {
            var timebody = Spawn(position);
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

}
