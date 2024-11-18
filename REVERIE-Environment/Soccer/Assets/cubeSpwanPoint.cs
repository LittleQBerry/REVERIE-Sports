using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeSpwanPoint : MonoBehaviour
{

    float cdTime = 3f;
    bool StartCD = false;
    float timer = 0f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Leg")&& !StartCD)
        {
            CrossSceneManager.instance.SpwanBall();
            StartCD = true;
        }
    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StartCD)
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                StartCD = false;
                timer = 0;
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CrossSceneManager.instance.SpwanBall();
            StartCD = true;
        }
    }
}
