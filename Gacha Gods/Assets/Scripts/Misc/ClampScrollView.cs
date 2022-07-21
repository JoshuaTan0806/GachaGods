using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampScrollView : MonoBehaviour
{
    [SerializeField] float minValue;
    [SerializeField] float maxValue;
    RectTransform RectTransform;
    ScrollRect ScrollRect;

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        ScrollRect = GetComponentInParent<ScrollRect>();
    }

    private void Start()
    {
        if (ScrollRect.vertical)
            RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, minValue);
        else
            RectTransform.anchoredPosition = new Vector2(maxValue, RectTransform.anchoredPosition.y);
    }

    private void Update()
    {
        if (ScrollRect.vertical)
            ClampVertical();
        else
            ClampHorizontal();
    }

    void ClampHorizontal()
    {
        if (RectTransform.anchoredPosition.x > maxValue)
        {
            ScrollRect.velocity = Vector2.zero;
            RectTransform.anchoredPosition = new Vector2(maxValue, RectTransform.anchoredPosition.y);
        }

        if (RectTransform.anchoredPosition.x < minValue)
        {
            ScrollRect.velocity = Vector2.zero;
            RectTransform.anchoredPosition = new Vector2(minValue, RectTransform.anchoredPosition.y);
        }
    }

    void ClampVertical()
    {
        if (RectTransform.anchoredPosition.y > maxValue)
        {
            ScrollRect.velocity = Vector2.zero;
            RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, maxValue);
        }

        if (RectTransform.anchoredPosition.y < minValue)
        {
            ScrollRect.velocity = Vector2.zero;
            RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, minValue);
        }
    }
}
