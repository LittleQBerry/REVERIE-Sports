using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class draw_orbit : MonoBehaviour
{

    Rigidbody rg;
    float velocity = 0;
    float timer = 0f;
    float offset = .1f;
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(rg.velocity.magnitude!= velocity)
        {
            velocity = rg.velocity.magnitude;
            timer += Time.fixedDeltaTime;
            if (timer > 0.1f)
            {
                timer = 0;
                //GameObject mark = PoolManager.instance.GetFreeMark();
                //mark.transform.position = transform.position;
                //mark.transform.localScale = new Vector3(rg.velocity.magnitude * offset, rg.velocity.magnitude * offset, rg.velocity.magnitude * offset) ;
            }
        }
    }

    void Update()
    {


    }
}

