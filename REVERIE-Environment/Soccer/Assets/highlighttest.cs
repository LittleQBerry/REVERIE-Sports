using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;


[RequireComponent(typeof(HighlightTrigger))]
public class highlighttest : MonoBehaviour
{
    HighlightTrigger highlightTrigger;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        highlightTrigger = GetComponent<HighlightTrigger>();
        highlightTrigger.Highlight(true);
        timer = 0;
    }
    public void tt() 
    {
        highlightTrigger.Highlight(false);
        timer = 0;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //if (timer > 3f)
        //{
        //    highlightTrigger.Highlight(true);
        //}
        if(timer > 10f)
        {
            highlightTrigger.Highlight(true);
            timer = 0;
        }
    }
}
