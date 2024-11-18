

using EliCDavis.RecordAndPlay.Playback;
using EliCDavis.RecordAndPlay.Record;
using HutongGames.PlayMaker.Actions;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.SpatialTracking.TrackedPoseDriver;

public enum FootSide
{
    Left,
    Right
}
public class GameManager : MonoBehaviour, IActorBuilder, IPlaybackCustomEventHandler
{
    public PlayMakerFSM fSM;
    public static GameManager instance;
    public GameType gameType;
    public int score;
    public Transform grand;
    //public FootContr leftFoot;
    //public FootContr rightFoot;
    //public int GameLevel = 1;//1 初级  2 中级 3 高级 


    FootSide freeFootSide = FootSide.Left;
    public GameObject ballObject;
    public Transform ballStartPos;
    private GameObject currentBall;

    public bool GameStart = false;

    public int m_braffleCount;
    public int m_goalCount;
    public int m_fakeManCount;
    public float gameTime;
    public GameObject CurrentBall { get => currentBall; set => currentBall = value; }

    public SpawnPoint spawnPoint;
    public SettingOffset settingOffset;

    public List<snapTracker> snapFeet;
    public void JumpSceneByIndex()
    {
        var index = SceneManager.GetActiveScene().buildIndex;
        if (index != 3)
        {
            SceneManager.LoadScene(index + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    private void FixedUpdate()
    {
        if (gameType != GameType.头球)
        {
            //if (leftFoot.speed < rightFoot.speed)
            //{
            //    freeFootSide = FootSide.Left;
            //}
            //else
            //{
            //    freeFootSide = FootSide.Right;
            //}
        }
    }
    public string GetFinishCount()
    {
        string scoreStr = String.Empty;
        int scoreCount = 0;
        int finishScore = 0;
        switch (gameType)
        {
            case GameType.传球:
                foreach(var beat in RecorderForAniAndVR.instance.gameData.beats)
                {
                    if (beat.results != null)
                    {
                        scoreCount++;
                    }
                    if (beat.force.Count >= 2)
                    {
                        if(beat.force[beat.force.Count-1]> beat.force[beat.force.Count - 2])
                        {
                            finishScore++;
                        }
                    }
                }
                scoreStr = finishScore+"("+scoreCount+")";
                break;
            case GameType.射门:
                foreach (var beat in RecorderForAniAndVR.instance.gameData.beats)
                {
                    if (beat.results != null)
                    {
                        scoreCount++;
                    }
                    if (beat.force.Count >= 1)
                    {
                        finishScore++;
                    }
                }
                scoreStr = finishScore + "(" + scoreCount + ")";
                break;
            case GameType.头球:
                foreach (var beat in RecorderForAniAndVR.instance.gameData.beats)
                {
                    if (beat.results != null)
                    {
                        scoreCount++;
                    }
                    if (beat.force.Count >= 1)
                    {
                        finishScore++;
                    }
                }
                scoreStr = finishScore + "(" + scoreCount + ")";
                break;
            case GameType.运球:
                scoreStr = "4(4)";
                break;
        }
        return scoreStr;
    }
    public string GetDir()
    {
        string type = String.Empty;
        string scoreStr = GetFinishCount();
        switch (gameType)
        {
            case GameType.射门:
                type = "射门训练";
                
                break;
            case GameType.运球:
                if(CrossSceneManager.instance.crossType == CrossType.脚内侧运球)
                {
                    type = "(脚内侧)运球训练";
                    scoreStr = "4(4)";
                }
                else if(CrossSceneManager.instance.crossType == CrossType.正脚背运球)
                {
                    type = "(正脚背)运球训练";
                    scoreStr = "4(4)";
                }
                else if (CrossSceneManager.instance.crossType == CrossType.外脚背运球)
                {
                    type = "(外脚背)运球训练";
                    scoreStr = "4(4)";
                }
                break;
            case GameType.头球:
                type = "头球训练";
                break;
            case GameType.传球:
                type = "传球训练";
                break;
        }
 

        string level = String.Empty;
        switch (settingOffset.gameLevel)
        {
            case 1:
                level = "初级";
                break;
            case 2:
                level = "中级";
                break;
            case 3:
                level = "高级";
                break;

        }
        string d = String.Empty;
        string dir = type + "_" + UserData.currentTime + "_" + level + "_" + scoreStr;
        return dir;
    }

    
    public void StartGame()
    {
        switch (gameType)
        {
            case GameType.头球:
                break;
            case GameType.传球:
                break;
            case GameType.射门:
                break;
            case GameType.运球:
                break;
        }
    }
    public void Init()
    {
        GameStart = true;
        m_braffleCount = 0;
        m_goalCount = 0;
        m_fakeManCount = 0;
        gameTime = 0;
        Debug.Log("Start Game");
        StartCoroutine(RepairAndStartGame());
        
    }
    public IEnumerator RepairAndStartGame()
    {

        TipUI.instance.ShowProgressTip("正在初始化" + GameManager.instance.gameType + "训练,请双脚自然向前，目视前面，并站在原地保持不动。", 3f);
        yield return new WaitForSeconds(3f);
        foreach(var foot in snapFeet)
        {
            foot.SetFoot();
        }
        RecorderForAniAndVR.instance.SetStartInfo();
        TipUI.instance.ShowProgressTip(GetTipByType(), 5f); 
        yield return new WaitForSeconds(5f);
        switch (gameType)
        {
            case GameType.传球:
                PassSceneManager.instance.StartGame();
                break;
            case GameType.射门:
                ShootMananger.instance.StartGame();
                break;
            case GameType.头球:
                //fSM.SetState("Start");
                HeadSceneManager.instance.StartGame();
                break;
            case GameType.运球:
                CrossSceneManager.instance.StartGame();
                break;
        }
        //SettingPos();

    }
    public string GetTipByType()
    {
        switch (gameType)
        {
            case GameType.传球:
                return "5秒后将开始训练，训练内容：停好发球机来球再攻入小门";
            case GameType.射门:
                return "5秒后将开始训练，训练内容：用脚背内侧将球攻入球门";
            case GameType.头球:
                return "5秒后将开始训练，训练内容：用额头将发球机来球攻入球门";
            case GameType.运球:
                return "5秒后将开始训练，训练内容：通过用脚触碰地面两个方块生成足球，并运球往返标杆";
        }
        return null;
    }
    public void RequirStartSet(List<GameStartSet> gameStartSets)
    {
        for (int i = 0; i < gameStartSets.Count; i++)
        {
            if (gameStartSets[i].gameLevel == settingOffset.gameLevel)
            {
                gameStartSets[i].sceneObjectGroup.SetActive(true);
                Init();
            }
        }
    }
    //IEnumerator DelaySetPlayerPos(GameStartSet gameStartSet)
    //{
    //    yield return new WaitForSeconds(1f);
    //    Player.instance.transform.position = gameStartSet.playerPos.position;
    //    yield return new WaitForSeconds(1f);
    //    Player.instance.playerContr.position = gameStartSet.playerPos.position;
    //    Quaternion quaternion = Quaternion.LookRotation(gameStartSet.target.position - gameStartSet.playerPos.position);
    //    Player.instance.transform.rotation = quaternion;
    //}
    public void AddBraffle()
    {
        m_braffleCount += 1;
    }
    public void AddGoal()
    {
        m_goalCount += 1;
    }
    public void AddFakeMan()
    {
        m_fakeManCount += 1;
    }
    public void SetPause(bool status)
    {

    }

    public void AddScore(int num = 1)
    {
        score += num;
    }
    public void MinScore(int num = 1)
    {
        score -= num;
    }
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update

    public float GetBallLifeTime()
    {
        switch (gameType)
        {
            case GameType.传球:
                return 3f;
            case GameType.射门:
                return 5f;
            case GameType.头球:
                return 5f;
            case GameType.运球:
                return 10f;
        }
        return 3f;
    }
    public void RestartFlow()
    {
        switch (gameType)
        {
            case GameType.传球:
                PassSceneManager.instance.StartGame();
                break;
            case GameType.射门:
                ShootMananger.instance.StartGame();
                break;
            case GameType.头球:
                HeadSceneManager.instance.StartGame();
                break;
            case GameType.运球:
                CrossSceneManager.instance.StartGame();
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
      
        //if (GameStart)
        //{
        //    CheckFootStatus();
        //}
        gameTime += Time.deltaTime;

        //if (InputBridge.Instance.YButtonUp)
        //{
        //    JumpSceneByIndex();
        //}
    }
    public void NextWithScore(int num)
    {
        AddScore(num);
        //fSM.SendEvent("Next");
    }
    public void Next()
    {
        //fSM.SendEvent("Next");
    }
    public void NextWithWaitAndScore(float time, int score = 0)
    {
        StartCoroutine(NextIE(time, score));
    }
    IEnumerator NextIE(float time, int score)
    {
        yield return new WaitForSeconds(time);
        NextWithScore(score);
    }
    public void QuitGame() 
    {
        Application.Quit();
    } 
 
    private void OnApplicationQuit()
    {
    }
    void SpawnBall()
    {
        var ball = Instantiate(ballObject, ballStartPos.position, ballStartPos.rotation);
    }
    public void ResetCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public Actor Build(int subjectId, string subjectName, Dictionary<string, string> metadata)
    {
        throw new NotImplementedException();
    }

    public void OnCustomEvent(EliCDavis.RecordAndPlay.SubjectRecording subject, EliCDavis.RecordAndPlay.CustomEventCapture customEvent)
    {
        throw new NotImplementedException();
    }
}
public enum GameType
{
    传球,
    射门,
    头球,
    运球,
    测试
}
[System.Serializable]
public class GameStartSet
{
    public Transform playerPos;
    public int gameLevel;
    public PlayMakerFSM fSM;
    public Transform target;
    public GameObject sceneObjectGroup;

}
[System.Serializable]
public class ResultMessage
{
    public string SN;
    public string trainningType;
    public string level;
    public string result;
    public string startTime;
    public string endtime;
}

public class Course
{
    public DateTime startTime;
    public DateTime endTime;
    public int subjectId;
    public string userName;
    public int levelId;
    public int schoolId;
    public int score;
    public string result;
    public int id;
}