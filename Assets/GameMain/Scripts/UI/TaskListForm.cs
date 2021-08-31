using FrameworkExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Voyage
{
    public class TaskListForm : UGuiForm
    {
        public void LogIn()
        {
            Game.UI.OpenUIForm(UIFormId.SelectForm);
        }


        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }

        public void CloseBtnClick()
        {
            Close();
        }

    }
}
