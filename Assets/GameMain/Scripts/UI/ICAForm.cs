using FrameworkExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace Voyage
{
    public class ICAForm : UGuiForm
    {
        public ScrollRect scrollRect;
        public Transform transPa;
        public GameObject dialoguePrefab;
        public GameObject selectionPrefab;

        int curLevelId;
        int dialogueId;

        bool isSelectOpen = false;
    
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            curLevelId = 1;
            if (userData != null)
            {
                curLevelId = (int)userData;
            }

            dialogueId = GameMain.GameDataManager.GetSelectTable(curLevelId).StartId;
            DialogueEle item = Instantiate(dialoguePrefab,transPa).GetComponent<DialogueEle>();
            item.dialogueTxt.text = GameMain.GameDataManager.GetDialogueTable(dialogueId).DialogueContent;
            item.nameTxt.text = GameMain.GameDataManager.GetDialogueTable(dialogueId).SpeakerName;
            item.ProcessDialog();
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            if(isSelectOpen)
            {
                return;
            }

            if(Input.GetMouseButtonDown(0))
            {

                scrollRect.verticalNormalizedPosition = 0;
                if (GameMain.GameDataManager.GetDialogueTable(dialogueId).NextId != 0)
                {
                    dialogueId = GameMain.GameDataManager.GetDialogueTable(dialogueId).NextId;
                    DialogueEle item = Instantiate(dialoguePrefab, transPa).GetComponent<DialogueEle>();
                    item.dialogueTxt.text = GameMain.GameDataManager.GetDialogueTable(dialogueId).DialogueContent;
                    item.nameTxt.text = GameMain.GameDataManager.GetDialogueTable(dialogueId).SpeakerName;
                    item.ProcessDialog();
                    
                    //transPa.GetComponent<RectTransform>().anchoredPosition = new Vector2(transPa.GetComponent<RectTransform>().anchoredPosition.x, transPa.GetComponent<RectTransform>().anchoredPosition.y +70);
                }
                else
                {
                    SelectionEle select= Instantiate(selectionPrefab, transPa).GetComponent<SelectionEle>();
                    select.selectA_Txt.text = GameMain.GameDataManager.GetSelectTable(curLevelId).SelectA;
                    select.selectB_Txt.text = GameMain.GameDataManager.GetSelectTable(curLevelId).SelectB;
                    isSelectOpen = true;
                    //show selection
                }
            }


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
