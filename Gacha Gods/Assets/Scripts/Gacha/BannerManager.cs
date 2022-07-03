using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BannerManager : MonoBehaviour
{
    public static Dictionary<Transform, Banner> Banners = new Dictionary<Transform, Banner>();

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
    [SerializeField] Transform BannerSliderHolder;

    [Header("Prefabs")]
    [SerializeField] GameObject BannerPrefab;
    [SerializeField] GameObject BannerSliderPrefab;

    private void Awake()
    {
        InitialiseBanners();
        currentBanner = Banners.Values.First();
        ChangeBanner();
    }

    void InitialiseBanners()
    {
        Banners.Clear();

        for (int i = 0; i < System.Enum.GetNames(typeof(BannerType)).Length; i++)
        {
            Banner b = Instantiate(BannerPrefab, BannerHolder).GetComponent<Banner>();
            b.bannerType = (BannerType)(i);

            Transform t = Instantiate(BannerSliderPrefab, BannerSliderHolder).transform;

            Banners.Add(t, b);
        }
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
