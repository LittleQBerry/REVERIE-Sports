using EliCDavis.RecordAndPlay;
using EliCDavis.RecordAndPlay.Playback;
using EliCDavis.RecordAndPlay.Record;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class RecordContr : MonoBehaviour, IActorBuilder,IPlaybackCustomEventHandler
{
    private Recorder recorder;
    public  PlaybackBehavior playback;
   
    public GameObject playerActor;
    public GameObject bpc;
    public GameObject footBallActor;
    public GameObject leftFootActor;
    public GameObject rightFootActor;
    public GameObject ballLauncherActor;
    public GameObject boneJointActor;
    public GameObject headEyeActor;
    public GameObject ballShooterActor;
    public Actor Build(int subjectId, string subjectName, Dictionary<string, string> metadata)
    {
        switch (subjectName)
        {
            case "HeadEye":
                return new Actor(Instantiate(headEyeActor), this);
            case "FootBall":
                return new Actor(Instantiate(footBallActor),this);
            case "LeftFoot":
                return new Actor(Instantiate(leftFootActor), this);
            case "RightFoot":
                return new Actor(Instantiate(rightFootActor), this);
            case "BallLauncer":
                return new Actor(Instantiate(ballLauncherActor), this);
            case "Player":            
                return new Actor(Instantiate(playerActor), this);
            case "hip":
                return new Actor(Instantiate(boneJointActor), this);
            case "spine":
                return new Actor(Instantiate(boneJointActor), this);
            case "leftShoulder":
                return new Actor(Instantiate(boneJointActor), this);
            case "rightShoulder":
                return new Actor(Instantiate(boneJointActor), this);
            case "head":
                return new Actor(Instantiate(boneJointActor), this);
            case "leftArm":
                return new Actor(Instantiate(boneJointActor), this);
            case "leftForeArm":
                return new Actor(Instantiate(boneJointActor), this);
            case "lefthand":
                return new Actor(Instantiate(boneJointActor), this);
            case "rightArm":
                return new Actor(Instantiate(boneJointActor), this);
            case "rightForeArm":
                return new Actor(Instantiate(boneJointActor), this);
            case "righthand":
                return new Actor(Instantiate(boneJointActor), this);
            case "leftUpLeg":
                return new Actor(Instantiate(boneJointActor), this);
            case "leftLeg":
                return new Actor(Instantiate(boneJointActor), this);
            case "leftFoot":
                return new Actor(Instantiate(boneJointActor), this);
            case "rightUpLeg":
                return new Actor(Instantiate(boneJointActor), this);
            case "rightLeg":
                return new Actor(Instantiate(boneJointActor), this);
            case "rightFoot":
                return new Actor(Instantiate(boneJointActor), this);
            case "BallShooter":
                return new Actor(Instantiate(ballShooterActor), this);

        }
        return null; ;
    }
    public void OnCustomEvent(SubjectRecording subject, CustomEventCapture customEvent)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    GetDataReplay("ÕýÊÖ¹¥Çò_2023-3-11 23-19-2_³õ¼¶_3(50)");
        //}
        if (Input.GetKeyDown(KeyCode.H))
        {
            var dirs = Directory.GetDirectories(Application.dataPath + "/StreamingAssets");
            Debug.Log(Application.dataPath);
            foreach (var dir in dirs)
            {
                var dirname = dir.Split('\\');
                Debug.Log(dirname[1]);
            }
        }
    }
    public void GetDataReplay(string folderName = "")
    {
        string dir = string.Format("{0}/{1}/{2}/recordData.rap", Application.streamingAssetsPath, UserData.userName, UserData.currentDir);
        //string dir = string.Format("{0}/{1}/recordData.rap", Application.streamingAssetsPath, UserData.userName);
        Recording[] recs = EliCDavis.RecordAndPlay.IO.Unpackager.Unpackage(new FileStream(dir, FileMode.Open));
        playback = PlaybackBehavior.Build(recs[0], this, this, true);
        playback.Play();
    }

    public void Replay()
    {
        playback.Play();
    }
    public void Pause()
    {
        playback.Pause();
    }

}
