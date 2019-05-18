﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public TimeBody timebody;
    public StatusBase status;

    private void Start()
    {
        status.OnDeath.AddListener(Die);
        GameManager.Instance.Level.OnRestart.AddListener(Revive);
    }

    private void OnDestroy()
    {
        status.OnDeath.RemoveListener(Die);
        GameManager.Instance.Level.OnRestart.RemoveListener(Revive);
    }

    void Die()
    {
        timebody.SetActive(false);
        GameManager.Instance.Level.Stop();
    }

    void Revive()
    {
        timebody.SetActive(true);
    }
}
