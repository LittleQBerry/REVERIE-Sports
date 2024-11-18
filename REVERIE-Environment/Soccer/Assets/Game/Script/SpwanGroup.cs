using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanGroup : MonoBehaviour
{
    public int count = 3;
    public SpawnPoint[] spawnPoints;

    public void RandomSpawn()
    {
        spawnPoints[0].OnInit();
        //int index = Random.Range(0, spawnPoints.Length-1);
        //spawnPoints[index].OnInit();
    }

}
