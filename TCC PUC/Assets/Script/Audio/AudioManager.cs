using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Enviroment")]
    public AudioClip background;

    [Header("Objects")]
    public AudioClip bigExplosion;
    public AudioClip dustExplosion;
    public AudioClip bulletShoot;

    [Header("PickUps")]
    public AudioClip coin;
    public AudioClip powerUp;

    [Header("Source")]
    public AudioSource source;
}
