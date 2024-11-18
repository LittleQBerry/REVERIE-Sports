using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Vector3 startPoint;
    public float moveDistance = 0.5f;
    Vector3 targetPoint;
    int randomInt = 0;
    public float speed = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        randomInt = Random.Range(0, 10);
        randomInt = randomInt % 2;
        ChangeDir();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, targetPoint)<0.1f)
        {
            ChangeDir();
        }
        else
        {
            transform.Translate((targetPoint-transform.position).normalized * Time.deltaTime* speed);
        }
    }
    void ChangeDir()
    { 
        if(randomInt == 1)
        {
            targetPoint = (startPoint - transform.right* moveDistance);
            randomInt = 0;
            return;
        }
        if(randomInt == 0)
        {
            targetPoint = (startPoint + transform.right * moveDistance);
            randomInt = 1;
            return;
        }
    }
}
