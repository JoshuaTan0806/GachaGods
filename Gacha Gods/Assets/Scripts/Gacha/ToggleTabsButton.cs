using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTabsButton : MonoBehaviour
{
    [SerializeField] GameObject GachaCanvas;
    [SerializeField] GameObject CharacterCanvas;

    private void Awake()
    {
        gameObject.AddListenerToButton(ToggleTabs);

        CharacterCanvas.SetActive(false);
        GachaCanvas.SetActive(true);
    }

    void ToggleTabs()
    {
        if(CharacterCanvas.activeInHierarchy)
        {
            CharacterCanvas.SetActive(false);
            GachaCanvas.SetActive(true);
        }
        else
        {
            GachaCanvas.SetActive(false);
            CharacterCanvas.SetActive(true);
        }
    }
}
