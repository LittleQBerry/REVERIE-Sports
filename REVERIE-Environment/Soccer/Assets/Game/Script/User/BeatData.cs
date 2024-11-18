using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BeatData
{
    public int beatIndex;
    public int usefulBeat;//0无效Beat，1有效Beat
    public float beatStartTime { get; set; }
    public float beatEndTime { get; set; }

    public List<CheckResult> results;
    public List<float> force;

    public void SetBeatStartTime(float _beatStartTime, int _beatIndex)
    {
        beatStartTime = _beatStartTime;
        beatIndex = _beatIndex;
    }
    public void SetBeatEndTime(float _beatEndTime)
    {
        beatEndTime = _beatEndTime;
    }
    public void SetBeatData(int _ruleId, string _result, string _reason, float v1, float v2, int _index, int isWrong)
    {
        if (results == null)
        {
            results = new List<CheckResult>();
            CheckResult checkResult = new CheckResult();
            checkResult.result = _result;
            checkResult.ruleId = _ruleId;
            Reason reason = new Reason();
            reason.reason = _reason;
            reason.value1 = v1;
            reason.value2 = v2;
            reason.timing = Time.time;
            checkResult.reasons = new List<Reason>();
            checkResult.reasons.Add(reason);
            checkResult.beatIndex = _index;
            checkResult.isWrong = isWrong;
            results.Add(checkResult);
        }
        else
        {
            bool hasResult = false;
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].result == _result)
                {
                    Reason reason = new Reason();
                    reason.reason = _reason;
                    reason.value1 = v1;
                    reason.value2 = v2;
                    reason.timing = Time.time;
                    results[i].beatIndex = _index;
                    results[i].reasons.Add(reason);
                    hasResult = true;
                }
            }
            if (!hasResult)
            {
                CheckResult checkResult = new CheckResult();
                checkResult.result = _result;
                checkResult.ruleId = _ruleId;
                Reason reason = new Reason();
                reason.reason = _reason;
                reason.value1 = v1;
                reason.value2 = v2;
                reason.timing = Time.time;
                checkResult.beatIndex = _index;
                checkResult.isWrong = isWrong;
                checkResult.reasons = new List<Reason>
                {
                    reason
                };
                results.Add(checkResult);
            }
        }
    }
    public BeatData(float btStartTime, int _index)
    {
        beatStartTime = btStartTime;
        beatIndex = _index;
        force = new List<float>();
    }
}
public class CheckResult
{
    public int ruleId;
    public int beatIndex;
    public int type;
    public int isWrong;
    public string result;
    public List<Reason> reasons;
}
public class Reason
{
    public string reason;
    public float value1;
    public float value2;
    public float timing;
}
public class GameData
{
    public int currentIndex;
    public int newIndex;
    public List<BeatData> beats;
    public float startTime;
    public void AddBeatForce(float _force)
    {
        if (beats.Count > 0)
        {
            beats[currentIndex].force.Add(_force); //(ruleId, _resutlt, _reason, _v1, _v2, currentIndex, isWrong);
        }
    }
    public void AddNewBeatData(float _time)
    {
        currentIndex = newIndex;
        beats.Add(new BeatData(_time, currentIndex));

        newIndex++;
    }
    public void SetBeatData(int ruleId, string _resutlt, string _reason, float _v1, float _v2, int isWrong)
    {
        if (beats.Count > 0)
        {
            beats[currentIndex].SetBeatData(ruleId, _resutlt, _reason, _v1, _v2, currentIndex, isWrong);
        }
    }
    public void SetBeatState(bool useful)
    {
        if (useful) beats[currentIndex].usefulBeat = 1;
    }
    public void SetBeatEnd(float endTime)
    {
        if (beats != null && beats.Count > 0)
        {
            beats[currentIndex].beatEndTime = endTime;
        }
    }
    public GameData(float time)
    {
        currentIndex = 0;
        newIndex = 0;
        startTime = time;
        beats = new List<BeatData>();
    }
}