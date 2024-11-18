using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highdetct : MonoBehaviour
{
    public LayerMask layerMask;
    //Collider collider;
    public Transform bottomPoint;
    public float maxHight;
    public float maxDegree;
    //Vector3 minSize;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(bottomPoint.position, -bottomPoint.up, out hit, Mathf.Infinity, layerMask))
        {
            var distance = hit.distance;
            //Debug.Log(distance);
            distance = Mathf.Clamp(distance, 0, maxHight);
            transform.localEulerAngles = new Vector3(distance * maxDegree / maxHight, 0f, 0f);
            //if (distance > 0.1f && distance < 0.4f)
            //{


            //}
            //else
            //{
            //    transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            //}
        }
    }
}
