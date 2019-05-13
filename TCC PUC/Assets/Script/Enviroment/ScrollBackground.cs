using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float scrollSpeed;
    public float tileSizeZ;
    public float counter = 0f;

    private Vector3 startPosition;


    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (TimeController.Instance.isRewinding)
        {
            return;
        }

        counter += Time.deltaTime;

        float newPosition = Mathf.Repeat(counter * scrollSpeed, tileSizeZ);
        transform.position = startPosition - Vector3.right * newPosition;
    }
}
