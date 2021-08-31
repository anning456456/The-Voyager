using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkExtension
{
	public class RotationUtility : MonoBehaviour
	{
        public float Speed = 100f;

        public Vector3 Axis = Vector3.zero;

        private void Update()
        {
            transform.Rotate(Axis * Speed * Time.deltaTime, Space.Self);
        }
    }
}
