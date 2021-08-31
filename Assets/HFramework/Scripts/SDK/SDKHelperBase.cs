using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkExtension
{
    public abstract class SDKHelperBase : MonoBehaviour, ISDK
    {
        public abstract bool AdsIsReady(string ads, AdsType adsType);
        public abstract void GetNotch();
        public abstract void GoRatePage();
        public abstract void HideBanner(string ads);
        public abstract bool IsShowRateForm();
        public abstract void ShowAds(string ads, AdsType adsType, Action<string, bool> callback, int index);
        public abstract void ShowBanner(string ads);
        public abstract void GamePause();
        public abstract void GameResume();
        public abstract void LoadAds(string ads, AdsType adsType);
        public abstract void StartLevel(string level);
        public abstract void FinishLevel(string level);
        public abstract void FailLevel(string level);
        public abstract void DestroyAds(string ads, AdsType adsType);
        public abstract void BuyProduct(string id, Action<string, bool> callback);
        public abstract void Init();
        public abstract void Event(string id);
        public abstract void EventLabel(string id, string label);

        public abstract void PlayerLevel(int level);

        public abstract void Login();
        public abstract void LoginSilent();
        public abstract void Logout();
        public abstract string GetDeviceId();
    }
}
