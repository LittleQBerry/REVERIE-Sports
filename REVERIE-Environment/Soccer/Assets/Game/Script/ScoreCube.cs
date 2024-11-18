using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCube : MonoBehaviour
{
    public int scoreNum;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (!other.GetComponent<CollisionCheck>().hasAddCount)
            {
                GameManager.instance.AddScore(1);
                GameManager.instance.AddGoal();
                other.GetComponent<CollisionCheck>().hasAddCount = true; 
            }
            
        }
    }
    
}
