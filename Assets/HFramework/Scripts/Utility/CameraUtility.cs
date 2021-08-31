using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkExtension
{
	public static class CameraUtility
	{
	    public static void FixedWidth(Camera camera, float size)
        {
            camera.orthographicSize = size / 2 / (Screen.width / (float)Screen.height);
        }
    }
}
