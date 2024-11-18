using EliCDavis.RecordAndPlay.Record;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallContr : MonoBehaviour
{


    public float lifeTime;

    [Tooltip("Audio")]
    public AudioClip ballOnFootClip;
    public AudioClip ballOnFakeManClip;
    public AudioClip ballOnBaffleClip;
    public AudioClip ballOnGoalPostClip;
    public AudioClip ballOnBigFootClip;
    public PhysicMaterial physicMaterial;
    AudioSource audioSource;
    Rigidbody rigidbody;
    public float speedOffset = 2f;
    bool hasBigPower = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        
        GameManager.instance.CurrentBall = this.gameObject;
        lifeTime = MainSceneManager.instance.GetBallLifeTime();
        audioSource = GetComponent<AudioSource>();
        SetBallAttr(MainSceneManager.instance.GetBallAttr());
    }
    private Recorder recorder;
    private SubjectBehavior subjectBehavior;

    public void Initialize(Recorder recorder)
    {
        this.recorder = recorder;
        BallRecordBehavior.Build(SubjectBehavior.Build(this.gameObject, RecorderForAniAndVR.instance.recorder, "FootBall"));
    }
    private void SetBallAttr(BallAttr ballAttr)
    {
        if (ballAttr == null) return;
        physicMaterial.bounciness = ballAttr.bounce;
        rigidbody.mass = ballAttr.mass;
        rigidbody.drag = ballAttr.drag;
        rigidbody.angularDrag = ballAttr.angelDrag;
        hasBigPower = ballAttr.hasBigFoot;
        physicMaterial.bounceCombine = ballAttr.combineMode;
    }
    

    private void FixedUpdate()
    {

    }
    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        //GameManager.instance.RestartFlow();
        Destroy(this.gameObject);
    }
    public bool hasOnBaffle = false;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        
        if(GameManager.instance.gameType == GameType.´«Çò && GameManager.instance.settingOffset.gameLevel != 1)
        {
            if (collision.gameObject.CompareTag("Baffle"))
            {
                hasOnBaffle = true;
            }
        }

        if (collision.gameObject.CompareTag("Leg") )
        {
            var legrigid = collision.rigidbody;
            var speed = legrigid.velocity.magnitude;
            var legSetting = collision.gameObject.GetComponent<LegSetting>();
            if (hasBigPower&&legSetting.isHighWayMode&& legrigid.velocity.magnitude >= MainSceneManager.instance.settingOffset.bigFootPowerTreshold)// MainSceneManager.instance.settingOffset.footBounceChangeThreshold)
            {
                audioSource.PlayOneShot(ballOnBigFootClip); 
                rigidbody.velocity = legSetting.GetHighDir()* speed * MainSceneManager.instance.settingOffset.bigFootOffset;
            }
            lifeTime = Mathf.Clamp(lifeTime + 8, 0, 10);
           
            if (collision.gameObject.CompareTag("Leg"))
            {
                audioSource.PlayOneShot(ballOnFootClip);
            }
        }
        if (collision.gameObject.CompareTag("Head"))
        {
            //Vector3 normalDir = collision.contacts[0].normal;
            float headspeed = collision.gameObject.GetComponent<HeadContr>().speed;
            headspeed = Mathf.Clamp(headspeed, 0, 2);
            rigidbody.drag = 1 - headspeed*.5f;
            Debug.Log("rigidbody.drag"+rigidbody.drag);
            rigidbody.mass = 0.4f;

            //Debug.Log("headspeed:"+headspeed);
            //rigidbody.velocity = normalDir * headspeed * MainSceneManager.instance.settingOffset.headForceOffset;
            audioSource.PlayOneShot(ballOnFootClip);
            lifeTime = Mathf.Clamp(lifeTime + 3, 0, 6);
        }
        if (collision.gameObject.CompareTag("Baffle"))
        {
            lifeTime = Mathf.Clamp(lifeTime + 3, 0, 6);
            audioSource.PlayOneShot(ballOnBaffleClip);
            GameManager.instance.AddBraffle();
        }
        if(collision.gameObject.CompareTag("goal post"))
        {
            audioSource.PlayOneShot(ballOnGoalPostClip);
        }
        if (collision.gameObject.CompareTag("FakeMan"))
        {
            audioSource.PlayOneShot(ballOnFakeManClip);
            GameManager.instance.AddFakeMan();
        }
    }
}
