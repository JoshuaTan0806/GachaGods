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

    private void Awake()
    {
        ScrollRect = GetComponentInParent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        Clamp();

        if (ShouldSnap())
        {
            MoveToBanner(ClosestBanner());
        }

        if (ScrollRect.velocity.magnitude == 0)
            MoveToBanner(ClosestBanner());
    }

    void Clamp()
    {
        if (transform.position.y > -BannerManager.Banners.Keys.First().localPosition.y)
        {
            SnapToBanner(ClosestBanner());
            ScrollRect.velocity = Vector2.zero;
        }

        if (transform.position.y < -BannerManager.Banners.Keys.Last().localPosition.y)
        {
            SnapToBanner(ClosestBanner());
            ScrollRect.velocity = Vector2.zero;
        }
    }

    public void MoveToBanner(Transform banner)
    {
        BannerManager.CurrentBanner = BannerManager.Banners[banner];
        //transform.Translate(Vector3.up * (transform.position.y - banner.transform.position.y) * _speed * Time.deltaTime);
    }

    bool ShouldSnap()
    {
        return ScrollRect.velocity.magnitude > 0 && ScrollRect.velocity.magnitude < _snapThreshold;
    }

    public void SnapToBanner(Transform banner)
    {
        BannerManager.CurrentBanner = BannerManager.Banners[banner];
        //transform.Translate(Vector3.up * (transform.position.y - banner.transform.position.y));
    }

    Transform ClosestBanner()
    {
        Transform Closest = BannerManager.Banners.Keys.First();

        float closestDist = Mathf.Infinity;

        foreach (var item in BannerManager.Banners)
        {
            float dist = Vector3.Distance(item.Key.position, transform.position);

            if (dist < closestDist)
            {
                closestDist = dist;
                Closest = item.Key;
            }
        }

        return Closest;
    }
}
