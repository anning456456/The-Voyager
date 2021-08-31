using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrameworkExtension;



namespace Voyage
{
    public class SelectionEle : MonoBehaviour
    {
        public Button selectA_Btn;
        public Button selectB_Btn;

        public Text  selectA_Txt;
        public Text  selectB_Txt;

        public GameObject curForm;
        public int turnId;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void A_Opera()
        {
            Game.UI.OpenUIForm(UIFormId.MobiusForm);
            //Game.UI.OpenUIForm(UIFormId.ICAForm, turnId);
        }



        public void B_Opera()
        {
            Game.UI.OpenUIForm(UIFormId.MobiusForm);
        }



    }
}
