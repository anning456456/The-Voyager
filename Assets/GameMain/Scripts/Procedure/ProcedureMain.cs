using GameFramework.DataTable;
using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using FrameworkExtension;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Voyage
{
	public class ProcedureMain : ProcedureBase
    {
        private Dictionary<string, bool> m_LoadedFlag = new Dictionary<string, bool>();
        private int MaxLoadNum;
        private bool appInit;

        private LogInForm mMainForm;
        private bool mGoGame = false;

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Game.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            appInit = procedureOwner.GetData<VarBoolean>(GameConstant.ProcedureAppInit).Value;
            if (appInit)
            {
                procedureOwner.SetData<VarBoolean>(GameConstant.ProcedureAppInit, false);
            }
            MaxLoadNum = m_LoadedFlag.Count;
            mGoGame = false;
            Game.UI.OpenUIForm(UIFormId.LogInForm, this);

            Log.Info(TimeUtility.currentTimeMillis);
            Log.Info(TimeUtility.currentTimeSecs);
            Log.Info(TimeUtility.MillisToDateTime(TimeUtility.currentTimeMillis));
            Log.Info(TimeUtility.SecsToDateTime(TimeUtility.currentTimeSecs));

            Log.Info(Screen.width);
            Log.Info(Screen.height);
            Log.Info(Screen.safeArea);
        }

        public void AddLoadedFlag(string value)
        {
            m_LoadedFlag.Add(value, false);
        }

        public void LoadedFlagDone(string value)
        {
            m_LoadedFlag[value] = true;
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }
            mMainForm = (LogInForm)ne.UIForm.Logic;
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            Game.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            Log.Info("main " + isShutdown);
            if (mMainForm != null)
            {
                mMainForm.Close(isShutdown);
                mMainForm = null;
            }
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (appInit)
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
                appInit = false;
                Inited();
            }

            if (mGoGame)
            {
                procedureOwner.SetData<VarInt32>(GameConstant.ProcedureNextScene, Game.Config.GetInt("Scene.Game"));
                procedureOwner.SetData<VarByte>(GameConstant.ProcedureGameMode, (byte)GameMode.Normal); 
                ChangeState<ProcedureChange>(procedureOwner);
            }
        }

        private void Inited()
        {
            IDataTable<DRScene> dtScene = Game.DataTable.GetDataTable<DRScene>();
            DRScene drScene = dtScene.GetDataRow(Game.Config.GetInt("Scene.Main"));
            int m_BackgroundMusicId = drScene.BgMusicId;
            if (m_BackgroundMusicId > 0)
            {
                Game.Sound.PlayMusic(m_BackgroundMusicId);
            }
        }

        public void StartGame()
        {
            mGoGame = true;
        }
    }
}
