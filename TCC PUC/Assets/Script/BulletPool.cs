using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }

    public List<GameObject> bullets = new List<GameObject>();
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    public GameObject SpawnBullet(Vector3 position, Quaternion rotation)
    {
        if (bullets.Count == 0)
        {
            return null;
        }

        var bullet = bullets[0];
        bullets.RemoveAt(0);

        bullet.transform.position = position;
        bullet.transform.rotation = rotation;

        bullet.GetComponent<TimeBody>().SetActive(true); 

        return bullet;
    }

    public void DespawnBullet(GameObject bullet)
    {
        bullet.GetComponent<TimeBody>().SetActive(false);

        bullets.Add(bullet);
    }



}
