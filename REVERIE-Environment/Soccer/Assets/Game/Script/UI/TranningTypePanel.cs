using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TranningTypePanel : MonoBehaviour
{
    public int level = 1;
    //public TrainingType trainingType;
    public ToggleGroup toggleGroup;
    public void SetTraning()
    {
        bool doubleNum = false;
        for(int i=0;i< toggleGroup.transform.childCount; i++)
        {
            if(toggleGroup.transform.GetChild(i).name == "100"&& toggleGroup.transform.GetChild(i).GetComponent<Toggle>().isOn)
            {
                doubleNum = true;
            }
        }
        //if(trainingType == TrainingType.TT_Forehand && GameManager.Singleton.cur_handdata.current_handType == HandType.left)
        //{
        //    GameManager.Singleton.StartGame(GameManager.Singleton.m_curGameType, TrainingType.TT_BackhandPush, level, doubleNum);
        //    return;
        //}
        //if (trainingType == TrainingType.TT_BackhandPush && GameManager.Singleton.cur_handdata.current_handType == HandType.left)
        //{
        //    GameManager.Singleton.StartGame(GameManager.Singleton.m_curGameType, TrainingType.TT_Forehand, level, doubleNum);
        //    return;
        //}
        //GameManager.Singleton.StartGame(GameManager.Singleton.m_curGameType, trainingType, level, doubleNum);

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
