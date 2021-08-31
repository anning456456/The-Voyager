using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace FrameworkExtension
{
	public abstract class GameComponent : GameFrameworkComponent
    {
        private bool inited;

        public virtual void Init()
        {
            if (inited)
            {
                return;
            }
            inited = true;
        }
	}
}
