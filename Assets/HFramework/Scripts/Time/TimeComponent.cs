using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace FrameworkExtension
{
	public class TimeComponent : GameFrameworkComponent
    {
        private List<Timer> mTimers;
        private float realDeltaTime;
        private float lastDeltaTime;
        protected override void Awake()
        {
            base.Awake();
            mTimers = new List<Timer>();
            lastDeltaTime = Time.realtimeSinceStartup;
        }

        private void Update()
        {
            realDeltaTime = Time.realtimeSinceStartup - lastDeltaTime;
            lastDeltaTime = Time.realtimeSinceStartup;
            Timer timer = null;
            for(int i = mTimers.Count - 1; i >= 0; i--)
            {
                timer = mTimers[i];
                if (timer.Remove)
                {
                    mTimers.RemoveAt(i);
                    ReferencePool.Release(timer);
                }
                else
                {
                    timer.Update(realDeltaTime);
                }
            }
        }

        private void OnDestroy()
        {
            mTimers.Clear();
            StopAllCoroutines();
        }

        public void AddTimer(Timer time)
        {
            if (time != null)
            {
                mTimers.Add(time);
            }
        }

        public void RemoveTimer(Timer time)
        {
            if (time == null)
            {
                return;
            }
            time.Remove = true;
        }

        public Coroutine Delay(float time, Action action)
        {
            return StartCoroutine(DelayCor(time, action));
        }

        public void StopDelay(Coroutine coroutine)
        {
            StopCoroutine(coroutine);
        }

        IEnumerator DelayCor(float t, Action act)
        {
            yield return new WaitForSeconds(t);
            act?.Invoke();
        }

    }
}
