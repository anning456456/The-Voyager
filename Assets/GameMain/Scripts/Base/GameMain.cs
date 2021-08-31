using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Voyage
{
	public class GameMain : MonoBehaviour
	{
        private void Start()
        {
            GameDataManager = GameEntry.GetComponent<GameDataManager>();
            GameManager= GameEntry.GetComponent<GameManager>();
        }

   
        public static GameDataManager GameDataManager
        {
            private set;
            get;
        }

        public static GameManager GameManager
        {
            private set;
            get;
        }



    }

    
}
