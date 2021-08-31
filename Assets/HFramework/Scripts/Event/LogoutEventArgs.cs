using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkExtension
{
    public class LogoutEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(LogoutEventArgs).GetHashCode();

        public override int Id
        {
            get
            {
                return EventId;
            }
        }


        public static LogoutEventArgs Create()
        {
            LogoutEventArgs args = ReferencePool.Acquire<LogoutEventArgs>();
            return args;
        }

        public override void Clear()
        {
        }
    }
}
