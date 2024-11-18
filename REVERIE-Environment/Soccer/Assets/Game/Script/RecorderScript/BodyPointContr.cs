using EliCDavis.RecordAndPlay.Record;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPointContr : MonoBehaviour
{
    [Tooltip("躯干")]
    public GameObject head;
    public GameObject hip;
    public GameObject leftShoulder;
    public GameObject rightShoulder;
    public GameObject spine;
    [Tooltip("手臂")]
    public GameObject leftArm;
    public GameObject leftForeArm;
    public GameObject lefthand;
    public GameObject rightArm;
    public GameObject rightForeArm;
    public GameObject righthand;
    [Tooltip("腿")]
    public GameObject leftUpLeg;
    public GameObject leftLeg;
    public GameObject leftFoot;
    public GameObject rightUpLeg;
    public GameObject rightLeg;
    public GameObject rightFoot;


    [Tooltip("躯干演员")]
    public GameObject headActor;
    public GameObject hipActor;
    public GameObject leftShoulderActor;
    public GameObject rightShoulderActor;
    public GameObject spineActor;
    [Tooltip("手臂演员")]
    public GameObject leftArmActor;
    public GameObject leftForeArmActor;
    public GameObject lefthandActor;
    public GameObject rightArmActor;
    public GameObject rightForeArmActor;
    public GameObject righthandActor;
    [Tooltip("腿演员")]
    public GameObject leftUpLegActor;
    public GameObject leftLegActor;
    public GameObject leftFootActor;
    public GameObject rightUpLegActor;
    public GameObject rightLegActor;
    public GameObject rightFootActor;

    private Recorder recorder;

    private SubjectBehavior subjectBehavior;

    public void Initialize(Recorder recorder)
    {
        this.recorder = recorder;
        SubjectBehavior.Build(head, recorder, 5, "head", new Dictionary<string, string>() { { "", "0" } }, 0.0001f);
        SubjectBehavior.Build(hip, recorder, 5, "hip", new Dictionary<string, string>() { { "", "0" } }, 0.0001f);
        SubjectBehavior.Build(leftShoulder, recorder, 5, "leftShoulder", new Dictionary<string, string>() { { "", "0" } }, 0.0001f);
        SubjectBehavior.Build(rightShoulder, recorder, 5, "rightShoulder", new Dictionary<string, string>() { { "", "0" } }, 0.0001f);
        SubjectBehavior.Build(spine, recorder, 5, "spine", new Dictionary<string, string>() { { "", "0" } }, 0.0001f);

        SubjectBehavior.Build(leftArm, recorder, 5, "leftArm", new Dictionary<string, string>() { { "", "0" } }, 0.0001f);
        SubjectBehavior.Build(leftForeArm, recorder, 5, "leftForeArm", new Dictionary<string, string>() { { "", "0" } }, 0.0001f);
        SubjectBehavior.Build(lefthand, recorder, 5, "lefthand", new Dictionary<string, string>() { { "", "0" } }, 0.0001f);
        SubjectBehavior.Build(rightArm, recorder, 5, "rightArm", new Dictionary<string, string>() { { "", "0" } }, 0.0001f);
        SubjectBehavior.Build(rightForeArm, recorder, 5, "rightForeArm", new Dictionary<string, string>() { { "", "0" } }, 0.0001f);
        SubjectBehavior.Build(righthand, recorder, 5, "righthand", new Dictionary<string, string>() { { "", "0" } }, 0.0001f);
        
        SubjectBehavior.Build(leftUpLeg, recorder, 5, "leftUpLeg", new Dictionary<string, string>() { { "", "0" } }, 0.0001f);
        SubjectBehavior.Build(leftLeg, recorder, 5, "leftLeg", new Dictionary<string, string>() { { "", "0" } }, 0.0001f);
        SubjectBehavior.Build(leftFoot, recorder, 5, "leftFoot", new Dictionary<string, string>() { { "", "0" } }, 0.0001f);
        SubjectBehavior.Build(rightUpLeg, recorder, 5, "rightUpLeg", new Dictionary<string, string>() { { "", "0" } }, 0.0001f);
        SubjectBehavior.Build(rightLeg, recorder, 5, "rightLeg", new Dictionary<string, string>() { { "", "0" } }, 0.0001f);
        SubjectBehavior.Build(rightFoot, recorder, 5, "rightFoot", new Dictionary<string, string>() { { "", "0" } }, 0.0001f);
    }
    // Start is called before the first frame update
    void Start()
    {
        angleQueue = new Queue<float>(60);
        leftLegAngleQueue =  new Queue<float>(60);
        rightLegAngleQueue = new Queue<float>(60);

        for (int i = 0; i < 60; i++)
        {
            angleQueue.Enqueue(0f);
            leftLegAngleQueue.Enqueue(0f);
            rightLegAngleQueue.Enqueue(0f);
        }
    }
    public float GetRightFootAndLegAngle()
    {
        // 计算该向量与脚正前方向量之间的夹角
        float angle = Vector3.Angle(rightLeg.transform.up, rightFoot.transform.forward);
        return angle;
    }
    public float GetRightFootInnerAngle()
    {
        float angle = Vector3.Angle(-hip.transform.right, rightFoot.transform.forward);
        return 90 - angle;
    }
    float lgtimer;
    float rgtimer;
    public bool CheckLegStreetForHead()
    {
        Vector3 upperLegDirection = leftUpLeg.transform.position - leftLeg.transform.position;

        float leftelbowAngle = Vector3.Angle(leftUpLeg.transform.up, leftLeg.transform.up);

        Vector3 rightLegDirection = rightUpLeg.transform.position - rightLeg.transform.position;

        float rightelbowAngle = Vector3.Angle(rightUpLeg.transform.up, rightLeg.transform.up);

        RecorderForAniAndVR.instance.DealRuleForBeat(49, leftelbowAngle, "", rightelbowAngle);
       
        return false;
    }
    public bool CheckLegStreet()
    {
        Vector3 upperLegDirection = leftUpLeg.transform.position - leftLeg.transform.position;

        float leftelbowAngle = Vector3.Angle(leftUpLeg.transform.up, leftLeg.transform.up);

        Vector3 rightLegDirection = rightUpLeg.transform.position - rightLeg.transform.position;

        float rightelbowAngle = Vector3.Angle(rightUpLeg.transform.up, rightLeg.transform.up);

        RecorderForAniAndVR.instance.DealRuleForBeat(43, leftelbowAngle,"", rightelbowAngle);
        //bool isLeftLegBent = leftelbowAngle < 5.0f;
        //bool isRightLegBent = rightelbowAngle < 5.0f;
        //if (isLeftLegBent)
        //{
        //    RecorderForAniAndVR.instance.AddBeats("腿部僵直", "左腿僵直：" + rightelbowAngle, rightelbowAngle);
        //    //lgtimer += Time.deltaTime;
        //    //if (lgtimer > 1)
        //    //{
        //    //    RecorderForAniAndVR.instance.AddBeats("腿部僵直", "左腿僵直：" + rightelbowAngle, rightelbowAngle);
        //    //    lgtimer = 0;
        //    //}
        //}
        //else
        //{
        //    lgtimer = 0;
        //}
        //if (isRightLegBent)
        //{
        //    RecorderForAniAndVR.instance.AddBeats("腿部僵直", "右腿僵直：" + rightelbowAngle, rightelbowAngle);
        //    //rgtimer += Time.deltaTime;
        //    //if (rgtimer > 1)
        //    //{
                
        //    //    rgtimer = 0;
        //    //}

        //}
        //else
        //{
        //    rgtimer = 0;
        //}
        return false;
    }


    float ltimer;
    float rtimer;
    //public bool CheckArmStreet()
    //{
    //    Vector3 upperArmDirection = leftArm.transform.position - leftForeArm.transform.position;
        
    //    float leftelbowAngle = Vector3.Angle(upperArmDirection, leftForeArm.transform.right);

    //    Vector3 rightArmDirection = rightArm.transform.position - rightForeArm.transform.position;

    //    float rightelbowAngle = Vector3.Angle(rightArmDirection, rightForeArm.transform.right);

    //    bool isLeftArmBent = leftelbowAngle < 10.0f;
    //    bool isRightArmBent = rightelbowAngle < 10.0f;
    //    if (isLeftArmBent)
    //    {
    //        ltimer += Time.deltaTime;
    //        if (ltimer > 2)
    //        {
    //            RecorderForAniAndVR.instance.AddBeats("手臂僵直","左臂僵直：" + leftelbowAngle, leftelbowAngle);
    //            ltimer = 0;
    //        }
    //    }
    //    else
    //    {
    //        ltimer = 0;
    //    }
    //    if (isRightArmBent) 
    //    {
    //        rtimer += Time.deltaTime;
    //        if(rtimer > 2)
    //        {
    //            RecorderForAniAndVR.instance.AddBeats("手臂僵直","右臂僵直：" + rightelbowAngle, rightelbowAngle);
    //            rtimer = 0;
    //        }
           
    //    }
    //    else
    //    {
    //        rtimer = 0;
    //    }
    //    return isLeftArmBent;
    //}
    float cd;
    public void CheckFootDistance()
    {
        float distance = Vector3.Distance(Player.instance.leftFoot.transform.position, Player.instance.rightFoot.transform.position);
        RecorderForAniAndVR.instance.DealRuleForBeat(44, distance, "", 0);

    }
    public float maxAngle;
    private Queue<float> angleQueue;


    public float leftLegMaxAngle;
    private Queue<float> leftLegAngleQueue;
    public float rightLegMaxAngle;
    private Queue<float> rightLegAngleQueue;
    void UpdateQueue()
    {
        float angle = Vector3.Angle(spine.transform.up, Vector3.up);
        angleQueue.Enqueue(angle);
        angleQueue.Dequeue();
        maxAngle = CalculateMaxAngle(angleQueue);

        float lAngle = Vector3.Angle(leftUpLeg.transform.position - leftLeg.transform.position, leftLeg.transform.up);
        leftLegAngleQueue.Enqueue(lAngle);
        leftLegAngleQueue.Dequeue();
        leftLegMaxAngle = CalculateMaxAngle(leftLegAngleQueue);

        float rAngle = Vector3.Angle(rightUpLeg.transform.position - rightLeg.transform.position, rightLeg.transform.up);
        rightLegAngleQueue.Enqueue(rAngle);
        rightLegAngleQueue.Dequeue();
        rightLegMaxAngle = CalculateMaxAngle(rightLegAngleQueue);

    }
    public float CalculateMaxAngle(Queue<float> queue)
    {
        float max = 0f;
        foreach (float angle in queue)
        {
            if (angle > max)
            {
                max = angle;
            }
        }
        return max;
    }
    void CheckArmStreetForTest()
    {
        Vector3 rightArmDirection = rightArm.transform.position - rightForeArm.transform.position;

        float rightelbowAngle = Vector3.Angle(rightArmDirection, rightForeArm.transform.right);
        Debug.Log("rightelbowAngle:" + rightelbowAngle);
    }
    // Update is called once per frame
    void Update()
    {
        GetRightFootAndLegAngle();
        //CheckArmStreetForTest();
        if (GameManager.instance != null)
        {
            if(GameManager.instance.gameType == GameType.运球)
            {
                //CheckArmStreet();
                //CheckLegStreet();
                //CheckFootDistance();
            }
            if(GameManager.instance.gameType == GameType.头球)
            {
                UpdateQueue();
            }
            
        }
     
     
    }

}
