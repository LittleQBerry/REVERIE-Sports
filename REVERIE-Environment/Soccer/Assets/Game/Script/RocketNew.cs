using EliCDavis.RecordAndPlay.Record;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RocketNew : MonoBehaviour
{
    public bool isPlayer = false;
    //public PingPongNewAI PingPongNewAI;
    public Transform centerPoint;
    Rigidbody rg;
    public bool canHitBall =true;
    private Recorder recorder;
    private SubjectBehavior subjectBehavior;
    public Transform colliderObj;
    public void Initialize(Recorder recorder)
    {

        this.recorder = recorder;
        subjectBehavior = SubjectBehavior.Build(gameObject, recorder, 5, "Rocket", new Dictionary<string, string>() { { "ss", "0" } }, 0.0001f);
    }
    private void Awake()
    {
        rg = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //if (centerPoint)
        //{
        //    rg.centerOfMass = centerPoint.position;
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<RocketTest>())
        {
            if (!isPlayer)
            {
                //if(canHitBall)
                   //PingPongNewAI.HitBall(other.gameObject.GetComponent<Rigidbody>());
            }
            else
            {
                
                GetComponent<Collider>().isTrigger = true;
                InvokeCollider();
            }

        }
        
    }
    public void InvokeCollider()
    {
        //Invoke("DisableCollider", 0.1f);
        StartCoroutine(DisableCollider(0.1f));
    }

    IEnumerator DisableCollider(float _time)
    {
        yield return new WaitForSeconds(_time);
        GetComponent<Collider>().isTrigger = false;
    }
}
