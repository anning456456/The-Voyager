using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrameworkExtension;



namespace Voyage
{
    public class SelectionBtn : MonoBehaviour
    {
        public Text selectContent;

        public int dialogueId;

        public void ProcessingSelection()
        {
            
            GameMain.GameManager.CurDialogId = dialogueId;
            GameMain.GameManager.CanConsume = true;
            GameMain.GameManager.IsSelectd = true;
            transform.parent.gameObject.SetActive(false);
            

        }




        
    }
}
