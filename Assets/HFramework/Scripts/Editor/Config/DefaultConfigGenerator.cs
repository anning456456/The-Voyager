using GameFramework;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace FrameworkExtension.Editor
{
	public class DefaultConfigGenerator
	{
        private const string DefaultConfigPath = "Assets/GameMain/Configs/DefaultConfig";

        [MenuItem("Table/Generate Default Config")]
        private static void Generator()
        {
            DataTable table = ExcelTool.GetDataTable(Utility.Path.GetRegularPath(Path.Combine(DefaultConfigPath, "DefaultConfig.xlsx")));
            string txtPath = Utility.Path.GetRegularPath(Path.Combine(DefaultConfigPath, "DefaultConfig.txt"));
            ExcelTool.CreateTxtFile(table, txtPath);
            string binaryDataFileName = Utility.Path.GetRegularPath(Path.Combine(DefaultConfigPath, "DefaultConfig.bytes"));
            if (File.Exists(binaryDataFileName))
            {
                File.Delete(binaryDataFileName);
            }
            DataTableProcessor dataTableProcessor = new DataTableProcessor(txtPath, Encoding.UTF8, 1, 2, null, 3, 4, 1);
            dataTableProcessor.GenerateDataFile(binaryDataFileName);
        }
    }
}
