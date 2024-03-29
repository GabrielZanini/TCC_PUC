﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeBody))]
public class ParticleEffect : MonoBehaviour
{
    public ParticleSystem particle;
    private float lifeTime = 0f;
    public float time = 0f;
    public bool loop = false;

    [SerializeField] TimeBody timebody;
    //ParticleSystem[] particles;
    //bool[] isRandom;


    private void Awake()
    {
        //particles = GetComponentsInChildren<ParticleSystem>();
        //isRandom = new bool[particles.Length];

        //for (int i = 0; i < particles.Length; i++)
        //{
        //    isRandom[i] = particles[i].useAutoRandomSeed;
        //}
    }

    private void Reset()
    {
        timebody = GetComponent<TimeBody>();
        timebody.scriptsToDisable.Add(this);
        particle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        if (lifeTime >= 0f)
        {
            lifeTime = particle.main.duration;
        }
        
        time = 0f;

        if (timebody.isActive)
        {
            particle.Play(true);
        }        
    }

    void Start()
    {
        AddListeners();
    }
    
    void Update()
    {
        CountTime();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }



    void CountTime()
    {
        if (!timebody.Controller.IsRewinding)
        {
            if (time < lifeTime)
            {
                time += Time.deltaTime;
            }
            else
            {
                if (loop)
                {
                    time = 0f;
                }
                else
                {
                    timebody.Despawn();
                }
                
            }
        }        
    }


    void AddListeners()
    {
        timebody.Controller.OnStartRewind.AddListener(Pause);
        timebody.Controller.OnStopRewind.AddListener(EnableParticle);
        timebody.OnActivate.AddListener(EnableParticle);
        timebody.OnDisactivate.AddListener(EnableParticle);
    }

    void RemoveListeners()
    {
        timebody.Controller.OnStartRewind.RemoveListener(Pause);
        timebody.Controller.OnStopRewind.RemoveListener(EnableParticle);
        timebody.OnActivate.RemoveListener(EnableParticle);
        timebody.OnDisactivate.RemoveListener(EnableParticle);
    }
    
    
    void DisableParticle()
    {
        //for (int i=0; i<particles.Length; i++)
        //{
        //    particles[i].Stop(false);
        //    particles[i].useAutoRandomSeed = false;
        //}
    }
    
    void EnableParticle()
    {
        if (timebody.isActive)
        {
            particle.Play(true);
        }
        else
        {
            particle.Clear();
            particle.Stop(true);
        }
    }

    void Pause()
    {
        particle.Pause(true);
    }

    void Stop()
    {
        particle.Stop(true);
    }


    public void Simulate(float t)
    {
        if (time != t)
        {
            time = t;
            particle.Simulate(time);
        }        
    }
}
