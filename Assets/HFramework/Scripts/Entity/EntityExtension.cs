using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace FrameworkExtension
{
	public static class EntityExtension
	{
        private static int s_SerialId = 0;
        public static int GenerateSerialId(this EntityComponent entityComponent)
        {
            return ++s_SerialId;
        }

    }
}
