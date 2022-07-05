using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextList : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TextPrefab;
    RectTransform RectTransform;
    ScrollRect ScrollRect;

    [SerializeField] float minY;
    [SerializeField] float maxY;

    private void Awake()
    {
        ScrollRect = GetComponentInParent<ScrollRect>();
        RectTransform = GetComponent<RectTransform>();
    }

    public void AddSpace()
    {
        SpawnText("", 0);
    }

    public void SpawnText(string text, float fontSize, Color color = default, bool bold = false)
    {
        TextMeshProUGUI t = Instantiate(TextPrefab, transform);
        t.SetText(text);
        t.gameObject.name = text;
        t.fontSize = fontSize;
        t.color = color;
        t.alpha = 1;
        if (bold)
            t.fontStyle = FontStyles.Bold;
    }

    public void SpawnText(string text, float fontSize, Gradient gradient, bool bold = false)
    {
        TextMeshProUGUI t = Instantiate(TextPrefab, transform);
        t.SetText(text);
        t.gameObject.name = text;
        t.fontSize = fontSize;
        t.enableVertexGradient = true;
        VertexGradient newGradient = new VertexGradient(gradient.colorKeys[0].color, gradient.colorKeys[1].color, gradient.colorKeys[1].color, gradient.colorKeys[1].color);
        t.colorGradient = newGradient;

        t.alpha = 1;

        if (bold)
            t.fontStyle = FontStyles.Bold;
    }

    private void Update()
    {
        Clamp();
    }

    void Clamp()
    {
        if(RectTransform.anchoredPosition.y > maxY)
        {
            ScrollRect.velocity = Vector2.zero;
            RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, maxY);
        }

        if (RectTransform.anchoredPosition.y < minY)
        {
            ScrollRect.velocity = Vector2.zero;
            RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, minY);
        }
    }
}
