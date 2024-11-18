using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            GameManager.instance.NextWithWaitAndScore(3,3);
            GameManager.instance.AddGoal();
        }
    }
}
