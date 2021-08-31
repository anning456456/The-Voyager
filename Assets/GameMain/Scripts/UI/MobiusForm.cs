using FrameworkExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Voyage
{
    public class MobiusForm: UGuiForm
    {
      

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }

        public void CloseBtnClick()
        {
            Close();
        }

        public void OpenTaskInfo()
        {
            Game.UI.OpenUIForm(UIFormId.TaskInfoForm);
        }

        public void OpenHealthSystem()
        {
            Game.UI.OpenUIForm(UIFormId.HealthForm);
        }


        public void OpenNavigation()
        {
            Game.UI.OpenUIForm(UIFormId.NavigationForm);
        }

    }
}
