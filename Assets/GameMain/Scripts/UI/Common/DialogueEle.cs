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
        public Image lineImg;


        public const int chatItemTextMaxWidth = 500;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void ProcessDialog()
        {
            float curTextWidth = dialogueTxt.GetComponent<Text>().preferredWidth;
            Debug.LogError(curTextWidth);
            if (curTextWidth >= chatItemTextMaxWidth)
            {
                // Text控件的Content Size Fitter由水平Preferred Size改为垂直Preferred Size
                dialogueTxt.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                dialogueTxt.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                dialogueTxt.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 500);
                this.GetComponent<LayoutElement>().preferredHeight= dialogueTxt.GetComponent<Text>().preferredHeight;
                lineImg.GetComponent<RectTransform>().anchoredPosition=new Vector2(lineImg.GetComponent<RectTransform>().anchoredPosition.x, lineImg.GetComponent<RectTransform>().anchoredPosition.y- dialogueTxt.GetComponent<Text>().preferredHeight+20);
                this.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dialogueTxt.GetComponent<Text>().preferredHeight+ nameTxt.GetComponent<RectTransform>().rect.height+20);
            }
        
        }




    }
}
