using FrameworkExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Voyage
{
    public class SelectForm : UGuiForm
    {
        public Button ICABtn;
        public Button MobiusBtn;
        public Button TaskBtn;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            if(GameMain.GameManager.CurLevelId==10)
            {
                ICABtn.interactable = true;
                MobiusBtn.interactable = false;
                TaskBtn.interactable = false;
            }
            else
            {
                ICABtn.interactable = false;
                MobiusBtn.interactable = true;
                TaskBtn.interactable = true;
            }
            
        }


        public void OpenICAForm()
        {
            Game.UI.OpenUIForm(UIFormId.ICAForm,GameMain.GameManager.CurLevelId);
        }

        public void OpenMobiusForm()
        {
            Game.UI.OpenUIForm(UIFormId.MobiusForm,GameMain.GameManager.CurLevelId);
        }

        public void OpenTaskListForm()
        {
            Game.UI.OpenUIForm(UIFormId.TaskListForm, GameMain.GameManager.CurLevelId);
        }

    }
}
