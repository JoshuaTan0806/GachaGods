using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextList : MonoBehaviour
{
    [SerializeField] float minY;
    [SerializeField] float maxY;

    public void AddSpace()
    {
        SpawnText("", 0);
    }

    public void SpawnText(string text, float fontSize, Color color = default, bool bold = false)
    {
        TextMeshProUGUI t = Instantiate(PrefabManager.StandardText, transform).GetComponent<TextMeshProUGUI>();
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
        TextMeshProUGUI t = Instantiate(PrefabManager.StandardText, transform).GetComponent<TextMeshProUGUI>();
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

    public void Clear()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
