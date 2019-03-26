using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;

    TimeBody timeBody;

    void Start()
    {
        timeBody = GetComponent<TimeBody>();
    }

    void Update()
    {
        if (timeBody.isRewinding)
        {
            return;
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject, 10f);
    }
}
