using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectMakeSquare : MonoBehaviour
{
    public bool copyHeight = true;



    RectTransform rectTransform;
    Rect newRect;



    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }



    void MakeItSquare()
    {
        if (rectTransform.rect.height != rectTransform.rect.width)
        {
            newRect = rectTransform.rect;

            if (copyHeight)
            {
                newRect.width = newRect.height;

            }
            else
            {

            }
        }
    }
}
