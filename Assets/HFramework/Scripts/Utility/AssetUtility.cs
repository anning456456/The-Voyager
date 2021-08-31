using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkExtension
{
    public static class AssetUtility
    {
        public static string GetConfigAsset(string assetName, bool fromBytes)
        {
            return Utility.Text.Format("Assets/GameMain/Configs/DefaultConfig/{0}.{1}", assetName, fromBytes ? "bytes" : "txt");
        }

        public static string GetDataTableAsset(string assetName, bool fromBytes)
        {
            return Utility.Text.Format("Assets/GameMain/DataTables/{0}.{1}", assetName, fromBytes ? "bytes" : "txt");
        }

        public static string GetDictionaryAsset(string assetName, bool fromBytes)
        {
            return Utility.Text.Format("Assets/GameMain/Localization/{0}/Dictionaries/{1}.{2}", Game.Localization.Language.ToString(), assetName, fromBytes ? "bytes" : "xml");
        }

        public static string GetFontAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Fonts/{0}/{1}.ttf", Game.Localization.Language.ToString(), assetName);
        }

        public static string GetFontSDFAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Fonts/{0}/{1}.asset", Game.Localization.Language.ToString(), assetName);
        }

        public static string GetSceneAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Scenes/{0}.unity", assetName);
        }

        public static string GetMusicAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Music/{0}.mp3", assetName);
        }

        public static string GetSoundAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Sounds/{0}.wav", assetName);
        }

        public static string GetUIFormAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/UI/UIForms/{0}.prefab", assetName);
        }

        public static string GetUISoundAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/UI/UISounds/{0}.wav", assetName);
        }

        public static string GetEntityAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Entities/{0}.prefab", assetName);
        }

        public static string GetMaterialAsset(string name)
        {
            return Utility.Text.Format("Assets/GameMain/Materials/{0}.mat", name);
        }

        public static string GetPrefabsAsset(string name)
        {
            return Utility.Text.Format("Assets/GameMain/Prefabs/{0}.prefab", name);
        }

        public static string GetAtlasAsset(string name)
        {
            return Utility.Text.Format("Assets/GameMain/Sprites/SpriteAtlas/{0}.spriteatlas", name);
        }

        public static string GetSpriteAsset(string name)
        {
            return Utility.Text.Format("Assets/GameMain/UI/UISprites/{0}.png", name);
        }

        public static string GetMapAsset(string name)
        {
            return Utility.Text.Format("Assets/GameMain/Map/{0}.asset", name);
        }
    }
}
