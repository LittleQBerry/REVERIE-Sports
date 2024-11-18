using Duck.Http;
using EliCDavis.RecordAndPlay.Playback;
using EliCDavis.RecordAndPlay.Record;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class RecorderForAniAndVR : MonoBehaviour
{
    public static RecorderForAniAndVR instance;

    public Recorder recorder;
    private PlaybackBehavior playback;
    public GameObject[] objects;

    bool onRecord;
    public RecorderForAxis player;
    public BodyPointContr bpc;
    public SpawnPoint ballShooter;
    public BallContr ballContr;
    public LeftFootForRec leftFoot;
    public RightFootForRecord rightFoot;
    public HeadEye headEye;

    public GameData gameData;
    string url = "http://124.221.101.31";
    public int eventIndex = 0;
    private string currnetEvIATime;
    private int beatIndex;
    public List<string> beatTimeList;
    public RecordTransform leftFootModel;
    public RecordTransform rightFootModel;

    public RuleAttr ruleAttr;

    public string CurrnetEvIATime { get => currnetEvIATime; set => currnetEvIATime = value; }

    public int GetCurrentIndex()
    {
        return beatIndex;
    }
    public string DealRuleForBeat(int ruleId, float value, string explain, float value2)
    {
        var rule = ruleAttr.rules.Find(r => r.ruleID == ruleId);
        string result = string.Empty;
        switch (rule.ruleType)
        {
            case RuleType.Angle:
                result = string.Format(rule.ruleResult, 
                    RichTextColorForRed(Math.Round(value, 1).ToString()), 
                    RichTextColorForRed(rule.minValue.ToString()), 
                    RichTextColorForRed(rule.maxValue.ToString()));
                AddBeats(rule.ruleID, rule.ruleName, result, value,value2,1);
                //DebugUI.instance.ShowDebug(string.Format(rule.ruleResult, (float)Math.Round(value, 1), rule.minValue, rule.maxValue));
                break;
            case RuleType.Distance:
                if (rule.minValue > rule.maxValue)
                {
                    result = string.Format(rule.ruleResult,
                    RichTextColorForRed(Math.Round(value * 100, 1).ToString()),
                    RichTextColorForRed((rule.minValue * 100).ToString()));
                }
                else
                {
                    result = string.Format(rule.ruleResult,
                    RichTextColorForRed(Math.Round(value * 100, 1).ToString()),
                    RichTextColorForRed((rule.minValue * 100).ToString()),
                    RichTextColorForRed((rule.maxValue * 100).ToString()));
                }
                AddBeats(rule.ruleID, rule.ruleName, result, value, value2, 1);
                break;
            case RuleType.Custom:
                result = rule.ruleResult;
               
                if (ruleId == 35)
                {
                    result = string.Format(rule.ruleResult,
                        RichTextColorForRed(explain.ToString()),
                        RichTextColorForRed(PartOfFoot.脚内侧.ToString())
                        );
                    if (explain != PartOfFoot.脚内侧.ToString())
                    {
                        AddBeats(rule.ruleID, rule.ruleName, result, value, value2, 0);
                    }
                    else
                    {
                        AddBeats(rule.ruleID, rule.ruleName, result,value,value2,1);
                    }
                }else if(ruleId == 41)
                {
                    result = string.Format(rule.ruleResult,
                       RichTextColorForRed(Math.Round(value, 1).ToString()),
                       RichTextColorForRed(rule.minValue.ToString()),
                       RichTextColorForRed(rule.maxValue.ToString()),
                       explain
                       );
                    if (explain == "中上部")
                    {
                        AddBeats(rule.ruleID, rule.ruleName, result, value, value2, 0);
                    }
                    else
                    {
                        AddBeats(rule.ruleID, rule.ruleName, result,value,value2,1);
                    }
                    
                }
                else if (ruleId == 42)
                {
                    result = string.Format(rule.ruleResult,
                        RichTextColorForRed(explain.ToString()),
                        RichTextColorForRed(PartOfFoot.脚背内侧.ToString())
                        );
                    if (explain != PartOfFoot.脚背内侧.ToString())
                    {
                        AddBeats(rule.ruleID, rule.ruleName, result, value, value2, 0);
                    }
                    else
                    {
                        AddBeats(rule.ruleID, rule.ruleName, result, value, value2, 1);
                    }
                }else if(ruleId == 43 || ruleId == 52||ruleId ==48||ruleId==49)
                {
                    result = string.Format(rule.ruleResult,
                        RichTextColorForRed(Math.Round(value, 1).ToString()),
                        RichTextColorForRed(Math.Round(value2, 1).ToString()),
                        RichTextColorForRed(Math.Round(rule.minValue, 1).ToString())
                        );
                    if(value<rule.minValue|| value2 < rule.minValue)
                    {
                        AddBeats(rule.ruleID, rule.ruleName, result, value, value2, 0);
                    }
                    else
                    {
                        AddBeats(rule.ruleID, rule.ruleName, result, value, value2, 1);
                    }
                }
                //else
                //{
                //    AddBeats(rule.ruleID, rule.ruleName, result);
                //}

                break;
        }
        return result;
    }
    string RichTextColorForRed(string text) { return string.Format("<color=red>{0}</color>", text); }
    public void AddBeatIndex()
    {
        beatIndex++;
    }
    private void OnEnable()
    {
        beatTimeList = new List<string>();
        beatIndex = 0;
        eventIndex = 0;
    }
    private void Awake()
    {
        instance = this;
        recorder = ScriptableObject.CreateInstance<Recorder>();
    }

    public void NewIndexAndTime()
    {
        eventIndex++;
        CurrnetEvIATime = eventIndex + "_" + Time.time;
        beatTimeList.Add(CurrnetEvIATime);
    }
  

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void StartRecording()
    {
        if (!onRecord)
        {
            recorder.ClearSubjects();
            player.Initialize(recorder);
            bpc.Initialize(recorder);
            if (ballShooter != null)
            {
                ballShooter.Initialize(recorder);
            }
            leftFoot.Initialize(recorder);
            rightFoot.Initialize(recorder);
            if (ballContr != null)
            {
                ballContr.Initialize(recorder); 
            }
            headEye.Initialize(recorder);
            SetUpRecorderObjectUp();
            recorder.Start();
            onRecord = true;
            Debug.Log("开始录制");
            TipUI.instance.ShowDebugText("开始录制");
            UserData.currentTime = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " " + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second;
            gameData = new GameData(Time.time);
        }
    }
    public void SetUpRecorderObjectUp()
    {
        recorder.Register(player.gameObject.GetComponent<SubjectBehavior>().GetSubjectRecorder());

        recorder.Register(bpc.head.GetComponent<SubjectBehavior>().GetSubjectRecorder());
        recorder.Register(bpc.hip.GetComponent<SubjectBehavior>().GetSubjectRecorder());
        recorder.Register(bpc.leftShoulder.gameObject.GetComponent<SubjectBehavior>().GetSubjectRecorder());
        recorder.Register(bpc.rightShoulder.GetComponent<SubjectBehavior>().GetSubjectRecorder());
        recorder.Register(bpc.spine.GetComponent<SubjectBehavior>().GetSubjectRecorder());
        recorder.Register(bpc.leftArm.GetComponent<SubjectBehavior>().GetSubjectRecorder());
        recorder.Register(bpc.leftForeArm.GetComponent<SubjectBehavior>().GetSubjectRecorder());
        recorder.Register(bpc.lefthand.GetComponent<SubjectBehavior>().GetSubjectRecorder());
        recorder.Register(bpc.rightArm.GetComponent<SubjectBehavior>().GetSubjectRecorder());
        recorder.Register(bpc.rightForeArm.GetComponent<SubjectBehavior>().GetSubjectRecorder());
        recorder.Register(bpc.righthand.GetComponent<SubjectBehavior>().GetSubjectRecorder());

        recorder.Register(bpc.leftUpLeg.GetComponent<SubjectBehavior>().GetSubjectRecorder());
        recorder.Register(bpc.leftLeg.GetComponent<SubjectBehavior>().GetSubjectRecorder());
        recorder.Register(bpc.leftFoot.GetComponent<SubjectBehavior>().GetSubjectRecorder());
        recorder.Register(bpc.rightUpLeg.GetComponent<SubjectBehavior>().GetSubjectRecorder());
        recorder.Register(bpc.rightLeg.GetComponent<SubjectBehavior>().GetSubjectRecorder());
        recorder.Register(bpc.rightFoot.GetComponent<SubjectBehavior>().GetSubjectRecorder());

        recorder.Register(leftFoot.gameObject.GetComponent<SubjectBehavior>().GetSubjectRecorder());
        recorder.Register(rightFoot.gameObject.GetComponent<SubjectBehavior>().GetSubjectRecorder());

        if (ballContr != null)
        {
            recorder.Register(ballContr.gameObject.GetComponent<SubjectBehavior>().GetSubjectRecorder());
        }
        if (ballShooter != null)
        {
            recorder.Register(ballShooter.gameObject.GetComponent<SubjectBehavior>().GetSubjectRecorder());
        }
        recorder.Register(headEye.GetComponent<SubjectBehavior>().GetSubjectRecorder());
    }
    public void AddCustomEvent(string eventName,string eventIndex)
    {
        recorder.CaptureCustomEvent(eventName, eventIndex);
    }
    public void FinishRecording()
    {   
        if (!onRecord) return;
        UserData.currentDir = GameManager.instance.GetDir();
        playback = PlaybackBehavior.Build(recorder.Finish(), true);
        try
        {
            string fileName = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second;
            Directory.CreateDirectory(Application.persistentDataPath + "/" + UserData.userName + "/" + UserData.currentDir + "/");
            using (FileStream fs = File.Create(Application.persistentDataPath + "/" + UserData.userName + "/" + UserData.currentDir + "/" + "recordData.rap"))
            {
                var rec = playback.GetRecording();
                rec.RecordingName = fileName;
                EliCDavis.RecordAndPlay.IO.Packager.Package(fs, rec);
            }
        }
        catch (Exception e)
        {
            // 当try中的语句发生异常时，错误处理代码（相当于备用方案）
            Debug.LogException(e);//这边啥错误打印出来，
            Debug.LogError("adssa"+e.Message);
        }
        UploadRecordData();
        onRecord = false;
        WriteBeatFile();
        Debug.Log("结束录制");
        TipUI.instance.ShowDebugText("结束录制");
    }
    void UploadRecordData() 
    {
        string dir = string.Format("{0}/{1}/{2}/recordData.rap", Application.persistentDataPath, UserData.userName, UserData.currentDir);
        WWWForm form = new WWWForm();
        byte[] bt = File.ReadAllBytes(dir);
        form.AddBinaryData("file", bt);
        string filePath = string.Empty;
        form.AddField("filePath", UserData.userName + "/" + UserData.currentDir + "/" + "recordData.rap");
        var quest = Http.Post(url + "/school/open/upload/", form)
            .OnSuccess(response => Debug.Log(response.Text))
            .OnError(response => Debug.Log(response.Text))
            .OnUploadProgress(response => Debug.Log(Time.deltaTime))
            .Send();
    }
    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.K))
        //{
        //    StartRecording();
        //}
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    FinishRecording();
        //}
    }
    public void AddBeats(int ruleId, string _result, string _reason , float v1 , float v2 , int isWrong )
    {
        gameData.SetBeatData(ruleId, _result, _reason, v1, v2, isWrong);
        //gameData.beats.Add(new BeatData("", Time.time, 0, _result, _reason));
    }
    public void WriteBeatFile()
    {
        //resultMessage.endtime = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " " + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second;

        string path = Path.Combine(Application.persistentDataPath + "/" + UserData.userName + "/" + UserData.currentDir, "beatdata.json");

        //找到当前路径
        FileInfo file = new FileInfo(path);
        //判断有没有文件，有则打开文件，，没有创建后打开文件
        StreamWriter sw = file.CreateText();

        //ToJson接口将你的列表类传进去，，并自动转换为string类型  
        string json = JsonConvert.SerializeObject(gameData);
        //string saveJsonStr = JsonMapper.ToJson(studentManagers);
        //将转换好的字符串存进文件，
        sw.WriteLine(json);
        sw.Close();//关闭文件
        sw.Dispose();
        UploadBeatFile();
    }
    void UploadBeatFile()
    {
        WWWForm form = new WWWForm();
        string path = Application.persistentDataPath+"/" + UserData.userName + "/" + UserData.currentDir + "/" + "beatdata.json";
        byte[] bt = File.ReadAllBytes(path);
        form.AddBinaryData("file", bt);
        string filePath = string.Empty;
        
        form.AddField("filePath", UserData.userName + "/" + UserData.currentDir + "/" + "beatdata.json");
        var quest = Http.Post(url + "/school/open/upload/", form)
            .OnSuccess(response => Debug.Log(response.Text))
            .OnError(response => Debug.Log("上传失败"))
            .OnUploadProgress(response => Debug.Log(Time.deltaTime))
            .Send();
    }
    public Vector3 hipStartForwardDir;
    public float HipAndWorldForwardOffsetAngle;
    internal void SetStartInfo()
    {
        hipStartForwardDir = bpc.hip.transform.forward;
        HipAndWorldForwardOffsetAngle = Vector3.Angle(hipStartForwardDir, Vector3.right);
        Debug.Log("HipAndWorldForwardOffsetAngle" + HipAndWorldForwardOffsetAngle);
    }
}
