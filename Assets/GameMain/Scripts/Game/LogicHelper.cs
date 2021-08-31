using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voyage
{
	public static class LogicHelper
	{
	    public static Vector3 GetRandomElfPosition(Vector3 playerPosition)
        {
            float x = Random.Range(0, 2) > 0 ? playerPosition.x + Random.Range(1f, 2f) : playerPosition.x - Random.Range(1f, 2f);
            float y = Random.Range(0, 2) > 0 ? playerPosition.y + Random.Range(1f, 2f) : playerPosition.y - Random.Range(1f, 2f);
            return new Vector3(x, y, GameConstant.ElfZ);
        }
	}
}
