using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMananger : MonoBehaviour
{
    public static ShootMananger instance;
    public List<GameStartSet> gameStartSets;
    public int count = 0;
    public int maxBallCount = 20;
    public bool gameStart = false;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        count = 0;
        GameManager.instance.RequirStartSet(gameStartSets);
    }

    void Start()
    {
       
    }
    public void AddBallCount()
    {
        count += 1;
    }

    public void StartGame()
    {
        //Debug.Log("StartShoot");
        gameStart = true;
        for (int i = 0; i < gameStartSets.Count; i++)
        {
            if (gameStartSets[i].gameLevel == GameManager.instance.settingOffset.gameLevel)
            {
                gameStartSets[i].sceneObjectGroup.SetActive(true);
                if(GameManager.instance.settingOffset.gameLevel == 2|| GameManager.instance.settingOffset.gameLevel == 3)
                {
                    gameStartSets[i].sceneObjectGroup.transform.GetChild(0).GetComponent<SpawnPoint>().isAuto = true;
                }
                //gameStartSets[i].target.GetComponent<SpawnPoint>().isAuto = true;
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
        if(count>maxBallCount&& gameStart)
        {
            gameStart = false;
            GameFinish();
        }
    }
    void GameFinish() 
    { 
        RecorderForAniAndVR.instance.FinishRecording();
    }

}
