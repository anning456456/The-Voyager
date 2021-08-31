using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkExtension
{
    public class Timer : IReference
    {
        private int mFirst;
        private int mInterval;
        private Action<int> mIntervalAct;

        private bool mFirstStart;

        private float mTime;
        private int mIntTime;

        public bool Remove
        {
            get;
            set;
        }

        public Timer()
        {
            Clear();
        }

        public void Update(float deltaTime)
        {
            if (mIntervalAct == null)
            {
                return;
            }
            mTime += deltaTime;
            mIntTime = (int)mTime;
            if (mFirstStart)
            {
                DoAction(mIntTime);
            }
            else
            {
                if (mIntTime >= mFirst)
                {
                    mFirstStart = true;
                    mTime -= mFirst;
                    DoAction(mIntTime - mFirst, 1);
                }
            }
        }

        private void DoAction(int time, int num = 0)
        {
            if(time >= mInterval)
            {
                int t = time / mInterval;
                mTime -= t * mInterval;
                num += t;
            }
            if (num > 0)
            {
                mIntervalAct.Invoke(num);
            }
        }

        /// <summary>
        /// 创建定时器
        /// </summary>
        /// <param name="first">开始时间</param>
        /// <param name="interval">定时间隔</param>
        /// <param name="intervalAction">定时执行的方法，int值为经过了多少次间隔，一般为1，如果出现程序进入后台恢复后，会经历多个间隔</param>
        /// <returns></returns>
        public static Timer Create(int first, int interval, Action<int> intervalAction)
        {
            Timer timer = ReferencePool.Acquire<Timer>();
            timer.mFirst = first;
            timer.mInterval = interval;
            timer.mIntervalAct = intervalAction;
            timer.Remove = false;
            return timer;
        }
          

        public void Clear()
        {
            mTime = 0f;
            mFirstStart = false;
            mIntervalAct = null;
            Remove = false;
        }
    }
}
