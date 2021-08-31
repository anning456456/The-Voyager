using System;
using System.Collections;
using System.Collections.Generic;
using FrameworkExtension;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityGameFramework.Runtime;

namespace FrameworkExtension
{
    public class AdsComponent : GameFrameworkComponent
    {

        public void ShowBanner() {
            StartCoroutine(ShowBannerWhenReady());
        }

        IEnumerator ShowBannerWhenReady()
        {
            while (!IsReady(AdsConstant.Banner, AdsType.BannerAds))
            {
                yield return new WaitForSeconds(1f);
            }
            Game.SDK.ShowBanner(AdsConstant.Banner);
        }

        public bool IsReady(string name, AdsType adsType) {
            return Game.SDK.AdsReady(name, adsType);
        }

        public void ShowAds(string name, AdsType adsType, Action<string, bool> callback, int index) {
            Game.SDK.ShowAds(name, adsType, callback, index);
        }

        public void LoadAds(string name, AdsType adsType) {
            Game.SDK.LoadAds(name, adsType);
        }

        public void HideBanner(string ads) {
            Game.SDK.HideBanner(ads);
        }

    }
}

