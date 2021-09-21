using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrameworkExtension;



namespace Voyage
{
    public class SelectionEle : MonoBehaviour
    {
        public List<int> selectionList;

        public GameObject selectBtnPrefab;


        public void Init()
        {

            for (int i = 0; i < selectionList.Count; i++)
            {
                SelectionBtn selectBtn = Instantiate(selectBtnPrefab, transform).GetComponent<SelectionBtn>();
                selectBtn.selectContent.text = GameMain.GameDataManager.GetSelectTable(selectionList[i]).SelectText;
                selectBtn.dialogueId= GameMain.GameDataManager.GetSelectTable(selectionList[i]).DialogueID;
            }

        }

        public void ProcessSelectionSize()
        {
            this.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (selectBtnPrefab.GetComponent<RectTransform>().rect.height+20)* selectionList.Count+20);
            this.GetComponent<LayoutElement>().preferredHeight = (selectBtnPrefab.GetComponent<RectTransform>().rect.height + 20) * selectionList.Count + 20;
        }





    }
}
