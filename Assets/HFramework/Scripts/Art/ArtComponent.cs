using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using TMPro;
using UnityEngine.U2D;
using System;

namespace FrameworkExtension
{
	public class ArtComponent : GameFrameworkComponent
    {
        public TMP_FontAsset TMPAsset { get; private set; }
        private Dictionary<string, SpriteAtlas> nameAtlas = new Dictionary<string, SpriteAtlas>();
        private Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();
        private Dictionary<string, Material> materials = new Dictionary<string, Material>();

        private void Start()
        {
            SpriteAtlasManager.atlasRequested += OnAtlasRequested;
        }
       
        public void SetFontAsset(TMP_FontAsset tMP_FontAsset)
        {
            TMPAsset = tMP_FontAsset;
        }

        private void OnAtlasRequested(string tag, Action<SpriteAtlas> action)
        {
            Log.Info("requested atlas {0}", tag);
            if (nameAtlas.ContainsKey(tag))
            {
                action(nameAtlas[tag]);
            }
        }

        public void AddAtlas(string name, SpriteAtlas spriteAtlas)
        {
            nameAtlas[name] = spriteAtlas;
        }

        public Sprite GetAtlasSprite(string atlasName, string spriteName)
        {
            if (nameAtlas.ContainsKey(atlasName))
            {
                return nameAtlas[atlasName].GetSprite(spriteName);
            }
            return null;
        }

        public void GetSprite(string path, Action<Sprite> callback)
        {
            if (Sprites.ContainsKey(path))
            {
                callback?.Invoke(Sprites[path]);
            }
            else
            {
                LoadUISprite(path, callback);
            }
        }

        public void LoadUISprite(string path, Action<Sprite> callback = null)
        {
            Game.Resource.LoadAsset(AssetUtility.GetSpriteAsset(path), typeof(Sprite), Constant.AssetPriority.UISpritesAsset, new GameFramework.Resource.LoadAssetCallbacks(
                (assetName, asset, duration, data) =>
                {
                    if (asset.GetType() == typeof(Texture2D))
                    {
                        Texture2D texture = (Texture2D)asset;
                        Sprites[path] = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                        callback?.Invoke(Sprites[path]);
                    }
                    else if (asset.GetType() == typeof(Sprite))
                    {
                        Sprites[path] = (Sprite)asset;
                        callback?.Invoke(Sprites[path]);
                    }
                },
                (assetName, status, errorMessage, data) =>
                {
                    Log.Error("Can not load sprite '{0}' from '{1}' with error message '{2}'.", path, assetName, errorMessage);
                }));
        }

        public void SetMat(string name, Material mat)
        {
            materials[name] = mat;
        }

        public Material GetMat(string name)
        {
            if (materials.ContainsKey(name))
            {
                return materials[name];
            }
            return null;
        }
    }
}
