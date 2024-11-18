using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UnityEngine;

public class SaveDialogOpen : MonoBehaviour
{
    public WordWrite word;
    public void OpenExplore()
    {
        using (SaveFileDialog saveFile = new SaveFileDialog())
        {
            saveFile.Title = "保存文件";
            //saveFile.Filter = "Excel files(*.xls)|*.xls|All files(*.*)|*.*";
            saveFile.Filter = "Word files(*.docx)|*.docx|All files(*.*)|*.*";
            saveFile.InitialDirectory = UnityEngine.Application.dataPath;
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                //using (Stream s = saveFile.OpenFile())
                //{
                //    if (onSave != null)
                //        onSave(s);
                //}

                string Savepath = Path.GetDirectoryName(saveFile.FileName);
                string fileName = "/" + Path.GetFileName(saveFile.FileName);
                Debug.Log(Savepath + "/" + fileName);
                word.CreatDocument(Savepath + "/" + fileName);
            }
        }
    }
}
