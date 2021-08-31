using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using System;

namespace FrameworkExtension
{
	public class Game : MonoBehaviour
	{
        private static bool m_GamePause;
        private static float m_Time;

        //private delegate void 
        public static event Action<bool, float> PauseEvent;

        // Start is called before the first frame update
        void Start()
        {
            m_GamePause = false;
            InitCompnents();
            InitCustomComponents();
        }

        /// <summary>
        /// 获取游戏基础组件。
        /// </summary>
        public static BaseComponent Base
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取配置组件。
        /// </summary>
        public static ConfigComponent Config
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取数据结点组件。
        /// </summary>
        public static DataNodeComponent DataNode
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取数据表组件。
        /// </summary>
        public static DataTableComponent DataTable
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取调试组件。
        /// </summary>
        public static DebuggerComponent Debugger
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取下载组件。
        /// </summary>
        public static DownloadComponent Download
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取实体组件。
        /// </summary>
        public static EntityComponent Entity
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取事件组件。
        /// </summary>
        public static EventComponent Event
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取有限状态机组件。
        /// </summary>
        public static FsmComponent Fsm
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取本地化组件。
        /// </summary>
        public static LocalizationComponent Localization
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取网络组件。
        /// </summary>
        public static NetworkComponent Network
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取对象池组件。
        /// </summary>
        public static ObjectPoolComponent ObjectPool
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取流程组件。
        /// </summary>
        public static ProcedureComponent Procedure
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取资源组件。
        /// </summary>
        public static ResourceComponent Resource
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取场景组件。
        /// </summary>
        public static SceneComponent Scene
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取配置组件。
        /// </summary>
        public static SettingComponent Setting
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取声音组件。
        /// </summary>
        public static SoundComponent Sound
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取界面组件。
        /// </summary>
        public static UIComponent UI
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取网络组件。
        /// </summary>
        public static WebRequestComponent WebRequest
        {
            get;
            private set;
        }

        private static void InitCompnents()
        {
            Base = UnityGameFramework.Runtime.GameEntry.GetComponent<BaseComponent>();
            Config = UnityGameFramework.Runtime.GameEntry.GetComponent<ConfigComponent>();
            DataNode = UnityGameFramework.Runtime.GameEntry.GetComponent<DataNodeComponent>();
            DataTable = UnityGameFramework.Runtime.GameEntry.GetComponent<DataTableComponent>();
            Debugger = UnityGameFramework.Runtime.GameEntry.GetComponent<DebuggerComponent>();
            Download = UnityGameFramework.Runtime.GameEntry.GetComponent<DownloadComponent>();
            Entity = UnityGameFramework.Runtime.GameEntry.GetComponent<EntityComponent>();
            Event = UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent>();
            Fsm = UnityGameFramework.Runtime.GameEntry.GetComponent<FsmComponent>();
            Localization = UnityGameFramework.Runtime.GameEntry.GetComponent<LocalizationComponent>();
            Network = UnityGameFramework.Runtime.GameEntry.GetComponent<NetworkComponent>();
            ObjectPool = UnityGameFramework.Runtime.GameEntry.GetComponent<ObjectPoolComponent>();
            Procedure = UnityGameFramework.Runtime.GameEntry.GetComponent<ProcedureComponent>();
            Resource = UnityGameFramework.Runtime.GameEntry.GetComponent<ResourceComponent>();
            Scene = UnityGameFramework.Runtime.GameEntry.GetComponent<SceneComponent>();
            Setting = UnityGameFramework.Runtime.GameEntry.GetComponent<SettingComponent>();
            Sound = UnityGameFramework.Runtime.GameEntry.GetComponent<SoundComponent>();
            UI = UnityGameFramework.Runtime.GameEntry.GetComponent<UIComponent>();
            WebRequest = UnityGameFramework.Runtime.GameEntry.GetComponent<WebRequestComponent>();
        }

        public static SDKComponent SDK
        {
            get;
            private set;
        }

        public static TimeComponent Time
        {
            get;
            private set;
        }

        public static BuildDataComponent BuildData
        {
            get;
            private set;
        }

        public static AdsComponent Ads {
            get;
            private set;
        }

        public static ArtComponent Art {
            get;
            private set;
        }

      
        private void InitCustomComponents()
        {
            SDK = UnityGameFramework.Runtime.GameEntry.GetComponent<SDKComponent>();
            Time = UnityGameFramework.Runtime.GameEntry.GetComponent<TimeComponent>();
            BuildData = UnityGameFramework.Runtime.GameEntry.GetComponent<BuildDataComponent>();
            Ads = GameEntry.GetComponent<AdsComponent>();
            Art = GameEntry.GetComponent<ArtComponent>();
        }

        private void OnApplicationFocus(bool focus)
        {
            Log.Info("game OnApplicationFocus {0} {1}", GamePause, focus);
            if (GamePause && focus)
            {
                GamePause = false;
            }
            else if (!GamePause && !focus)
            {
                GamePause = true;
            }

        }

        private void OnApplicationPause(bool pause)
        {
            Log.Info("game OnApplicationPause {0} {1}", GamePause, pause);
            if (!GamePause && pause)
            {
                GamePause = true;
            }
            else if (GamePause && !pause)
            {
                GamePause = false;
            }
        }

        public static void SetGamePause(bool enable)
        {
            m_GamePause = enable;
        }

        public bool GamePause
        {
            get
            {
                return m_GamePause;
            }
            set
            {
                m_GamePause = value;
                Log.Info("game pause {0}", value);
                float t = UnityEngine.Time.realtimeSinceStartup - m_Time;
                if (value) {
                    m_Time = UnityEngine.Time.realtimeSinceStartup;
                    SDK?.GamePause();
                } else {
                    SDK?.GameResume();
                }
                PauseEvent?.Invoke(m_GamePause, t);
            }
        }
    }
}
