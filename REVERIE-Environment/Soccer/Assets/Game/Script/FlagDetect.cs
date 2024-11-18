using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagDetect : MonoBehaviour
{
    public GameObject redDetect;
    public GameObject greenDect;
    float countTime;
    Vector3 dir;
    Vector3 startPos;
    Quaternion startRotation ;
    // Start is called before the first frame update
    private void Awake()
    {
        DisAbleDetect();
    }
    void Start()
    {
        startPos = transform.position;
        startRotation = transform.rotation;
        dir = transform.forward;
        //Reset();
    }
    void ResetPose()
    {
        transform.position = startPos;
        transform.rotation = startRotation;
    }
    public void Reset()
    {
        redDetect.SetActive(true);
        greenDect.SetActive(true);
    }
    public void DisAbleDetect()
    {
        redDetect.SetActive(false);
        greenDect.SetActive(false);
    }
    public void OnRight()
    {
        DisAbleDetect();
        CrossSceneManager.instance.Next();
        CrossSceneManager.instance.AddScore(1);
        GameManager.instance.AddScore(2);
    }
    public void OnWrong()
    {
        DisAbleDetect();
        CrossSceneManager.instance.Next();
        CrossSceneManager.instance.AddScore(-1);
        GameManager.instance.MinScore(1);
    }
    // Update is called once per frame
    void Update()
    {
        if(dir!= transform.forward)
        {
            countTime += Time.deltaTime;
            if (countTime > 5f)
            {
                ResetPose();
                countTime = 0;
            }
        }
    }
}
