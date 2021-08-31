
using ExcelDataReader;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;
using System.IO;
using UnityEngine;

namespace FrameworkExtension.Editor
{
    public class ExcelTool
	{
        public static DataTable GetDataTable(string path)
        {
            IWorkbook workbook = null;
            ISheet sheet;
            DataTable data = new DataTable();
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

            if (path.IndexOf(".xlsx") > 0)
            {
                workbook = new XSSFWorkbook(fs);
            }
            else if (path.IndexOf(".xls") > 0)
            {

                workbook = new HSSFWorkbook(fs);
            }

            sheet = workbook.GetSheetAt(0);

            if (sheet != null)
            {
                IRow row;
                DataRow dataRow;
                int colMax = 0;
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {
                    row = sheet.GetRow(i);
                    if (row.LastCellNum > colMax)
                    {
                        colMax = row.LastCellNum;
                    }
                }
                for (int i = 0; i < colMax; i++)
                {
                    data.Columns.Add(new DataColumn());
                }
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {
                    row = sheet.GetRow(i);   //row读入第i行数据
                    if (row != null)
                    {
                        dataRow = data.NewRow();
                        for (int j = 0; j < row.LastCellNum; j++)  //对工作表每一列
                        {
                            ICell cell = row.GetCell(j);
                            string cellValue = cell == null ? "" : cell.ToString(); //获取i行j列数据
                            dataRow[j] = cellValue;
                        }
                        data.Rows.Add(dataRow);
                    }
                }
            }
            fs.Close();
            return data;
        }

        public static DataTable XLSX(string path)
        {
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            var excelReader = ExcelReaderFactory.CreateReader(stream);

            DataSet result = excelReader.AsDataSet();
            DataTable data = new DataTable();
            DataRow dataRow;
            int columns = result.Tables[0].Columns.Count;
            int rows = result.Tables[0].Rows.Count;

            for (int i = 0; i < columns; i++)
            {
                data.Columns.Add(new DataColumn());
            }

            for (int i = 0; i < rows; i++)
            {
                dataRow = data.NewRow();
                for (int j = 0; j < columns; j++)
                {
                    dataRow[j] = result.Tables[0].Rows[i][j].ToString();
                }
                data.Rows.Add(dataRow);
            }
            stream.Close();
            return data;
        }

        public static void CreateTxtFile(DataTable table, string filePath)
        {
            //Debug.Log(table.Rows.Count + ","  + table.Columns.Count);
            string path = filePath;
            File.Delete(path);
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            string val = "";
            for (int j = 0; j < table.Rows.Count; j++)
            {
                DataRow dr = table.Rows[j];
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    val = "";
                    if (dr[i] != null)
                    {
                        val = dr[i].ToString();
                    }
                    //Debug.LogError(val + "," + i + "," + table.Columns.Count);
                    if (i == table.Columns.Count - 1)
                    {
                        sw.Write(val);
                    }
                    else
                    {
                        sw.Write(val + "\t");
                    }
                }
                if (j != table.Rows.Count - 1)
                {
                    sw.WriteLine();
                }
            }
            sw.Flush();
            sw.Close();
            fs.Close();
            Debug.Log("Create table txt:" + path);
        }
    }
}
