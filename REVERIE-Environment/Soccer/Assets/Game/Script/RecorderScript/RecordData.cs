using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "RecordData")]
public class RecordData : ScriptableObject
{
    public List<RecordDetail> recordDetails;
}
[System.Serializable]
public class RecordDetail
{
    public string trainingType;
    public float beforeTime;
    public float afterTime;
    public string animationTrigger;
    public string otherAniTrigger;
    public List<JointObj> jointObj;
}

public enum JointObj
{
    head,
    leftShoulder,
    rightShoulder,
    spine,
    hip,
    leftArm,
    rightArm,
    leftForeArm,
    rightForeArm,
    leftHand,
    rightHand,
    leftUpLeg,
    rightUpLeg,
    leftLeg,
    rightLeg,
    leftFoot,
    rightFoot
}