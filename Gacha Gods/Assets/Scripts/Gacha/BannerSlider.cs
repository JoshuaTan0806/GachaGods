using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class BannerSlider : MonoBehaviour
{
    [SerializeField] float _snapThreshold;
    [SerializeField] float _speed;
    ScrollRect ScrollRect;
    RectTransform RectTransform;

    private void Awake()
    {
        ScrollRect = GetComponentInParent<ScrollRect>();
        RectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Clamp();

        if (ShouldSnap())
        {
            MoveToBanner(ClosestBanner());
        }

        BannerManager.CurrentBanner = BannerManager.Banners[ClosestBanner()];
        //if (ScrollRect.velocity.magnitude == 0)
        //    MoveToBanner(ClosestBanner());
    }

    void Clamp()
    {
        if (RectTransform.anchoredPosition.y < -BannerManager.Banners.Keys.First().anchoredPosition.y)
        {
            SnapToBanner(ClosestBanner());
            ScrollRect.velocity = Vector2.zero;
        }

        if (RectTransform.anchoredPosition.y > -BannerManager.Banners.Keys.Last().anchoredPosition.y)
        {
            SnapToBanner(ClosestBanner());
            ScrollRect.velocity = Vector2.zero;
        }
    }

    public void MoveToBanner(RectTransform banner)
    {
        BannerManager.CurrentBanner = BannerManager.Banners[banner];

        Vector3 destination = RectTransform.anchoredPosition;
        destination.y = -banner.anchoredPosition.y;
        RectTransform.anchoredPosition = Vector3.MoveTowards(RectTransform.anchoredPosition, destination, _speed * Time.deltaTime);
    }

    bool ShouldSnap()
    {
        return ScrollRect.velocity.magnitude > 0 && ScrollRect.velocity.magnitude < _snapThreshold;
    }

    public void SnapToBanner(RectTransform banner)
    {
        BannerManager.CurrentBanner = BannerManager.Banners[banner];

        Vector3 destination = RectTransform.anchoredPosition;
        destination.y = -banner.anchoredPosition.y;
        RectTransform.anchoredPosition = destination;
    }

    RectTransform ClosestBanner()
    {
        RectTransform Closest = BannerManager.Banners.Keys.First();

        float closestDist = Mathf.Infinity;

        foreach (var item in BannerManager.Banners)
        {
            float dist = Mathf.Abs(item.Key.anchoredPosition.y + RectTransform.anchoredPosition.y);

            if (dist < closestDist)
            {
                closestDist = dist;
                Closest = item.Key;
            }
        }

        return Closest;
    }
}
