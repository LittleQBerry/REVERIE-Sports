using EliCDavis.RecordAndPlay.Record;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeadEye : MonoBehaviour
{
    private Recorder recorder;
    private SubjectBehavior subjectBehavior;

    public void Initialize(Recorder recorder)
    {
        this.recorder = recorder;
        subjectBehavior = SubjectBehavior.Build(gameObject, recorder, 5, "HeadEye", new Dictionary<string, string>() { { "Bullets Fired", "0" } }, 0.0001f);
    }
}
