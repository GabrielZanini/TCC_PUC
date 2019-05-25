using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollStarts : ScrollTiles
{

    protected override void MoveToBottom()
    {
        base.MoveToBottom();
        Vector3 newPosition = transform.localPosition;
        newPosition.z -= 1;
        transform.localPosition = newPosition;
    }

}
