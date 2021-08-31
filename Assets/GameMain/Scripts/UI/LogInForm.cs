using FrameworkExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Voyage
{
    public class LogInForm : UGuiForm
    {
        public void LogIn()
        {
            Game.UI.OpenUIForm(UIFormId.SelectForm);
        }

    }
}
