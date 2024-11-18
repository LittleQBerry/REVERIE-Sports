using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObj : MonoBehaviour
{
    public List<Transform> spawnPoints;

    public void OnInit()
    {
        DisableAllObj();
    }
    void DisableAllObj()
    {
        if(spawnPoints!= null&&spawnPoints.Count == 0)
        {
            foreach(var trn in spawnPoints)
            {
                trn.gameObject.SetActive(false);
            }
        } 
    }

    public void SpwanObj()
    {
        if (spawnPoints == null || spawnPoints.Count == 0) return;
        spawnPoints[Random.Range(0, spawnPoints.Count)].gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
