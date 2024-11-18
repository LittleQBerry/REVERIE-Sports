using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassSceneManager : MonoBehaviour
{
    public static PassSceneManager instance;
    public List<GameStartSet> gameStartSets;
    public GameObject cycle;
    public int count = 0;
    public int maxCount = 20;
    public bool startGame = false;
    public Transform gate;
    private void Awake()
    {
        instance = this;

    }
    public void AddBallCount(int _count)
    {
        if(count != 0)
        {
            RecorderForAniAndVR.instance.gameData.SetBeatEnd(Time.time);
        }
        count += _count;
        RecorderForAniAndVR.instance.gameData.AddNewBeatData(Time.time);
        RecorderForAniAndVR.instance.NewIndexAndTime();
    }
    private void Start()
    {
        GameManager.instance.RequirStartSet(gameStartSets);
        count = 0;
        startGame = true;
    }
 
    public void StartGame()
    {
        for (int i = 0; i < gameStartSets.Count; i++)
        {
            if (gameStartSets[i].gameLevel == GameManager.instance.settingOffset.gameLevel)
            {
                gameStartSets[i].sceneObjectGroup.SetActive(true);
                gameStartSets[i].target.GetComponent<SpawnPoint>().isAuto = true;
            }
            else
            {
                gameStartSets[i].sceneObjectGroup.SetActive(false);
            }
        }
        RecorderForAniAndVR.instance.StartRecording(); 
    }
    // Update is called once per frame
    void Update()
    {
        if(count >maxCount&& startGame)
        {
            startGame = false;
            GameFinshed();
        }
    }
    public void GameFinshed()
    {
        RecorderForAniAndVR.instance.FinishRecording();
    }
}
