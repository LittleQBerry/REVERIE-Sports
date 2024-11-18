using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootContr : MonoBehaviour
{
    Rigidbody rigidbody;
    public Vector3 relativeVallocity = Vector3.zero;
    public Vector3 relativeAnglarVallocity = Vector3.zero;
    public float speed;
    Vector3 relativePoint = Vector3.zero;
    public Transform foot;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        //if(relativeVallocity!= rigidbody.velocity)
        //{
        //    relativeVallocity = rigidbody.velocity;
        //}
        //if(relativeAnglarVallocity != rigidbody.angularVelocity)
        //{
        //    relativeAnglarVallocity = rigidbody.angularVelocity;
        //}
        if(relativePoint!= foot.transform.position)
        {
            var dis = Vector3.Distance(relativePoint, foot.transform.position);
            speed = dis / Time.fixedDeltaTime;
            relativePoint = foot.transform.position;
        }
        
    }

    
    
}
