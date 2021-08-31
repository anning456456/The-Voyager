using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkExtension
{
	public interface ISDK
	{
        void Init();
        void GetNotch();
        bool AdsIsReady(string ads, AdsType adsType);
        void LoadAds(string ads, AdsType adsType);
        void ShowAds(string ads, AdsType adsType, Action<string, bool> callback, int index);
        void DestroyAds(string ads, AdsType adsType);
        void ShowBanner(string ads);
        void HideBanner(string ads);
        bool IsShowRateForm();
        void GoRatePage();
        void GamePause();
        void GameResume();
        void StartLevel(string level);
        void FinishLevel(string level);
        void FailLevel(string level);

        void BuyProduct(string id, Action<string, bool> callback);

        void PlayerLevel(int level);

        void Login();

        void LoginSilent();
        void Logout();

        string GetDeviceId();
    }
}
