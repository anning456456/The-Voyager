using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkExtension
{
	public static class Constant
	{
        /// <summary>
        /// 设置
        /// </summary>
        public static class Setting
        {
            public const string Language = "Setting.Language";
            public const string QualityLevel = "Setting.QualityLevel";
            public const string SoundGroupMuted = "Setting.{0}Muted";
            public const string SoundGroupVolume = "Setting.{0}Volume";
            public const string MusicMuted = "Setting.MusicMuted";
            public const string MusicVolume = "Setting.MusicVolume";
            public const string SoundMuted = "Setting.SoundMuted";
            public const string SoundVolume = "Setting.SoundVolume";
            public const string UISoundMuted = "Setting.UISoundMuted";
            public const string UISoundVolume = "Setting.UISoundVolume";
        }

        /// <summary>
        /// 资源优先级，数值小的优先级高
        /// </summary>
        public static class AssetPriority
        {
            public const int ConfigAsset = 100;
            public const int DataTableAsset = 100;
            public const int DictionaryAsset = 100;
            public const int AtlasAsset = 50;
            public const int FontAsset = 50;
            public const int MaterialAsset = 30;
            public const int MusicAsset = 20;
            public const int SceneAsset = 0;
            public const int SoundAsset = 30;
            public const int UIFormAsset = 50;
            public const int UISoundAsset = 30;
            public const int UISpritesAsset = 30;
            public const int PrefabAsset = 40;
            public const int MapAssset = 20;
            public const int EntityAsset = 50;
        }

        public static class ProcedureData
        {
            public const string NextSceneId = "NextSceneId";
        }
    }
}
