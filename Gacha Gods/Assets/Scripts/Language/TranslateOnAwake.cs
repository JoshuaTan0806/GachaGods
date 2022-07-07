using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TranslateOnAwake : MonoBehaviour
{
    TextMeshProUGUI text;
    string originalText;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        originalText = text.text;
        Translate();
    }

    private void OnEnable()
    {
        LanguageManager.OnLanguageChanged += Translate;
    }

    private void OnDisable()
    {
        LanguageManager.OnLanguageChanged -= Translate;
    }

    void Translate()
    {
        text.text = LanguageManager.FindTranslation(originalText);
    }
}
