using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RocketTest>())
        {
            Debug.Log("ตรทึ");
            RocketTest ball = other.GetComponent<RocketTest>();
            if (!ball.addTrainingScore && ball.lastHitTableCollider != null && ball.lastHitTableCollider.playerSide && ball.m_curBallState == BallState.BS_Start)
            {
                ball.addTrainingScore = true;
                //GameManager.Singleton.AddTrainingScore(1);
            }
        }
    }
}
