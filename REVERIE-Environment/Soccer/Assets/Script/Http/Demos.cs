using Duck.Http;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Demos : MonoBehaviour
{
    //上传文件
    private void UploadFiles(string path)
    {
        WWWForm form = new WWWForm();
        byte[] bt = File.ReadAllBytes(path);//尧都区的文件路径
        form.AddBinaryData("file", bt);
        form.AddField("fileName", "文件名称", Encoding.UTF8);
        //int _intFileType = fileType.value;
        //form.AddField("fileTitle", title.text);
        //form.AddField("fileDescrb", discrib.text);
        //form.AddField("resource_type", resource_type.value);
        //form.AddField("product_type", product_type.value);
        //form.AddField("positionX", position.x.ToString());
        //form.AddField("positionY", position.y.ToString());
        form.AddField("target_id", -1);
        //form.AddField("userID", GameManager.instance.sSD_User.id);
        var quest = Http.Post("http://127.0.0.1:8080" + "/log/upload", form)
            .OnSuccess(response => Debug.Log("上传成功"))
            .OnError(response => Debug.Log("上传失败"))
            .OnUploadProgress(response => Debug.Log(Time.deltaTime))
            .Send();
    }
#region 下载并加载图片
    void DealImageResource()
    {
        //Resource resource = JsonConvert.DeserializeObject<Resource>(param);
        Http.Get("http://127.0.0.1:8080/log/upload/pictureName.png")
              .OnSuccess(response => SetImage(response.Bytes))
              .OnError(response => Debug.Log("下载图片失败！"))
              .OnDownloadProgress(progress => Debug.Log(progress))
              .Send();
    }
    public Sprite FormDataToSprite(byte[] bytes)
    {
        Texture2D texture = new Texture2D(10, 10);
        texture.LoadImage(bytes);//流数据转换成Texture2D
        //创建一个Sprite,以Texture2D对象为基础
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        return sp;
    }
    Image image;
    private void SetImage(byte[] bytes)
    {
        image.sprite = FormDataToSprite(bytes);
    }
    #endregion



    #region 下载并加载音乐
    private void DealAudioResource()
    {
       // Resource resource = JsonConvert.DeserializeObject<Resource>(param);
        Http.Get("http://127.0.0.1:8080/log/upload/music.mp3")
              .OnSuccess(response => SetAudioType(response.Bytes))
              .OnError(response => Debug.Log("下载音乐失败！"))
              .OnDownloadProgress(progress => Debug.Log(progress))
              .Send();
    }
    AudioSource audioSource;
    private void SetAudioType(byte[] bytes)
    {
       audioSource.clip = NAudioPlayer.FromMp3Data(bytes);
       //audioSource.clip = NAudioPlayer.FromWavData(bytes);
    }
    #endregion
}
