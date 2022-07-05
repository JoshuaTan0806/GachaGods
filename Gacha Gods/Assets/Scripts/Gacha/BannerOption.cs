using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BannerOption : MonoBehaviour
{
    RectTransform RectTransform;

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();

        GetComponent<Button>().onClick.AddListener(() => MoveToBanner());

        GameManager.OnGameStart += RefreshName;
        GameManager.OnRoundEnd += RefreshName;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStart -= RefreshName;
        GameManager.OnRoundEnd -= RefreshName;
    }

    void RefreshName()
    {
        gameObject.name = BannerManager.Banners[RectTransform].name + " Option";
    }

    void MoveToBanner()
    {
        GetComponentInParent<BannerSlider>().SetDestination(RectTransform);
    }
}
