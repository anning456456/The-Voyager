using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Voyage
{
    public class DialogueEle : MonoBehaviour
    {
        public Text nameTxt;
        public Text dialogueTxt;
        public Image NameTitleImg;

        public const int chatItemTextMaxWidth = 500;


        public void ProcessDialogSize()
        {
            float curTextWidth = dialogueTxt.GetComponent<Text>().preferredWidth;
            if (curTextWidth >= chatItemTextMaxWidth)
            {
                // Text控件的Content Size Fitter由水平Preferred Size改为垂直Preferred Size
                dialogueTxt.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                dialogueTxt.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.Unconstrained;
                dialogueTxt.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 500);
                dialogueTxt.GetComponent<RectTransform>().sizeDelta =new Vector2(500, 40 + ((int)curTextWidth / chatItemTextMaxWidth) * 40);
                this.GetComponent<LayoutElement>().preferredHeight = 40+((int)curTextWidth / chatItemTextMaxWidth) * 40;
            }
            if (nameTxt.gameObject.activeInHierarchy)
            {
                this.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dialogueTxt.GetComponent<RectTransform>().rect.height + nameTxt.GetComponent<RectTransform>().rect.height);
            }
            else
            {
                this.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dialogueTxt.GetComponent<RectTransform>().rect.height);
            }

        }
    }
}
