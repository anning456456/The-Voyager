using FrameworkExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Voyage
{
	public static class GameSetting
	{
        private const string UserDataKey = "UserData";
        public static UserData CurUserData { get; private set; }

        public static void Init()
        {
           
            CurUserData = Game.Setting.GetObject(UserDataKey, new UserData());
        }

      

        public static void SaveUser(bool save = false)
        {
            Game.Setting.SetObject(UserDataKey, CurUserData);
            if (save)
                Game.Setting.Save();
        }


    }
}
