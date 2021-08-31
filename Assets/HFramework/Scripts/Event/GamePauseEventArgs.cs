using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkExtension
{
    public class GamePauseEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(GamePauseEventArgs).GetHashCode();

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public bool Pause
        {
            get;
            private set;
        }

        public GamePauseEventArgs()
        {
            Pause = true;
        }


        public static GamePauseEventArgs Create(bool open)
        {
            GamePauseEventArgs args = ReferencePool.Acquire<GamePauseEventArgs>();
            args.Pause = open;
            return args;
        }

        public override void Clear()
        {
        }
    }
}
