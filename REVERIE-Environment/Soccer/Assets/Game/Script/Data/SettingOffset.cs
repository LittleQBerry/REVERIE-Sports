using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SettingOffset")]
public class SettingOffset : ScriptableObject
{
    [Header("生成的脚向前移动多少距离")]
    public float forwardFootOffset = 0.05f;
    [Header("脚的速度达到这个阈值以后增加弹力")]
    public float footBounceChangeThreshold = 6f;
    [Header("所有球的弹性")]
    public List<BallAttr> ballAttrs;
    [Header("所有球的生命周期")]
    public List<BallLifeTime> ballLifeTimes;
    public FootBounceseMode footBounceseMode;

    [Header("大力脚角度的范围")]
    public float angleA_min = 15;
    public float angleA_max = 70;
    public float angleB_min = 110;
    public float angleB_max = 165;
    [Header("大力脚的速度阈值")]
    public float bigFootPowerTreshold = 6f;
    [Header("大力脚的力量插值")]
    public float bigFootOffset = 1.5f;
    [Header("头球力量偏差值")]
    public float headForceOffset = 1.5f;

    public int gameLevel = 1;
}
[System.Serializable]
public class BallAttr
{
    public float bounce;
    public float mass;
    public float drag;
    public float angelDrag;
    public GameType gameType;
    public bool hasBigFoot;
    public PhysicMaterialCombine combineMode;
}
[System.Serializable]
public class BallLifeTime
{
    public GameType gameType;
    public int level;
    public float lifeTime;
}

public enum FootBounceseMode
{
    Threshold,
    ByVelocity
}