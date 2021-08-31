using GameFramework;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace FrameworkExtension.Editor
{
	public class DataTableGeneratorMenu 
	{
        private const string TablePath = "Assets/GameMain/DataTables";

        [MenuItem("Table/Generate Table Data")]
        private static void GeneratorTableData()
        {
            Generator();
        }

        [MenuItem("Table/Generate Table Data And Code")]
        private static void GeneratorTableDataAndCode()
        {
            Generator(true);
        }

        private static void Generator(bool code = false)
        {
            string[] files = Directory.GetFiles(Utility.Path.GetRegularPath(Path.Combine(TablePath, "xlsx")));
            for (int i=0; i<files.Length; i++)
            {
                string oriPath = Utility.Path.GetRegularPath(files[i]);
                if (oriPath.Contains(".meta"))
                {
                    continue;
                }
                Debug.Log(oriPath);
                if (oriPath.EndsWith(".xlsx") || oriPath.EndsWith(".xls"))
                {       
#if UNITY_IOS
                    DataTable table = ExcelTool.XLSX(oriPath);
#else
                    DataTable table = ExcelTool.GetDataTable(oriPath);
#endif
                    string dataTableName = PathUtility.EraseExtension(PathUtility.GetFullName(oriPath));
                    string txtPath = Utility.Path.GetRegularPath(Path.Combine(TablePath, dataTableName)) + ".txt";
                    ExcelTool.CreateTxtFile(table, txtPath);
                    DataTableProcessor dataTableProcessor = DataTableGenerator.CreateDataTableProcessor(dataTableName);
                    if (!DataTableGenerator.CheckRawData(dataTableProcessor, dataTableName))
                    {
                        Debug.LogError(Utility.Text.Format("Check raw data failure. DataTableName='{0}'", dataTableName));
                        break;
                    }

                    DataTableGenerator.GenerateDataFile(dataTableProcessor, dataTableName);
                    if (code)
                        DataTableGenerator.GenerateCodeFile(dataTableProcessor, dataTableName);
                
                }
            }
        }

        
    }
}
