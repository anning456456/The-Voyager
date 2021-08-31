using FrameworkExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Voyage
{
    public class SelectForm : UGuiForm
    {
        public void OpenICAForm()
        {
            Game.UI.OpenUIForm(UIFormId.ICAForm,1);
        }

        public void OpenMobiusForm()
        {
            Game.UI.OpenUIForm(UIFormId.MobiusForm);
        }

        public void OpenTaskListForm()
        {
            Game.UI.OpenUIForm(UIFormId.TaskListForm);
        }

    }
}
