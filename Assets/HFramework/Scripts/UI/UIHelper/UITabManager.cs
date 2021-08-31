using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace FrameworkExtension
{
	public class UITabManager : MonoBehaviour
	{
        private Dictionary<int, UITab> Tabs = new Dictionary<int, UITab>();
        private Dictionary<int, UITabContext> Contexts = new Dictionary<int, UITabContext>();
        private int curIndex = 0;

        public void Show(int index)
        {
            if (Tabs.ContainsKey(index) && Contexts.ContainsKey(index))
            {
                curIndex = index;
                foreach (int key in Tabs.Keys)
                {
                    Tabs[key].Selected(index == key);
                }
                foreach (int key in Contexts.Keys)
                {
                    Contexts[key].Selected(index == key);
                }
            }
            else
            {
                Log.Error("Tab index {0} not right", index);
            }
        }

        public void Hide()
        {
            Contexts[curIndex].Selected(false);
        }

        public void Open()
        {
            foreach(UITabContext uITabContext in Contexts.Values)
            {
                uITabContext.Open();
            }
        }

        public void Close()
        {
            foreach (UITabContext uITabContext in Contexts.Values)
            {
                uITabContext.Close();
            }
        }

        public void AddTab(int index, UITab tab)
        {
            Tabs.Add(index, tab);
        }

        public void AddContext(int index, UITabContext context)
        {
            Contexts.Add(index, context);
        }

        public void ClickTab(int index)
        {
            if (curIndex == index)
            {
                return;
            }
            if (Tabs.ContainsKey(index) && Contexts.ContainsKey(index))
            {
                Tabs[curIndex].Selected(false);
                Contexts[curIndex].Selected(false);
                curIndex = index;
                Tabs[curIndex].Selected(true);
                Contexts[curIndex].Selected(true);
            }
            else
            {
                Log.Error("Tab index {0} not right", index);
            }
        }
	}
}
