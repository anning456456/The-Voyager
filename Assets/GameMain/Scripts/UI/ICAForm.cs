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
        public GameObject lineImgPrefab;
        public GameObject selectionPrefab;
        public Transform timeBox;
        public Button CloseBtn;



        List<int> selectionList;
        List<string> specialDataList;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            if (userData == null)
            {
                Debug.LogError("未传入当前关卡ID");
            }
            GameMain.GameManager.CurDialogId = GameMain.GameDataManager.GetLevelTable((int)userData).StartId;
            GameMain.GameManager.CanConsume = true;
            GameSetting.SaveUser();
            List<int> timeList = GameMain.GameDataManager.GetLevelData((int)userData);
            timeBox.GetChild(0).GetComponent<Text>().text = timeList[0].ToString();
            timeBox.GetChild(1).GetComponent<Text>().text = timeList[1].ToString();
            timeBox.GetChild(2).GetComponent<Text>().text = timeList[2].ToString();
            CloseBtn.interactable = false;
            ShowDiaLogue();
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            scrollRect.verticalNormalizedPosition = 0;

            if (GameMain.GameManager.CanConsume==false)
            {
                return;
            }


            if (GameMain.GameManager.IsSelectd == true)
            {
                ShowDiaLogue();
                GameMain.GameManager.IsSelectd = false;
            }


            if (Input.GetMouseButtonDown(0))
            {
                //处理对话后的各项数值
                switch ((DialogueType)GameMain.GameDataManager.GetDialogueTable(GameMain.GameManager.CurDialogId).DiaType)
                {
                    case DialogueType.ToDialogue:
                        GameMain.GameManager.CurDialogId = GameMain.GameDataManager.GetDialogueTable(GameMain.GameManager.CurDialogId).NextID;
                        ShowDiaLogue();
                        GameMain.GameManager.CanConsume = true;
                        break;

                    case DialogueType.ToSelection:
                        selectionList = GameMain.GameDataManager.GetSelectionData(GameMain.GameManager.CurDialogId);
                        SelectionEle selectItem = Instantiate(selectionPrefab, transPa).GetComponent<SelectionEle>();
                        selectItem.selectionList = selectionList;
                        selectItem.Init();
                        selectItem.ProcessSelectionSize();
                        GameMain.GameManager.CanConsume = false;
                        break;
                    case DialogueType.ToOtherForm:
                        specialDataList=  GameMain.GameDataManager.GetSpecialData(GameMain.GameManager.CurDialogId);
                        int UIformID = int.Parse(specialDataList[0]);
                        if(int.Parse(specialDataList[1])==1)
                        {
                            GameMain.GameManager.CurLevelId= int.Parse(specialDataList[2]);
                            GameSetting.SaveUser();
                        }
                        Game.UI.OpenUIForm(UIformID, int.Parse(specialDataList[2]));
                        GameMain.GameManager.CanConsume = false;
                        break;
                   

                }
            }
        }



        private void ShowDiaLogue()
        {
            if (GameMain.GameDataManager.GetDialogueTable(GameMain.GameManager.CurDialogId).SpeakerName != "NaN")
            {
                GameObject line = Instantiate(lineImgPrefab, transPa);
            }
            DialogueEle item = Instantiate(dialoguePrefab, transPa).GetComponent<DialogueEle>();
            item.dialogueTxt.text = GameMain.GameDataManager.GetDialogueTable(GameMain.GameManager.CurDialogId).DialogueContent;
            if (GameMain.GameDataManager.GetDialogueTable(GameMain.GameManager.CurDialogId).SpeakerName == "NaN")
            {
                item.nameTxt.gameObject.SetActive(false);
                item.dialogueTxt.GetComponent<RectTransform>().anchoredPosition = new Vector3(item.dialogueTxt.GetComponent<RectTransform>().anchoredPosition.x, 0);
            }
            else
            {
                item.nameTxt.text = GameMain.GameDataManager.GetDialogueTable(GameMain.GameManager.CurDialogId).SpeakerName;
                item.dialogueTxt.GetComponent<RectTransform>().anchoredPosition = new Vector3(item.dialogueTxt.GetComponent<RectTransform>().anchoredPosition.x, -60);
                if (GameMain.GameDataManager.GetDialogueTable(GameMain.GameManager.CurDialogId).IsShowTitle)
                {
                    item.NameTitleImg.gameObject.SetActive(true);
                }
                else
                {
                    item.NameTitleImg.gameObject.SetActive(false);
                }
            }
            item.ProcessDialogSize();
        }



        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }

        public void CloseBtnClick()
        {
            Close();
            Game.UI.OpenUIForm(UIFormId.SelectForm);
        }

    }
}
