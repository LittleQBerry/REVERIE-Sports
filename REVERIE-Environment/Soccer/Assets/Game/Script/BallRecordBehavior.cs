using EliCDavis.RecordAndPlay.Record;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRecordBehavior : MonoBehaviour
{
    SubjectBehavior subject;

    public static BallRecordBehavior Build(SubjectBehavior subject)
    {
        BallRecordBehavior bullet;
        if (subject.gameObject.GetComponent<BallRecordBehavior>() != null)
        {
            bullet = subject.gameObject.GetComponent<BallRecordBehavior>();
        }
        else
        {
            bullet = subject.gameObject.AddComponent<BallRecordBehavior>();
        }
        
        bullet.subject = subject;
        return bullet;
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
