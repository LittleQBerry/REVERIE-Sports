using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapFoot : MonoBehaviour
{
    public Transform footPoint;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (footPoint != null)
        {
            rigidbody.MovePosition(footPoint.position);
            rigidbody.MoveRotation(footPoint.rotation);
        }
    }
}
