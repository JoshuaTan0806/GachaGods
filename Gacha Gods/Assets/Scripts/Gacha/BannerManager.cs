using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BannerManager : MonoBehaviour
{
    List<Banner> Banners = new List<Banner>();
    Banner CurrentBanner
    {
        get
        {
            return currentBanner;
        }
        set
        {
            currentBanner = value;
            ChangeBanner();
        }
    }
    Banner currentBanner;

    [Header("Prefabs")]
    [SerializeField] GameObject BannerPrefab;

    private void Awake()
    {
        InitialiseBanners();
        CurrentBanner = Banners[0];
        ChangeBanner();
    }

    void InitialiseBanners()
    {
        for (int i = 0; i < System.Enum.GetNames(typeof(BannerType)).Length; i++)
        {
            Banner b = Instantiate(BannerPrefab, transform).GetComponent<Banner>();
            b.bannerType = (BannerType)(i);
            Banners.Add(b);
        }
    }

    void ChangeBanner()
    {
        for (int i = 0; i < Banners.Count; i++)
        {
            Banners[i].gameObject.SetActive(false);
        }

        CurrentBanner.gameObject.SetActive(true);
    }
}
