using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinGameplayUI : MonoBehaviour
{
    public RectTransform reference;
    
    void OnEnable()
    {
        Vector2 anchoredPosition = reference.anchoredPosition;
        Vector3 screenPosition = RectTransformUtility.WorldToScreenPoint(
            Camera.main, reference.TransformPoint(anchoredPosition)
        );

        // Convert screen position to world position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        transform.position = worldPosition;
    }
}
