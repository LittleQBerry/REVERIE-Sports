using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class UploadFile : MonoBehaviour
{
    public void OpenExplore()
    {
        string fileFitter = "图片\0 *.jpg;0 *.png";
        OpenFileName openFileName = new OpenFileName();
        openFileName.structSize = Marshal.SizeOf(openFileName);
        //switch (fileType.value)三种文件的上传赛选格式
        //{
        //    case 0:
        //        fileFitter = "图片\0*.png; *.jpg\0";
        //        break;
        //    case 1://kim cancel vediotype hard code
        //        fileFitter = "视频\0 *.mp4";
        //        break;
        //    case 2:
        //        fileFitter = "音频\0*.mp3;*.wav\0";
        //        break;
        //}
        openFileName.filter = fileFitter;
        openFileName.path = new string(new char[256]);
        openFileName.maxFile = openFileName.path.Length;
        openFileName.fileTitle = new string(new char[64]);
        openFileName.maxFileTitle = openFileName.fileTitle.Length;
        openFileName.initialDir = Application.streamingAssetsPath.Replace('/', '\\');//默认路径
        openFileName.title = "选择上传文件，文件大小不能超过100mb";
        openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;

        if (LocalDialog.GetSaveFileName(openFileName))
        {
            //do something
            string _path = openFileName.path;
            string _fileName = openFileName.fileTitle;
        }
    }
    public bool CheckFileSize(string path)
    {
        FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        if (fileStream.Length >= 104857600)
        {
            Debug.Log("文件不能超过100mb");
            return false;
        }
        if (fileStream.Length == 0)
        {
            Debug.Log("不能上传空文件");
            return false;
        }
        return true;
    }
}
