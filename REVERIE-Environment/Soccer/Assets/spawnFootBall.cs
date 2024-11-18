using EliCDavis.RecordAndPlay.Record;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using BNG;
public class spawnFootBall : MonoBehaviour
{
    public GameObject footba;
    Vector3 startpos;
    Transform tra;
    public float v = 3;
   
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        //SpawnBall();
    }
    void SpawnBall()
    {
        RecorderForAniAndVR.instance.gameData.AddNewBeatData(Time.time);
        RecorderForAniAndVR.instance.NewIndexAndTime();
        var ba = Instantiate(footba);
        ba.transform.position = transform.position;
        tra = ba.transform;
        BallRecordBehavior.Build(SubjectBehavior.Build(ba, RecorderForAniAndVR.instance.recorder, "FootBall"));
        ShootMananger.instance.AddBallCount();
    }
    bool canSpwanBall = true;
    // Update is called once per frame
    void Update()
    {
        if (ShootMananger.instance.gameStart)
        {
            if (tra != null)
            {
                if (Vector3.Distance(tra.position, startpos) > v && canSpwanBall)
                {
                    canSpwanBall = false;
                    StartCoroutine(DelaySpawnBall());
                }
            }
            else
            {
                SpawnBall();
            }
        }
    }
    IEnumerator DelaySpawnBall()
    {
        yield return new WaitForSeconds(1.5f);
        RecorderForAniAndVR.instance.gameData.SetBeatEnd(Time.time);
        SpawnBall();
        canSpwanBall = true;
    }
}
