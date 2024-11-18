using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RuleAttr : ScriptableObject
{
    public List<Rule> rules;
}
[System.Serializable]
public class Rule
{
    public int ruleID;
    public RuleFor ruleFor;
    public string ruleName;
    public string ruleResult;
    public RuleType ruleType;
    public float minValue;
    public float maxValue;
    public bool isActive;
    public string ruleDesc;
    public Stage stage;
}
public enum Stage
{
    Ready,
    Hit,
    All
}
public enum RuleType
{
    Angle,
    Distance,
    Custom
}
public enum RuleFor
{
    ForeHand,
    BackHand,
    TwoOneHand,
    LRHand,
    All,
    Send,
    Shoot,
    Cross,
    Head,
    Pass
}
