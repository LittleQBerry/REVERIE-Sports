using EliCDavis.RecordAndPlay.Record;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossSceneManager : MonoBehaviour
{
    public static CrossSceneManager instance;
    public PlayMakerFSM fSM;
    public int Score;
    public float timer;
    public Transform startPos;
    public Transform player;
    public Transform ballPos;
    public GameObject ball;
    public int time = 120; //设定倒计时时间
    public CrossType crossType;

    public Text t_time;
    public Text t_score;

    bool inGame = true;

    public List<GameStartSet> gameStartSets;


    public void StartGame()
    {

        RecorderForAniAndVR.instance.StartRecording();
        for (int i=0;i< gameStartSets.Count; i++)
        {
            if(gameStartSets[i].gameLevel == GameManager.instance.settingOffset.gameLevel)
            {
                if (gameStartSets[i].fSM != null)
                {
                    gameStartSets[i].fSM.gameObject.SetActive(true);
                    fSM = gameStartSets[i].fSM;
                }
                gameStartSets[i].sceneObjectGroup.SetActive(true);
            }
            else
            {
                if (gameStartSets[i].fSM != null)
                {
                    gameStartSets[i].fSM.gameObject.SetActive(false);
                    gameStartSets[i].sceneObjectGroup.SetActive(false);
                }
            }
        }
    }
    private void Start()
    {
        GameManager.instance.RequirStartSet(gameStartSets);//初始化 游戏 包含开始
    }

    private void Update()
    {
        //if (GameManager.instance.CurrentBall == null)
        //{
        //    var ballObj = Instantiate(ball);
        //    ballObj.transform.position = ballPos.position;
        //}
        //t_time.text = time.ToString();
        //t_score.text = Score.ToString();
    }
    public void SpwanBall()
    {
        var ballObj = Instantiate(ball);
        BallRecordBehavior.Build(SubjectBehavior.Build(ballObj, RecorderForAniAndVR.instance.recorder, "FootBall"));
        ballObj.transform.position = ballPos.position;
    }
    //private IEnumerator ChangeTime()
    //{
    //    while (time > 0)
    //    {
    //        yield return new WaitForSeconds(1);// 每次 自减1，等待 1 秒
    //        time--;
    //    }

    //    GameOver();
    //}
    //void GameOver()//gameOver
    //{ 
    //    inGame = false;
    //    RecorderForAniAndVR.instance.FinishRecording();
    //}
    private void Awake()
    {
        instance = this;
    }
    public void AddScore(int num)
    {
        if(inGame)
            Score += num;
    }
    int count = 0;
    public void Next()
    {
        if (count == 0)
        {
            RecorderForAniAndVR.instance.gameData.AddNewBeatData(Time.time);
            RecorderForAniAndVR.instance.gameData.SetBeatState(true);
        }
        else if (count == 4)
        {
            RecorderForAniAndVR.instance.gameData.SetBeatEnd(Time.time);
            RecorderForAniAndVR.instance.FinishRecording();
        }
        else if (count > 0 && count < 4)
        {
            RecorderForAniAndVR.instance.gameData.SetBeatEnd(Time.time);
            RecorderForAniAndVR.instance.gameData.AddNewBeatData(Time.time);
            RecorderForAniAndVR.instance.gameData.SetBeatState(true);
        }
        //if (count == 0)
        //{
        //    RecorderForAniAndVR.instance.gameData.AddNewBeatData(Time.time);
        //    RecorderForAniAndVR.instance.gameData.SetBeatState(true);
        //}
        //else if (count == 1)
        //{
        //    RecorderForAniAndVR.instance.gameData.SetBeatEnd(Time.time);
        //    RecorderForAniAndVR.instance.FinishRecording();
        //}

        fSM.SendEvent("Next");
        count++;
        //TipUI.instance.ShowDebugText(count.ToString());
    }
}
public enum CrossType
{
    正脚背运球,
    外脚背运球,
    脚内侧运球
}