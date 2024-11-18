using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;
[RequireComponent(typeof(HighlightTrigger))]
public class DetectObj : MonoBehaviour
{
    public bool alwayExistObj;
    HighlightTrigger highlight;
    public bool onState = false;
    public float stateTime = 0f;
    public string stateName;
    // Start is called before the first frame update
    void Start()
    {
        stateTime = 0f;
        highlight = GetComponent<HighlightTrigger>();
    }
    private void Update()
    {
        if (onState)
        {
            stateTime += Time.deltaTime;
        }
    }

    public void OnStateStart()
    {
        onState = true;
        highlight.Highlight(true);
    }
    public void OnStateFinish()
    {
        highlight.Highlight(false);
        onState = false;
        stateTime = 0;
    }

}
