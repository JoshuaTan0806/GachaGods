using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BannerManager : MonoBehaviour
{
    [SerializeField] List<Banner> Banners;
    [SerializeField] Button LeftArrow;
    [SerializeField] Button RightArrow;
    Banner CurrentBanner;

    private void Awake()
    {
        LeftArrow.onClick.AddListener(CycleLeft);   
        RightArrow.onClick.AddListener(CycleRight);
        CurrentBanner = Banners[0];
        ChangeBanner();
    }

    void CycleLeft()
    {
        if (BannerIndex() == 0)
            CurrentBanner = Banners[Banners.Count - 1];
        else
            CurrentBanner = Banners[BannerIndex() - 1];

        ChangeBanner();
    }

    void CycleRight()
    {
        if (BannerIndex() == Banners.Count - 1)
            CurrentBanner = Banners[0];
        else
            CurrentBanner = Banners[BannerIndex() + 1];

        ChangeBanner();
    }

    int BannerIndex()
    {
        for (int i = 0; i < Banners.Count; i++)
        {
            if (Banners[i] == CurrentBanner)
                return i;
        }

        throw new System.Exception("Current banner isn't in list of banners");
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
