using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkObject : MonoBehaviour
{

    float liftTime = 3f;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }
    public void OnEnable()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer> liftTime)
        {
            this.gameObject.SetActive(false);
        }
    }
}
