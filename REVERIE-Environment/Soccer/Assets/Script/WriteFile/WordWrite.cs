using NPOI.XWPF.UserModel;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordWrite : MonoBehaviour
{
    private XWPFDocument doc;

    public int title1Size = 14;
    public int title2Size = 12;
    public int contentSize = 10;
    public void CreatDocument(string path)
    {
        doc = new XWPFDocument();
        //创建标题名字
        XWPFParagraph paragraph = doc.CreateParagraph();
        paragraph.Alignment = ParagraphAlignment.CENTER;
        XWPFRun run = paragraph.CreateRun();
        run.FontSize = 20;
        run.FontFamily = "宋体";
        run.SetText("旅游报告");
        run.IsBold = true;
        doc.CreateParagraph();

        //创建表格 20行 3列
        XWPFTable table = doc.CreateTable(20, 3);
        table.SetColumnWidth(0, 20 * 256);
        table.SetColumnWidth(1, 2 * 256);
        table.SetColumnWidth(2, 20 * 256);
        {
            SetTableCell(table.GetRow(0).GetCell(0), ParagraphAlignment.CENTER, contentSize, "资源列表", true);
            SetTableCell(table.GetRow(0).GetCell(1), ParagraphAlignment.CENTER, contentSize, "资源类型", true);
            SetTableCell(table.GetRow(0).GetCell(2), ParagraphAlignment.CENTER, contentSize, "适合的旅游产品类型", true);
        }

        ////根据Report类创建表格后面的文字内容
        //CreateParagraph(report);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        FileStream fs = new FileStream(path, FileMode.Create);


        doc.Write(fs);
        fs.Close();
        fs.Dispose();
        Debug.Log("写入成功");
    }
    void SetTableCell(XWPFTableCell cell, ParagraphAlignment _alignment, int _fontSize,
       string _content, bool bold = false)
    {
        XWPFParagraph p = cell.AddParagraph();
        XWPFRun run = p.CreateRun();
        p.Alignment = _alignment;
        run.FontSize = _fontSize;
        run.FontFamily = "宋体";
        run.SetText(_content);
        run.IsBold = bold;
    }
}
