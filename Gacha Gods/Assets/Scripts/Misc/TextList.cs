using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextList : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TextPrefab;
    
    public void SpawnText(string text, float fontSize, Color color = default, bool bold = false)
    {
        TextMeshProUGUI t = Instantiate(TextPrefab, transform);
        t.SetText(text);
        t.fontSize = fontSize;
        t.color = color;
        t.alpha = 1;

        if (bold)
            t.fontStyle = FontStyles.Bold;
    }

    public void SpawnText(string text, float fontSize, Gradient gradient = default, bool bold = false)
    {
        TextMeshProUGUI t = Instantiate(TextPrefab, transform);
        t.SetText(text);
        t.fontSize = fontSize;
        t.enableVertexGradient = true;
        VertexGradient newGradient = new VertexGradient(gradient.colorKeys[0].color, gradient.colorKeys[1].color, gradient.colorKeys[1].color, gradient.colorKeys[1].color);
        t.colorGradient = newGradient;

        t.alpha = 1;

        if (bold)
            t.fontStyle = FontStyles.Bold;
    }
}