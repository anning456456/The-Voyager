﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2021-09-21 12:06:47.301
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;
using FrameworkExtension;

namespace Voyage
{
    /// <summary>
    /// 对话配置。
    /// </summary>
    public class DRDialogue : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取对话类型。
        /// </summary>
        public int DiaType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取讲话人。
        /// </summary>
        public string SpeakerName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取是否显示职级。
        /// </summary>
        public bool IsShowTitle
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取对话内容。
        /// </summary>
        public string DialogueContent
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取对话衔接ID。
        /// </summary>
        public int NextID
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取特殊判断。
        /// </summary>
        public string SpecialNext
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取自动跳转时间。
        /// </summary>
        public int ShowTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取认知读数。
        /// </summary>
        public int CongnitiveChange
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString, object userData)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            index++;
            m_Id = int.Parse(columnStrings[index++]);
            index++;
            DiaType = int.Parse(columnStrings[index++]);
            SpeakerName = columnStrings[index++];
            IsShowTitle = bool.Parse(columnStrings[index++]);
            DialogueContent = columnStrings[index++];
            NextID = int.Parse(columnStrings[index++]);
            SpecialNext = columnStrings[index++];
            ShowTime = int.Parse(columnStrings[index++]);
            CongnitiveChange = int.Parse(columnStrings[index++]);

            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    DiaType = binaryReader.Read7BitEncodedInt32();
                    SpeakerName = binaryReader.ReadString();
                    IsShowTitle = binaryReader.ReadBoolean();
                    DialogueContent = binaryReader.ReadString();
                    NextID = binaryReader.Read7BitEncodedInt32();
                    SpecialNext = binaryReader.ReadString();
                    ShowTime = binaryReader.Read7BitEncodedInt32();
                    CongnitiveChange = binaryReader.Read7BitEncodedInt32();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private void GeneratePropertyArray()
        {

        }
    }
}