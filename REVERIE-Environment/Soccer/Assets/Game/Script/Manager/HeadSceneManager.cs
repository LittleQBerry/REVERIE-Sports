using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSceneManager : MonoBehaviour
{
    public static HeadSceneManager instance;
    public List<GameStartSet> gameStartSets;

    public int count;
    public int maxCount;
    public bool isStartGame;
    private void Awake()
    {
        instance = this;
    }
    
    public void AddCount()
    {
        if (count != 0)
        {
            RecorderForAniAndVR.instance.gameData.SetBeatEnd(Time.time);
        }
        count ++;
        RecorderForAniAndVR.instance.gameData.AddNewBeatData(Time.time);
        RecorderForAniAndVR.instance.NewIndexAndTime();
    }
    public void StartGame() 
    {
        for (int i = 0; i < gameStartSets.Count; i++)
        {
            if (gameStartSets[i].gameLevel == GameManager.instance.settingOffset.gameLevel)
            {
                Debug.Log("StartGame");
                gameStartSets[i].sceneObjectGroup.SetActive(true);
                gameStartSets[i].sceneObjectGroup.transform.GetChild(0).GetComponent<SpawnPoint>().isAuto = true;
            }
            else
            {
                gameStartSets[i].sceneObjectGroup.SetActive(false);
            }
        }
        isStartGame = true;
        RecorderForAniAndVR.instance.StartRecording();
    }
    // Start is called before the first frame update
    void Start()
    {
        //StartGame();
        GameManager.instance.RequirStartSet(gameStartSets);
    }

    // Update is called once per frame
    void Update()
    {
        if(count>maxCount&& isStartGame)
        {
            isStartGame = false;
            FinishGame();
        }

    }

    void FinishGame() 
    {
        RecorderForAniAndVR.instance.FinishRecording();
    }
}
