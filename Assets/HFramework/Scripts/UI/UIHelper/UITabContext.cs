using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkExtension
{
	public abstract class UITabContext : MonoBehaviour
	{
        public UITabManager TabManager;
        public int Index;
        private bool isShow = false;
        protected virtual void Awake()
        {
            TabManager.AddContext(Index, this);
        }

        public virtual void Selected(bool enable)
        {
            gameObject.SetActive(enable);

            if(enable)
            {
                Show();
            }
            else
            {
                if (isShow)
                {
                    Hide();
                }
            }
        }

        public virtual void Show()
        {
            isShow = true;
        }

        public virtual void Hide()
        {
            isShow = false;
        }

        public virtual void Open()
        {

        }

        public virtual void Close()
        {

        }
	}
}
