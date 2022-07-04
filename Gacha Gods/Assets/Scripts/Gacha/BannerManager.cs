using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BannerManager : MonoBehaviour
{
    public static Dictionary<RectTransform, Banner> Banners = new Dictionary<RectTransform, Banner>();

    public static Banner CurrentBanner
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
    static Banner currentBanner;
   
    [Header("References")]
    [SerializeField] Transform BannerHolder;
    [SerializeField] Transform BannerOptionHolder;

    [Header("Prefabs")]
    [SerializeField] GameObject BannerPrefab;
    [SerializeField] GameObject BannerOptionPrefab;

    private void Awake()
    {
        InitialiseBanners();
        currentBanner = Banners.Values.First();
        ChangeBanner();
    }

    void InitialiseBanners()
    {
        Banners = new Dictionary<RectTransform, Banner>();

        SpawnBanner(BannerType.Regular);

        for (int i = 0; i < 3; i++)
        {
            SpawnBanner(BannerType.RateUp);
        }
    }

    void SpawnBanner(BannerType bannerType)
    {
        Banner b = Instantiate(BannerPrefab, BannerHolder).GetComponent<Banner>();
        b.bannerType = bannerType;
        RectTransform t = Instantiate(BannerOptionPrefab, BannerOptionHolder).GetComponent<RectTransform>();
        Banners.Add(t, b);
    }

    static void ChangeBanner()
    {
        foreach (var item in Banners)
        {
            item.Value.gameObject.SetActive(false);
        }

        CurrentBanner.gameObject.SetActive(true);
    }
}
