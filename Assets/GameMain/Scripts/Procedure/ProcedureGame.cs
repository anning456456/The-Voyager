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
	public class ProcedureGame : ProcedureBase
    {
        private GameForm mGameForm;
        private bool mGoMain = false;

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Game.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            mGoMain = false;
            Game.UI.OpenUIForm(UIFormId.SelectForm, this);
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            Game.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            base.OnLeave(procedureOwner, isShutdown);
            if (mGameForm != null)
            {
                mGameForm.Close(isShutdown);
                mGameForm = null;
            }
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (mGoMain)
            {
                procedureOwner.SetData<VarInt32>(GameConstant.ProcedureNextScene, Game.Config.GetInt("Scene.Main"));
                ChangeState<ProcedureChange>(procedureOwner);
            }
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }
            mGameForm = (GameForm)ne.UIForm.Logic;
        }

        public void GoMain()
        {
            mGoMain = true;
        }
    }
}
