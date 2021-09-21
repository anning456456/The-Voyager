using GameFramework.DataTable;
using FrameworkExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Voyage
{
    public class GameManager : GameComponent
    {
        private UserData userData;
        public Camera UICamera;

        public bool CanConsume=false;
        public bool IsSelectd = false;

        public int CurLevelId
        {
            get
            {
                return userData.CurLevel;
            }
            set
            {
                userData.CurLevel = value;
            }
        }

        public string PlayerName
        {
            get
            {
                return userData.PlayerName;
            }
            set
            {
                userData.PlayerName=value;
            }
        }


        public int CurDialogId
        {
            get
            {
                return userData.CurDialogue;
            }
            set
            {
                userData.CurDialogue = value;
            }
        }

        public override void Init()
        {
            base.Init();
            userData = GameSetting.CurUserData;
            if(GameSetting.CurUserData.CurLevel==0|| GameSetting.CurUserData.CurDialogue==0)
            {
                Debug.LogWarning("FirstOpenGame");
                CurLevelId = 10;
                CurDialogId = 1001;
                GameSetting.SaveUser();
            }
            else
            {
                Debug.LogWarning("当前进度为："+ GameSetting.CurUserData.CurLevel);
                Debug.LogWarning("当前对话为：" + GameSetting.CurUserData.CurDialogue);
            }
        }


        public void LoadScene(int sceneId)
        {
            IDataTable<DRScene> dtScene = Game.DataTable.GetDataTable<DRScene>();
            DRScene drScene = dtScene.GetDataRow(sceneId);
            if (drScene == null)
            {
                Log.Warning("Can not load scene '{0}' from data table.", sceneId.ToString());
                return;
            }
            Game.Scene.LoadScene(AssetUtility.GetSceneAsset(drScene.Name), Constant.AssetPriority.SceneAsset, this);
        }


    }
}
