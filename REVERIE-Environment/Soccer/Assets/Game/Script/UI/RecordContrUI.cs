using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class RecordContrUI : MonoBehaviour
{
    public RecordData recordConfig;
    public RecordContr recordContr;
    public GameData gameData;
    public Animator animator;
    public TMPro.TMP_Text _result;
    //public Transform target1; // ��һ������
    //public Transform target2; // �ڶ�������


    //private Quaternion initRotation1; // ��һ������ĳ�ʼ��ת�Ƕ�
    //private Quaternion initRotation2; // �ڶ�������ĳ�ʼ��ת�Ƕ�
    private float timer = 0f;
    private const float interval = 1f;

    public Slider slider;
    public TMP_Text currentTime;
    public TMP_Text fullTime;
    bool wholePlay = true;
    public GameObject playButton;
    public GameObject pauseButton;

     




    RecordDetail GetRecordDetail()
    {
        string[] gamet = UserData.currentDir.Split('_');
        foreach(var p in recordConfig.recordDetails)
        {
            if (gamet[0] == p.trainingType)
            {
                return p;
            }
        }
        return null;
    }
    // Start is called before the first frame update
    void Start()
    {
        WholeMode();
    }
    public void WholeMode()
    {
        Debug.Log("wholeMode");
        beatUI.gameObject.SetActive(false);
        wholePlay = true;
        recordContr.GetDataReplay();
        SetPlayButtonOn(false);
        
        ReadJsonFile(UserData.currentDir);

    }
    public void SetPlayButtonOn(bool state)
    {
        playButton.SetActive(state);
        pauseButton.SetActive(!state);
    }
    public RecordBeatUI beatUI;
    public void BeatMode()
    {
        wholePlay = false;
        ReadJsonFile(UserData.currentDir);
        if (gameData != null)
        {
            beatUI.gameObject.SetActive(true);
            beatUI.FillData(gameData);
        }
    }
    public BeatData currentBeatData;
    float lastSetTime;
    float lastStopTime;
    public void PlayRecord(BeatData beatData)
    {
        //currentBeatData = beatData;
        //RecordDetail recordDetail = GetRecordDetail();
        //var st = beatData.beatTime - recordDetail.beforeTime - UserData.startGameTime;
        //lastSetTime = st;
        //lastStopTime = beatData.beatTime - UserData.startGameTime + recordDetail.afterTime;
        //recordContr.playback.SetTimeThroughPlayback(st);
        //recordContr.Replay();
    }
    public void PlayRecord()
    {
        if (!wholePlay)
        {
            recordContr.playback.SetTimeThroughPlayback(lastSetTime);
            recordContr.Replay();
        }
        else
        {
            recordContr.Replay();
        }
        
        SetPlayButtonOn(false);
    }
    public void OnPauseButton()
    {
        recordContr.Pause();
        SetPlayButtonOn(true);
    }
  
    // Update is called once per frame
    void Update()
    {

        if (recordContr.playback.CurrentlyPlaying())
        {
            //float value = recordContr.playback.GetTimeThroughPlayback() / recordContr.playback.RecordingDuration();
            slider.value = recordContr.playback.GetTimeThroughPlayback() / recordContr.playback.RecordingDuration();
        }
        currentTime.text = SecToTime(recordContr.playback.GetTimeThroughPlayback());
        fullTime.text = SecToTime(recordContr.playback.RecordingDuration());
        if (!wholePlay && recordContr.playback.GetTimeThroughPlayback() >lastStopTime)
        {
            OnPauseButton();
        }

    }
    string SecToTime(float sec)
    {
        TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(sec));
        string str = "";
        if (ts.Hours > 0)
        {
            str = ts.Hours.ToString() + ": " + ts.Minutes.ToString() + ": " + ts.Seconds + "";
        }
        if (ts.Hours == 0 && ts.Minutes > 0)
        {
            str = ts.Minutes.ToString() + ": " + ts.Seconds + "";
        }
        if (ts.Hours == 0 && ts.Minutes == 0)
        {
            str = ts.Seconds + "";
        }
        return str;

    }
    public void ReadJsonFile(string folderName)
    {
        Debug.Log("ReadJsonFile");
        string path = Application.streamingAssetsPath + "/"+UserData.userName + "/"+UserData.currentDir+"/" + "beatdata.json";
        //�ҵ���ǰ·��
        Debug.Log("path" + path);
        StreamReader reader = new StreamReader(path);
        if (reader == null) return;
        string jsonText = reader.ReadToEnd();
        gameData = JsonConvert.DeserializeObject<GameData>(jsonText);
        UserData.startGameTime = gameData.startTime;
        SetDetailInfo();
        if (gameData != null)
        {
            beatUI.gameObject.SetActive(true);
            beatUI.FillData(gameData);
        }
        reader.Close();
    }


    //******************************************
    public TMP_Text projectName;
    public TMP_Text dateTime;
    public TMP_Text level;
    public TMP_Text score;
    public TMP_Text title;
    public TMP_Text standerdTitle;
    void SetDetailInfo()
    {
        Debug.Log("dir:" + UserData.currentDir);
        var datas = UserData.currentDir.Split('_');
        projectName.text = datas[0];
        dateTime.text = datas[1];
        level.text = gameData.beats.Count.ToString();
        score.text = datas[3];
      
        switch (datas[0])
        {
            case "����ѵ��":
                title.text = "����ѵ���ط�";
                standerdTitle.text = "�����׼����";
                animator.SetTrigger("Pass");
                break;
            case "ͷ��ѵ��":
                title.text = "ͷ��ѵ���ط�";
                standerdTitle.text = "ͷ���׼����";
                animator.SetTrigger("Head");
                break;
            case "����ѵ��":
                title.text = "����ѵ���ط�";
                standerdTitle.text = "���ű�׼����";
                animator.SetTrigger("Shoot");
                break;
            case "����ѵ��":
                title.text = "����ѵ���ط�";
                standerdTitle.text = "�����׼����";
                animator.SetTrigger("Cross");
                break;
        }
    }


    //*******************************************
    public void SetPlayTime(float playTime)
    {
        recordContr.playback.SetTimeThroughPlayback(playTime);
    }
    
}


