using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace FrameworkExtension
{
    public abstract class UITab : MonoBehaviour, IPointerClickHandler
    {
        public UITabManager TabManager;
        public int Index;

        protected virtual void Awake()
        {
            TabManager.AddTab(Index, this);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            TabManager.ClickTab(Index);
        }

        public virtual void Selected(bool enable)
        {
        }
    }
}
