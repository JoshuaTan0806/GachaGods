using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BannerOption : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => MoveToBanner());
    }

    void MoveToBanner()
    {
        GetComponentInParent<BannerSlider>().SetDestination(GetComponent<RectTransform>());
    }
}
