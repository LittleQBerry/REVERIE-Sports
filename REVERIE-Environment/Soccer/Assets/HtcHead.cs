using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HtcHead : MonoBehaviour
{
    public Transform m_camera;
    public Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.MovePosition(m_camera.position);
        rigidbody.MoveRotation(m_camera.rotation);
    }
}
