﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2021-08-31 06:36:25.853
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
    /// 关卡配置。
    /// </summary>
    public class DRSelectControl : DataRowBase
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
        /// 获取起始对话。
        /// </summary>
        public int StartId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取选项A的内容。
        /// </summary>
        public string SelectA
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取。
        /// </summary>
        public int NextID_A
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取。
        /// </summary>
        public int CongnitiveChange_A
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取选项B的内容。
        /// </summary>
        public string SelectB
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取。
        /// </summary>
        public int NextID_B
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取。
        /// </summary>
        public int CongnitiveChange_B
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
            StartId = int.Parse(columnStrings[index++]);
            SelectA = columnStrings[index++];
            NextID_A = int.Parse(columnStrings[index++]);
            CongnitiveChange_A = int.Parse(columnStrings[index++]);
            SelectB = columnStrings[index++];
            NextID_B = int.Parse(columnStrings[index++]);
            CongnitiveChange_B = int.Parse(columnStrings[index++]);

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
                    StartId = binaryReader.Read7BitEncodedInt32();
                    SelectA = binaryReader.ReadString();
                    NextID_A = binaryReader.Read7BitEncodedInt32();
                    CongnitiveChange_A = binaryReader.Read7BitEncodedInt32();
                    SelectB = binaryReader.ReadString();
                    NextID_B = binaryReader.Read7BitEncodedInt32();
                    CongnitiveChange_B = binaryReader.Read7BitEncodedInt32();
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