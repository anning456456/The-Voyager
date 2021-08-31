using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkExtension
{
	public class FloatingEffect : MonoBehaviour
	{
        float radian = 0; // 弧度
        float perRadian = 0.03f; // 每次变化的弧度
        public float radius = 0.8f; // 半径
        Vector3 oldPos; // 开始时候的坐标

        void Start()
        {
            oldPos = transform.position; // 将最初的位置保存到oldPos
        }

        // Update is called once per frame
        void Update()
        {
            radian += perRadian; // 弧度每次加0.03
            float dy = Mathf.Cos(radian) * radius; // dy定义的是针对y轴的变量，也可以使用sin，找到一个适合的值就可以
            transform.position = oldPos + new Vector3(0, dy, 0);
        }

    }
}
