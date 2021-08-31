using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkExtension
{
	public static class NumberUtility
	{
	    public static string NumToStr(long num)
        {
            if (num < 10000)
            {
                return num.ToString();
            }
            else if (num < 1000000)
            {
                return (num / 1000f).ToString("#.#K");
            }
            else if (num < 1000000000)
            {
                return (num / 1000000f).ToString("#.#M");
            }
            else if (num < 1000000000000)
            {
                return (num / 1000000000f).ToString("#.##B");
            }
            else
            {
                return (num / 1000000000000f).ToString("#.##T");
            }
        }
	}
}
