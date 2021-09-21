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
            GameMain.GameManager.LoadScene(1);
            Game.UI.OpenUIForm(UIFormId.SelectForm, GameMain.GameManager.CurLevelId);
        }

    }
}
