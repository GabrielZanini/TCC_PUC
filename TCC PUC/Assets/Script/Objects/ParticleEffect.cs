using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    public ParticleSystem particle;
    private float lifeTime = 3f;
    public float time = 0f;

    TimeBody timebody;
    ParticleSystem[] particles;
    bool[] isRandom;


    private void Awake()
    {
        timebody = GetComponent<TimeBody>();
        particles = GetComponentsInChildren<ParticleSystem>();
        isRandom = new bool[particles.Length];

        for (int i = 0; i < particles.Length; i++)
        {
            isRandom[i] = particles[i].useAutoRandomSeed;
        }
    }

    private void OnEnable()
    {
        lifeTime = particle.main.duration;
        time = 0f;
        particle.Play(true);
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
        if (!TimeController.Instance.IsRewinding)
        {
            if (time < lifeTime)
            {
                time += Time.deltaTime;
            }
            else
            {
                timebody.Despawn();
            }
        }        
    }


    void AddListeners()
    {
        TimeController.Instance.OnStopRewind.AddListener(EnableParticle);
        timebody.OnDisactivate.AddListener(EnableParticle);
    }

    void RemoveListeners()
    {
        TimeController.Instance.OnStopRewind.RemoveListener(EnableParticle);
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
        //Debug.Log(gameObject.name + " - EnableParticle - " + timebody.isActive.ToString());
        
        //for (int i = 0; i < particles.Length; i++)
        //{
        //    particles[i].Stop(false);
        //    particles[i].useAutoRandomSeed = isRandom[i];
        //}

        if (timebody.isActive)
        {

            particle.Play(true);
        }
        else
        {
            particle.Simulate(lifeTime);
            particle.Stop(true);
        }
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
