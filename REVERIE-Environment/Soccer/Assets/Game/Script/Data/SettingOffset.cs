using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SettingOffset")]
public class SettingOffset : ScriptableObject
{
    [Header("���ɵĽ���ǰ�ƶ����پ���")]
    public float forwardFootOffset = 0.05f;
    [Header("�ŵ��ٶȴﵽ�����ֵ�Ժ����ӵ���")]
    public float footBounceChangeThreshold = 6f;
    [Header("������ĵ���")]
    public List<BallAttr> ballAttrs;
    [Header("���������������")]
    public List<BallLifeTime> ballLifeTimes;
    public FootBounceseMode footBounceseMode;

    [Header("�����ŽǶȵķ�Χ")]
    public float angleA_min = 15;
    public float angleA_max = 70;
    public float angleB_min = 110;
    public float angleB_max = 165;
    [Header("�����ŵ��ٶ���ֵ")]
    public float bigFootPowerTreshold = 6f;
    [Header("�����ŵ�������ֵ")]
    public float bigFootOffset = 1.5f;
    [Header("ͷ������ƫ��ֵ")]
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