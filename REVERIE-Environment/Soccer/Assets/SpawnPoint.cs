using EliCDavis.RecordAndPlay.Record;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool isRandomSpawnPoint = false;
    public bool isSingleBall = false;
    public GameObject football;
    public float offsetTime = 10f;
    float timer = 0f;
    public float force = 10f;
    public Transform muzzle;
    public Transform[] muzzles;
    public AudioClip fireClip;
    AudioSource audioSource;
    public bool isAuto = false;
    GameObject cur_fb;


    private Recorder recorder;
    private SubjectBehavior subjectBehavior;

    public void Initialize(Recorder recorder)
    {
        this.recorder = recorder;
        subjectBehavior = SubjectBehavior.Build(gameObject, recorder, 5, "BallShooter", new Dictionary<string, string>() { { "Bullets Fired", "0" } }, 0.0001f);
    }
    public void OnInit()
    {

        FirstSpawnBall();
    }
    public void StartByAuto()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAuto)
        {
            timer += Time.deltaTime;
            if (timer > offsetTime)
            {
                if (isRandomSpawnPoint)
                {
                    SpawnRandomBalls();
                }
                else
                {
                    SpawnBall();
                }
                timer = 0;
            }
        }
        
    }
    int randomInt = 0;
    void AddCount()
    {
        if(GameManager.instance.gameType == GameType.Í·Çò)
        {
            HeadSceneManager.instance.AddCount();
        }else if(GameManager.instance.gameType == GameType.´«Çò)
        {
            PassSceneManager.instance.AddBallCount(1);
        }
    }
    void SpawnRandomBalls()
    {
        if (isSingleBall && cur_fb != null) return;
        randomInt = Random.Range(0, 10);
        randomInt = randomInt % muzzles.Length;
        audioSource.PlayOneShot(fireClip);
        cur_fb = Instantiate(football);
        cur_fb.transform.position = muzzles[randomInt].position;
        cur_fb.transform.rotation = muzzles[randomInt].rotation;
        float randemForce = Random.Range(-10, 10) * 0.01f + force;
        cur_fb.GetComponent<Rigidbody>().AddForce(randemForce * muzzles[randomInt].forward, ForceMode.Impulse);
        BallRecordBehavior.Build(SubjectBehavior.Build(cur_fb, RecorderForAniAndVR.instance.recorder, "FootBall"));
        //GameManager.instance.CurrentBall = fb;
        AddCount();


    }
    void SpawnBall()
    {
        if (isSingleBall && cur_fb != null) return;
        audioSource.PlayOneShot(fireClip);
        cur_fb = Instantiate(football);
        cur_fb.transform.position = muzzle.position;
        cur_fb.transform.rotation = muzzle.rotation;
        cur_fb.GetComponent<Rigidbody>().AddForce(force * muzzle.forward, ForceMode.Impulse);
        BallRecordBehavior.Build(SubjectBehavior.Build(cur_fb, RecorderForAniAndVR.instance.recorder, "FootBall"));
        //GameManager.instance.CurrentBall = fb;
        AddCount();
        

    }

    void FirstSpawnBall()
    {
        
        audioSource.PlayOneShot(fireClip);
        cur_fb = Instantiate(football);
        cur_fb.transform.position = muzzle.position;
        cur_fb.transform.rotation = muzzle.rotation;
        cur_fb.GetComponent<Rigidbody>().AddForce(force * muzzle.forward, ForceMode.Impulse);
        BallRecordBehavior.Build(SubjectBehavior.Build(cur_fb, RecorderForAniAndVR.instance.recorder, "FootBall"));
        //GameManager.instance.CurrentBall = fb;
        AddCount();
    }
}
