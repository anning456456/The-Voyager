using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkExtension
{
    public class LoginEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(LoginEventArgs).GetHashCode();

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public bool Suc
        {
            get;
            private set;
        }

        public string Info
        {
            get;
            private set;
        }


        public static LoginEventArgs Create(bool suc, string info)
        {
            LoginEventArgs args = ReferencePool.Acquire<LoginEventArgs>();
            args.Suc = suc;
            args.Info = info;
            return args;
        }

        public override void Clear()
        {
        }
    }
}
