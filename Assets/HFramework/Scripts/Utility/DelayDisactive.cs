using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkExtension
{
    public class DelayDisactive : MonoBehaviour
    {
        float time = 0f;

        public float Delay;

        private void OnEnable()
        {
            time = 0f;
        }

        private void Update()
        {
            time += Time.deltaTime;
            if (time >= Delay) {
                time = 0f;
                gameObject.SetActive(false);
            }
        }
    }
}

