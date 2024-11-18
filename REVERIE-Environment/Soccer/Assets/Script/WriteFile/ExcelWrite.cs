using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExcelWrite : MonoBehaviour
{
    public void ExportExcel(string path)
    {
        NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
        NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("Score");

        // 第一行
        NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);
        row.CreateCell(0).SetCellValue("第一行第一列");
        row.CreateCell(1).SetCellValue("第一行第二列");

        // 第第二行
        NPOI.SS.UserModel.IRow row1 = sheet.CreateRow(2);
        row.CreateCell(0).SetCellValue("第二行第一列");
        row.CreateCell(1).SetCellValue("第二行第二列");
        // 写入到客户端  
        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
        {
            book.Write(ms);
            File.WriteAllBytes(path, ms.ToArray());
            ms.Close();
            ms.Dispose();
        }
    }
}
