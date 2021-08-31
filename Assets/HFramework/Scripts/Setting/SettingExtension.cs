using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace FrameworkExtension
{
    public static class SettingExtension
    {
        public static long GetLong(this SettingComponent settingComponent, string settingName)
        {
            return PlayerPrefsX.GetLong(settingName);
        }

        public static long GetLong(this SettingComponent settingComponent, string settingName, long defaultValue)
        {
            return PlayerPrefsX.GetLong(settingName, defaultValue);
        }

        public static void SetLong(this SettingComponent settingComponent, string settingName, long value)
        {
            PlayerPrefsX.SetLong(settingName, value);
        }

        public static void RemoveLong(this SettingComponent settingComponent, string settingName)
        {
            PlayerPrefs.DeleteKey(settingName + "_lowBits");
            PlayerPrefs.DeleteKey(settingName + "_highBits");
        }

        public static void SetInitArray(this SettingComponent settingComponent, string settingName, int[] array) {
            PlayerPrefsX.SetIntArray(settingName, array);
        }

        public static int[] GetIntArray(this SettingComponent settingComponent, string settingName) {
            return PlayerPrefsX.GetIntArray(settingName);
        }

        public static int[] GetIntArray(this SettingComponent settingComponent, string settingName, int defaultValue, int defaultSize)
        {
            return PlayerPrefsX.GetIntArray(settingName, defaultValue, defaultSize);
        }
    }
}
