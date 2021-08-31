using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FrameworkExtension
{
    [RequireComponent(typeof(Image))]
	public class UISetNativeSize : MonoBehaviour
	{
        private void Awake()
        {
            GetComponent<Image>().SetNativeSize();
        }
    }
}
