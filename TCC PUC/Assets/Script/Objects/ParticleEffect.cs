using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    public ParticleSystem particle;
    private float lifeTime = 0f;
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
        if (lifeTime >= 0f)
        {
            lifeTime = particle.main.duration;
        }
        
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
                //timebody.DestroyObject();
            }
        }        
    }


    void AddListeners()
    {
        TimeController.Instance.OnStopRewind.AddListener(EnableParticle);
        timebody.OnActivate.AddListener(EnableParticle);
        timebody.OnDisactivate.AddListener(EnableParticle);
    }

    void RemoveListeners()
    {
        TimeController.Instance.OnStopRewind.RemoveListener(EnableParticle);
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
        //Debug.Log(gameObject.name + " - EnableParticle - " + timebody.isActive.ToString());
        
        if (timebody.isActive)
        {
            particle.Play(true);
        }
        else
        {
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
