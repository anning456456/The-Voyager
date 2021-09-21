using GameFramework;
using GameFramework.DataTable;
using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using GameFramework.Resource;
using FrameworkExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using UnityEngine.UI;
using LoadDataTableFailureEventArgs = UnityGameFramework.Runtime.LoadDataTableFailureEventArgs;
using LoadDataTableSuccessEventArgs = UnityGameFramework.Runtime.LoadDataTableSuccessEventArgs;
using UnityEngine.U2D;

namespace Voyage
{
    public class ProcedurePreload : ProcedureBase
    {
        private Dictionary<string, bool> m_LoadedFlag = new Dictionary<string, bool>();

        private Image ProgressImage;
        private int MaxLoadNum;
        private bool mStartLoadScene;
        private bool mLoadSceneDone;

        public static readonly string[] DataTableNames = new string[]
        {
            "Music",
            "Scene",
            "Sound",
            "UIForm",
            "UISound",
            "Entity",
            "Dialogue",
            "Selection",
            "Level",
            "Task"
        };

        public static readonly string[] MatNames = new string[]
        {
            "UIGreyMat"
        };


      

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            Game.Event.Subscribe(LoadConfigSuccessEventArgs.EventId, OnLoadConfigSuccess);
            Game.Event.Subscribe(LoadConfigFailureEventArgs.EventId, OnLoadConfigFailure);
            Game.Event.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            Game.Event.Subscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
            Game.Event.Subscribe(LoadDictionarySuccessEventArgs.EventId, OnLoadDictionarySuccess);
            Game.Event.Subscribe(LoadDictionaryFailureEventArgs.EventId, OnLoadDictionaryFailure);
            Game.Event.Subscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            Game.Event.Subscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);

   
            mStartLoadScene = false;
            mLoadSceneDone = false;

            LoadGame();
        }

        private void LoadGame()
        {
            m_LoadedFlag.Clear();
            
            LoadConfig("DefaultConfig");
            // Preload data tables
            foreach (string dataTableName in DataTableNames)
            {
                LoadDataTable(dataTableName);
            }
            // Preload dictionaries
            //LoadDictionary("Default");

            // Preload fonts
            LoadFont("MainFont");
            //LoadFontAsset("MainFont SDF");

            //LoadAtlas("UISpriteAtlas");
            //LoadAtlas("GameSpriteAtlas");

            foreach (string mats in MatNames)
            {
                LoadMats(mats);
            }

            MaxLoadNum = m_LoadedFlag.Count;
            Log.Info(MaxLoadNum);
        }

        private void InitManager()
        {
            GameSetting.Init();
            Game.SDK.Init();
            GameMain.GameDataManager.Init();
            GameMain.GameManager.Init();
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (!mStartLoadScene)
            {
                int count = 0;
                foreach (bool suc in m_LoadedFlag.Values)
                {
                    if (suc)
                    {
                        count++;
                    }
                }
                float v = ((float)count / (float)MaxLoadNum) / 2f;

                if (count < MaxLoadNum)
                {
                    return;
                }
           
                InitManager();
                //LoadScene();
                mStartLoadScene = true;
                return;
            }

            if (mLoadSceneDone)
            {
                procedureOwner.SetData<VarBoolean>(GameConstant.ProcedureAppInit, true);
                ChangeState<ProcedureMain>(procedureOwner);
            }
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            Game.Event.Unsubscribe(LoadConfigSuccessEventArgs.EventId, OnLoadConfigSuccess);
            Game.Event.Unsubscribe(LoadConfigFailureEventArgs.EventId, OnLoadConfigFailure);
            Game.Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            Game.Event.Unsubscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
            Game.Event.Unsubscribe(LoadDictionarySuccessEventArgs.EventId, OnLoadDictionarySuccess);
            Game.Event.Unsubscribe(LoadDictionaryFailureEventArgs.EventId, OnLoadDictionaryFailure);
            Game.Event.Unsubscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            Game.Event.Unsubscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
            
        }

        private void LoadConfig(string configName)
        {
            string configAssetName = AssetUtility.GetConfigAsset(configName, false);
            m_LoadedFlag.Add(configAssetName, false);
            Game.Config.ReadData(configAssetName, Constant.AssetPriority.ConfigAsset, this);
        }

        private void LoadDataTable(string dataTableName)
        {
            string dataTableAssetName = AssetUtility.GetDataTableAsset(dataTableName, true);
            m_LoadedFlag.Add(dataTableAssetName, false);
            Game.DataTable.LoadDataTable(dataTableName, dataTableAssetName, this);
        }

        private void LoadDictionary(string dictionaryName)
        {
            string dictionaryAssetName = AssetUtility.GetDictionaryAsset(dictionaryName, false);
            m_LoadedFlag.Add(dictionaryAssetName, false);
            Game.Localization.ReadData(dictionaryAssetName, Constant.AssetPriority.DictionaryAsset, this);
        }

        private void LoadFont(string fontName)
        {
            m_LoadedFlag.Add(Utility.Text.Format("Font.{0}", fontName), false);
            Game.Resource.LoadAsset(AssetUtility.GetFontAsset(fontName), Constant.AssetPriority.FontAsset, new LoadAssetCallbacks(
                (assetName, asset, duration, userData) =>
                {
                    m_LoadedFlag[Utility.Text.Format("Font.{0}", fontName)] = true;
                    UGuiForm.SetMainFont((Font)asset);
                    Log.Info("Load font '{0}' OK.", fontName);
                },

                (assetName, status, errorMessage, userData) =>
                {
                    Log.Error("Can not load font '{0}' from '{1}' with error message '{2}'.", fontName, assetName, errorMessage);
                }));
        }

        private void LoadFontAsset(string fontName)
        {
            m_LoadedFlag.Add(Utility.Text.Format("FontSDF.{0}", fontName), false);
            Game.Resource.LoadAsset(AssetUtility.GetFontSDFAsset(fontName), Constant.AssetPriority.FontAsset, new LoadAssetCallbacks(
                (assetName, asset, duration, userData) =>
                {
                    m_LoadedFlag[Utility.Text.Format("FontSDF.{0}", fontName)] = true;
                    Game.Art.SetFontAsset(asset as TMPro.TMP_FontAsset);
                    Log.Info("Load font sdf '{0}' OK.", fontName);
                },

                (assetName, status, errorMessage, userData) =>
                {
                    Log.Error("Can not load font sdf '{0}' from '{1}' with error message '{2}'.", fontName, assetName, errorMessage);
                }));
        }

        private void LoadAtlas(string atlasName)
        {
            m_LoadedFlag.Add(Utility.Text.Format("Atlas.{0}", atlasName), false);
            Game.Resource.LoadAsset(AssetUtility.GetAtlasAsset(atlasName), Constant.AssetPriority.AtlasAsset, new LoadAssetCallbacks(
                (assetName, asset, duration, userData) =>
                {
                    m_LoadedFlag[Utility.Text.Format("Atlas.{0}", atlasName)] = true;
                    Game.Art.AddAtlas(atlasName, (SpriteAtlas)asset);
                    Log.Info("Load atlas '{0}' OK.", atlasName);
                },

                (assetName, status, errorMessage, userData) =>
                {
                    Log.Error("Can not load atlas '{0}' from '{1}' with error message '{2}'.", atlasName, assetName, errorMessage);
                }));
        }

        private void LoadMats(string matName)
        {
            m_LoadedFlag.Add(Utility.Text.Format("Mats.{0}", matName), false);
            Game.Resource.LoadAsset(AssetUtility.GetMaterialAsset(matName), Constant.AssetPriority.AtlasAsset, new LoadAssetCallbacks(
                (assetName, asset, duration, userData) =>
                {
                    m_LoadedFlag[Utility.Text.Format("Mats.{0}", matName)] = true;
                    Game.Art.SetMat(matName, (Material)asset);
                    Log.Info("Load mats '{0}' OK.", matName);
                },

                (assetName, status, errorMessage, userData) =>
                {
                    Log.Error("Can not load mat '{0}' from '{1}' with error message '{2}'.", matName, assetName, errorMessage);
                }));
        }

        private void LoadScene()
        {
            int sceneId = 1;
            IDataTable<DRScene> dtScene = Game.DataTable.GetDataTable<DRScene>();
            DRScene drScene = dtScene.GetDataRow(sceneId);
            if (drScene == null)
            {
                Log.Warning("Can not load scene '{0}' from data table.", sceneId.ToString());
                return;
            }
            Game.Scene.LoadScene(AssetUtility.GetSceneAsset(drScene.Name), Constant.AssetPriority.SceneAsset, this);
        }


       

        #region event
        private void OnLoadConfigSuccess(object sender, GameEventArgs e)
        {
            LoadConfigSuccessEventArgs ne = (LoadConfigSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            m_LoadedFlag[ne.ConfigAssetName] = true;
            Log.Info("Load config '{0}' OK.", ne.ConfigAssetName);
        }

        private void OnLoadConfigFailure(object sender, GameEventArgs e)
        {
            LoadConfigFailureEventArgs ne = (LoadConfigFailureEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }
            Log.Error("Can not load config '{0}' from '{1}' with error message '{2}'.", ne.ConfigAssetName, ne.ConfigAssetName, ne.ErrorMessage);
        }

        private void OnLoadDataTableSuccess(object sender, GameEventArgs e)
        {
            LoadDataTableSuccessEventArgs ne = (LoadDataTableSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }
            m_LoadedFlag[ne.DataTableAssetName] = true;
            Log.Info("Load data table '{0}' OK.", ne.DataTableAssetName);
        }

        private void OnLoadDataTableFailure(object sender, GameEventArgs e)
        {
            LoadDataTableFailureEventArgs ne = (LoadDataTableFailureEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }
            Log.Error("Can not load data table '{0}' from '{1}' with error message '{2}'.", ne.DataTableAssetName, ne.DataTableAssetName, ne.ErrorMessage);
        }

        private void OnLoadDictionarySuccess(object sender, GameEventArgs e)
        {
            LoadDictionarySuccessEventArgs ne = (LoadDictionarySuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }
            m_LoadedFlag[ne.DictionaryAssetName] = true;
            Log.Info("Load dictionary '{0}' OK.", ne.DictionaryAssetName);
        }

        private void OnLoadDictionaryFailure(object sender, GameEventArgs e)
        {
            LoadDictionaryFailureEventArgs ne = (LoadDictionaryFailureEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }
            Log.Error("Can not load dictionary '{0}' from '{1}' with error message '{2}'.", ne.DictionaryAssetName, ne.DictionaryAssetName, ne.ErrorMessage);
        }

        private void OnLoadSceneSuccess(object sender, GameEventArgs e)
        {
            LoadSceneSuccessEventArgs ne = (LoadSceneSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Info("Load scene '{0}' OK.", ne.SceneAssetName);
            mLoadSceneDone = true;
            //CameraUtility.FixedWidth(Camera.main, 9.1125f);
        }

        private void OnLoadSceneFailure(object sender, GameEventArgs e)
        {
            LoadSceneFailureEventArgs ne = (LoadSceneFailureEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Error("Load scene '{0}' failure, error message '{1}'.", ne.SceneAssetName, ne.ErrorMessage);
        }
        #endregion
    }
}
