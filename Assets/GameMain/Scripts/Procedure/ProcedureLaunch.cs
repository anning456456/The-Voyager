using GameFramework.Fsm;
using GameFramework.Localization;
using GameFramework.Procedure;
using FrameworkExtension;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Voyage
{
    public class ProcedureLaunch : ProcedureBase
    {
        private bool m_InitResourcesComplete;
        private bool inited = false;
        private float time = 0f;

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Log.Info("Game Launch");
            Log.Info(Screen.safeArea);
        }

        private void Init()
        {
            inited = true;
            //Game.SDK.GetNotch();
            DG.Tweening.DOTween.Clear();
            // 构建信息：发布版本时，把一些数据以 Json 的格式写入 Assets/GameMain/Configs/BuildInfo.txt，供游戏逻辑读取。
            Game.BuildData.InitBuildInfo();
            // 语言配置：设置当前使用的语言，如果不设置，则默认使用操作系统语言。
            InitLanguageSettings();
            // 变体配置：根据使用的语言，通知底层加载对应的资源变体。
            InitCurrentVariant();
            // 画质配置：根据检测到的硬件信息和用户配置数据，设置即将使用的画质选项。
            InitQualitySettings();
            // 声音配置：根据用户配置数据，设置即将使用的声音选项。
            InitSoundSettings();
            // 默认字典：加载默认字典文件 Assets/GameMain/Configs/DefaultDictionary.xml。
            // 此字典文件记录了资源更新前使用的各种语言的字符串，会随 App 一起发布，故不可更新。
            Game.BuildData.InitDefaultDictionary();

            if (!Game.Base.EditorResourceMode)
            {
                Game.Resource.InitResources(OnInitResourcesComplete);
            }
            else
            {
                m_InitResourcesComplete = true;
            }
        }

        private void OnInitResourcesComplete()
        {
            m_InitResourcesComplete = true;

            Log.Info("Init resources complete.");
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            time += elapseSeconds;
            if (!inited && time > 0.1f)
            {
                Init();
            }
            if (m_InitResourcesComplete)
            {
                ChangeState<ProcedurePreload>(procedureOwner);
            }
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

        }

        private void InitLanguageSettings()
        {
            if (Game.Base.EditorResourceMode && Game.Base.EditorLanguage != Language.Unspecified)
            {
                // 编辑器资源模式直接使用 Inspector 上设置的语言
                return;
            }

            Language language = Game.Localization.Language;
            if (Game.Setting.HasSetting(Constant.Setting.Language))
            {
                try
                {
                    string languageString = Game.Setting.GetString(Constant.Setting.Language);
                    language = (Language)Enum.Parse(typeof(Language), languageString);
                }
                catch
                {

                }
            }

            if (language != Language.English
                && language != Language.ChineseSimplified)
            {
                // 若是暂不支持的语言，则使用英语
                language = Language.English;

                Game.Setting.SetString(Constant.Setting.Language, language.ToString());
                Game.Setting.Save();
            }

            Game.Localization.Language = language;

            Log.Info("Init language settings complete, current language is '{0}'.", language.ToString());
        }

        private void InitCurrentVariant()
        {
            if (Game.Base.EditorResourceMode)
            {
                // 编辑器资源模式不使用 AssetBundle，也就没有变体了
                return;
            }

            string currentVariant = null;
            switch (Game.Localization.Language)
            {
                case Language.English:
                    currentVariant = "en-us";
                    break;
                case Language.ChineseSimplified:
                    currentVariant = "zh-cn";
                    break;
            }

            Game.Resource.SetCurrentVariant(currentVariant);

            Log.Info("Init current variant complete.");
        }

        private void InitQualitySettings()
        {
            QualityLevelType defaultQuality = QualityLevelType.Ultra;
            int qualityLevel = Game.Setting.GetInt(Constant.Setting.QualityLevel, (int)defaultQuality);
            QualitySettings.SetQualityLevel(qualityLevel, true);

            Log.Info("Init quality settings complete.");
        }

        private void InitSoundSettings()
        {
            Game.Sound.Mute("Music", Game.Setting.GetBool(Constant.Setting.MusicMuted, false));
            Game.Sound.SetVolume("Music", Game.Setting.GetFloat(Constant.Setting.MusicVolume, 0.3f));
            Game.Sound.Mute("Sound", Game.Setting.GetBool(Constant.Setting.SoundMuted, false));
            Game.Sound.SetVolume("Sound", Game.Setting.GetFloat(Constant.Setting.SoundVolume, 1f));
            Game.Sound.Mute("UISound", Game.Setting.GetBool(Constant.Setting.UISoundMuted, false));
            Game.Sound.SetVolume("UISound", Game.Setting.GetFloat(Constant.Setting.UISoundVolume, 1f));

            Log.Info("Init sound settings complete.");
        }
    }
}
