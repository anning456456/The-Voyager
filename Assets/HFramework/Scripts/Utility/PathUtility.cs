using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkExtension
{
	public static class PathUtility
	{
        /// <summary>
        /// 去掉路径的扩展名
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static string EraseExtension(string fullName)
        {
            if (fullName == null)
            {
                return null;
            }
            int num = fullName.LastIndexOf('.');
            if (num > 0)
            {
                return fullName.Substring(0, num);
            }
            return fullName;
        }

        /// <summary>
        /// 获取扩展名
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static string GetExtension(string fullName)
        {
            int num = fullName.LastIndexOf('.');
            if (num > 0 && num + 1 < fullName.Length)
            {
                return fullName.Substring(num);
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取文件名，带扩展名
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static string GetFullName(string fullPath)
        {
            if (fullPath == null)
            {
                return null;
            }
            int num = fullPath.LastIndexOf("/");
            if (num > 0)
            {
                return fullPath.Substring(num + 1, fullPath.Length - num - 1);
            }
            return fullPath;
        }
    }
}
