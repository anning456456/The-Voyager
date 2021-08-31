using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace FrameworkExtension
{
    public class DefaultSDKHelper : SDKHelperBase
    {
        public override bool AdsIsReady(string ads, AdsType adsType)
        {
            return true;
        }

        public override void BuyProduct(string id, Action<string, bool> callback)
        {
            StartCoroutine(WaitBuy(id, callback));
        }

        IEnumerator WaitBuy(string id, Action<string, bool> callback)
        {
            yield return new WaitForSeconds(3f);
            callback?.Invoke(id, true);
        }

        public override void DestroyAds(string ads, AdsType adsType)
        {
    
        }

        public override void FailLevel(string level)
        {

        }

        public override void FinishLevel(string level)
        {

        }

        public override void GamePause()
        {

        }

        public override void GameResume()
        {

        }

        public override void GetNotch()
        {
            Game.SDK.NotchInfo("true|135|0");
        }

        public override void GoRatePage()
        {
            Log.Info("show rate");
        }

        public override void HideBanner(string ads)
        {
            Log.Info("hide banner");
        }

        public override void Init()
        {
           
        }

        public override bool IsShowRateForm()
        {
            return true;
        }

        public override void LoadAds(string ads, AdsType adsType)
        {

        }

        public override void ShowAds(string ads, AdsType adsType, Action<string, bool> callback, int index)
        {
            Log.Info("show ads {0}", ads);
            callback?.Invoke(ads, true);
        }

        public override void ShowBanner(string ads)
        {
            Log.Info("show banner ads {0}", ads);
        }

        public override void StartLevel(string level)
        {

        }

        public override void Event(string id)
        {
            Log.Info("event {0}", id);
        }

        public override void EventLabel(string id, string label)
        {
            
        }

        public override void PlayerLevel(int level)
        {
            Log.Info("player level {0}", level);
        }

        public override void Login()
        {
            Game.SDK.LoginSuc("eyJhbGciOiJSUzI1NiIsImtpZCI6Ijg0NjJhNzFkYTRmNmQ2MTFmYzBmZWNmMGZjNGJhOWMzN2Q2NWU2Y2QiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2FjY291bnRzLmdvb2dsZS5jb20iLCJhenAiOiI3ODU0OTgxMzMwMTItZnBpYzQ1b2pjZDQwcGN2amtnMXVuamJkYmk4aHRycGQuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJhdWQiOiI3ODU0OTgxMzMwMTItamJtcW83YmsxcDFoZzBlZTF1a2dvZWsxNnVsYW84aDguYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJzdWIiOiIxMDEwNjQyNzUwNjk0NjUwODE3ODkiLCJlbWFpbCI6ImJhaWp1bi5oZUAxNjMuY29tIiwiZW1haWxfdmVyaWZpZWQiOnRydWUsIm5hbWUiOiLkvZXmn4_kv4oiLCJwaWN0dXJlIjoiaHR0cHM6Ly9saDQuZ29vZ2xldXNlcmNvbnRlbnQuY29tLy1WRzIzeTgzWnRuMC9BQUFBQUFBQUFBSS9BQUFBQUFBQUFBQS9BTVp1dWNrejVnVHJMeUd0aUxaaG5QLVNDUzNXeWxyNGtnL3M5Ni1jL3Bob3RvLmpwZyIsImdpdmVuX25hbWUiOiLmn4_kv4oiLCJmYW1pbHlfbmFtZSI6IuS9lSIsImxvY2FsZSI6InpoLUNOIiwiaWF0IjoxNjE1ODY1MjgyLCJleHAiOjE2MTU4Njg4ODJ9.D3V4-THHiZNA8dhgV1YJDFtxP-2S_Kjv1r6XmPdOI12d0fef_CW3qm2Bw2HYGW_q4zbZfPCKl7PBfu4uCgy6k3npV5WS7jW_ocJf8HiQdPvEI-jb9Yk_DBVUPIlKCjpA8q5zJp6GwVDReFesrSKy-pw-SuekvilmzJ4qLJeCjsF3-UDmM24RMlAsE9J3rJoyWO20wXndDtfeGPM5qRr24VJFWjU9V9ka3ryyEQAUqNI95nr6UF2kLDrBKjUoeoAl3jJwrJs0II8lEXDY9ki09WrjyAZbWKntNqH6jTX5TrqnzGoWpI15JHsTeOIEe_NVzmlQjKHarFGtjO3UA9Iu_w");
        }

        public override void Logout()
        {
            Game.SDK.Logout();
        }

        public override void LoginSilent()
        {
            
        }

        public override string GetDeviceId()
        {
            return SystemInfo.deviceUniqueIdentifier;
        }
    }
}
