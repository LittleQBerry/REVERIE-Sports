using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadContr : MonoBehaviour
{
    public float speed;
    public Transform headCol;
    Vector3 relativePoint = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (relativePoint != headCol.position)
        {
            var dis = Vector3.Distance(relativePoint, headCol.position);
            speed = dis / Time.fixedDeltaTime;
            relativePoint = headCol.position;
        }
    }
}
