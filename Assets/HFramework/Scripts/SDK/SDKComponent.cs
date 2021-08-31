using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace FrameworkExtension
{
	public class SDKComponent : GameFrameworkComponent
    {
        [SerializeField]
        private string m_SDKHelperTypeName = "FrameworkExtension.DefaultSDKHelper";
        [SerializeField]
        private SDKHelperBase m_CustomSDKHelper = null;

        private bool mIsNotch = false;
        private float mNotchHeight = 0f;
        private SDKHelperBase sdk;
        private bool isCN;
        private bool logined;
        private void Start()
        {
#if UNITY_EDITOR
            m_SDKHelperTypeName = "FrameworkExtension.DefaultSDKHelper";
#endif
            sdk = Helper.CreateHelper(m_SDKHelperTypeName, m_CustomSDKHelper);
            if (sdk == null)
            {
                Log.Error("Can not create sdk helper.");
                return;
            }
            sdk.name = "SDK Helper";
            Transform transform = sdk.transform;
            transform.SetParent(this.transform);
            transform.localScale = Vector3.one;
            isCN = (RegionInfo.CurrentRegion.Name == "CN");
            Log.Info("Region: " + RegionInfo.CurrentRegion.Name);
        }

        public void Init()
        {
            sdk.Init();
        }

        public bool IsCN
        {
            get
            {
                return isCN;
            }
        }

        public bool Logined
        {
            get
            {
                return logined;
            }
        }

        public void GetNotch()
        {
            this.sdk.GetNotch();
        }

        public bool AdsReady(string name, AdsType adsType)
        {
            return this.sdk.AdsIsReady(name, adsType);
        }

        public void ShowAds(string name, AdsType adsType, Action<string, bool> callback, int index = 0)
        {
            this.sdk.ShowAds(name, adsType, callback, index);
        }

        public void ShowBanner(string name) {
            this.sdk.ShowBanner(name);
        }

        public void HideBanner(string ads) {
            this.sdk.HideBanner(ads);
        }

        public void LoadAds(string name, AdsType adsType) {
            this.sdk.LoadAds(name, adsType);
        }

        public void DestroyAds(string name, AdsType adsType)
        {
            this.sdk.DestroyAds(name, adsType);
        }

        public void SetNotch(RectTransform rectTransform) {
            if (IsNotch)
            {
                Vector2 anchorMax = new Vector2(1f, (Screen.height - Game.SDK.NotchHeight) / (float)Screen.height);
                rectTransform.anchorMax = anchorMax;
            }
        }

        public void SetNotchPosition(RectTransform rectTransform, float height) {
            if(IsNotch) {
                Vector2 pos = new Vector2(0f, -NotchHeight * height / Screen.height);
                rectTransform.anchoredPosition = pos;
            }
        }

        public void GamePause()
        {
            this.sdk.GamePause();
        }

        public void GameResume()
        {
            this.sdk.GameResume();
        }

        #region callback
        public void NotchInfo(string info)
        {
            Log.Info(info);
            string[] arg = info.Split(new char[] { '|' });
            if (arg[0] == "true" || arg[0] == "True")
            {
                mIsNotch = true;
                mNotchHeight = int.Parse(arg[1]);
            }
        }

        public void LoginState(string info)
        {
            if (info == "True")
            {
                Log.Info("login state true");
                logined = true;
            }
        }

        public void LogoutSuc(string info)
        {
            logined = false;
            Game.Event.Fire(this, LogoutEventArgs.Create());
        }

        public void LoginSuc(string info)
        {
            logined = true;
            Game.Event.Fire(this, LoginEventArgs.Create(true, info));
        }

        public void LoginFail(string info)
        {
            Game.Event.Fire(this, LoginEventArgs.Create(false, info));
        }
        #endregion

        public bool IsNotch
        {
            get
            {
                return mIsNotch;
            }
        }

        public float NotchHeight
        {
            get
            {
                return mNotchHeight;
            }
        }

        public bool ShowRate()
        {
            return sdk.IsShowRateForm();
        }

        public void GoRatePage()
        {
            sdk.GoRatePage();
        }

        public void FailLevel(string level)
        {
            sdk.FailLevel(level);
        }

        public void FinishLevel(string level)
        {
            sdk.FinishLevel(level);
        }

        public void StartLevel(string level)
        {
            sdk.StartLevel(level);
        }

        public void Event(string id)
        {
            sdk.Event(id);
        }

        public void EventLabel(string id, string label)
        {
            sdk.EventLabel(id, label);
        }

        public void BuyProduct(string id, Action<string, bool> callback)
        {
            sdk.BuyProduct(id, callback);
        }

        public void PlayerLevel(int level)
        {
            sdk.PlayerLevel(level);
        }

        public void Login()
        {
            sdk.Login();
        }

        public void Logout()
        {
            sdk.Logout();
        }

        public void LoginSilent()
        {
            sdk.LoginSilent();
        }

        public string GetDeviceId()
        {
            return sdk.GetDeviceId();
        }
    }
}
