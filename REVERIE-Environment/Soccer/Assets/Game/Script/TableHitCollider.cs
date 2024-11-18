using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableHitCollider : MonoBehaviour
{
   public bool playerSide;

    public GameObject leftArea;
    public GameObject rightArea;



    //改用另外的Trigger判断，这里暂时不会被调用
    public void OnBallHit()
   {
       //if(GameManager.Singleton.m_curGameType == GameManager.GameType.GT_Training)
       //{
       //     if (!playerSide)
       //     {
       //         GameManager.Singleton.AddTrainingScore(1);
       //     }
       // }
    }
    private void Update()
    {
        //switch (GameManager.Singleton.m_curTrainingType)
        //{
        //    case TrainingType.TT_Forehand:
        //        SetLeftAreaActive();
        //        break;
        //    case TrainingType.TT_BackhandPush:
        //        SetRightAreaActive();
        //        break;
        //    case TrainingType.TT_TwoPointForehand:
        //        if (IsOdd(GameManager.Singleton.m_curBallNumber))
        //        {
        //            SetRightAreaActive();
        //        }
        //        else
        //        {
        //            SetLeftAreaActive();
        //        }
        //        break;
        //    case TrainingType.TT_PushLeftAttackRight:
        //        if (IsOdd(GameManager.Singleton.m_curBallNumber))
        //        {
        //            SetLeftAreaActive();
        //        }
        //        else
        //        {
        //            SetRightAreaActive();
        //        }
        //        break;

        //}
        //GameManager.Singleton.m_curTrainingType
    }
    //public void SetRightAreaActive()
    //{
    //    rightArea.SetActive(true);
    //    leftArea.SetActive(false);
    //}
    //public void SetLeftAreaActive()
    //{
    //    rightArea.SetActive(false);
    //    leftArea.SetActive(true);
    //}
    //public bool IsOdd(int num)
    //{
    //    if (num % 2 == 0)
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        return true;
    //    }
    //}
}
