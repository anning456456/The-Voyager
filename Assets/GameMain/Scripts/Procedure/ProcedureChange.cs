using GameFramework.DataTable;
using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using FrameworkExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Voyage
{
	public class ProcedureChange : ProcedureBase
    {
        private const int MainSceneId = 1;
        private const int GameSceneId = 2;

        private LoadForm loadForm;
        private int sceneId;

        private bool changeToGame;
        private int m_BackgroundMusicId = 0;
        private bool m_IsChangeSceneComplete = false;


        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            m_IsChangeSceneComplete = false;

            Game.Event.Subscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            Game.Event.Subscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
            Game.Event.Subscribe(LoadSceneUpdateEventArgs.EventId, OnLoadSceneUpdate);
            Game.Event.Subscribe(LoadSceneDependencyAssetEventArgs.EventId, OnLoadSceneDependencyAsset);
            Game.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            //sceneId = procedureOwner.GetData<VarInt32>(GameConstant.ProcedureNextScene).Value;
            //gameMode = (GameMode)procedureOwner.GetData<VarByte>(GameConstant.ProcedureGameMode).Value;
            //Log.Info(sceneId);
            //Log.Info(gameMode);

            Game.UI.OpenUIForm(UIFormId.LogInForm, this);
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }
            loadForm = (LoadForm)ne.UIForm.Logic;
        }

        public void StartLoad()
        {
            // 停止所有声音
            Game.Sound.StopAllLoadingSounds();
            Game.Sound.StopAllLoadedSounds();

            changeToGame = sceneId == GameSceneId;

            IDataTable<DRScene> dtScene = Game.DataTable.GetDataTable<DRScene>();
            DRScene drScene = null;
            if (changeToGame)
            {
                
                drScene = dtScene.GetDataRow(sceneId);
                if (drScene == null)
                {
                    Log.Warning("Can not load scene '{0}' from data table.", sceneId.ToString());
                    return;
                }
                string sceneAsset = AssetUtility.GetSceneAsset(drScene.Name);
                if (Game.Scene.SceneIsLoaded(sceneAsset))
                {
                    LoadSceneDone();
                }
                else
                {
                    Game.Scene.LoadScene(sceneAsset, Constant.AssetPriority.SceneAsset, this);
                }
                m_BackgroundMusicId = drScene.BgMusicId;
            }
            else
            {
                drScene = dtScene.GetDataRow(GameSceneId);
                if (drScene == null)
                {
                    Log.Warning("Can not load scene '{0}' from data table.", sceneId.ToString());
                    return;
                }
                drScene = dtScene.GetDataRow(sceneId);
                m_BackgroundMusicId = drScene.BgMusicId;
                LoadSceneDone();
            }
        }

        private void LoadSceneDone()
        {
            if (m_BackgroundMusicId > 0)
            {
                Game.Sound.PlayMusic(m_BackgroundMusicId);
            }

            m_IsChangeSceneComplete = true;
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            Game.Event.Unsubscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            Game.Event.Unsubscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
            Game.Event.Unsubscribe(LoadSceneUpdateEventArgs.EventId, OnLoadSceneUpdate);
            Game.Event.Unsubscribe(LoadSceneDependencyAssetEventArgs.EventId, OnLoadSceneDependencyAsset);
            Game.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            base.OnLeave(procedureOwner, isShutdown);
            if (loadForm != null)
            {
                //loadForm.LoadDone();
            }
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (!m_IsChangeSceneComplete)
            {
                return;
            }
            if (changeToGame)
            {
                ChangeState<ProcedureGame>(procedureOwner);
            }
            else
            {
                ChangeState<ProcedureMain>(procedureOwner);
            }
        }

        private void OnLoadSceneSuccess(object sender, GameEventArgs e)
        {
            LoadSceneSuccessEventArgs ne = (LoadSceneSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Info("Load scene '{0}' OK.", ne.SceneAssetName);
            LoadSceneDone();
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

        private void OnLoadSceneUpdate(object sender, GameEventArgs e)
        {
            LoadSceneUpdateEventArgs ne = (LoadSceneUpdateEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Info("Load scene '{0}' update, progress '{1}'.", ne.SceneAssetName, ne.Progress.ToString("P2"));
        }

        private void OnLoadSceneDependencyAsset(object sender, GameEventArgs e)
        {
            LoadSceneDependencyAssetEventArgs ne = (LoadSceneDependencyAssetEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Info("Load scene '{0}' dependency asset '{1}', count '{2}/{3}'.", ne.SceneAssetName, ne.DependencyAssetName, ne.LoadedCount.ToString(), ne.TotalCount.ToString());
        }
    }
}
