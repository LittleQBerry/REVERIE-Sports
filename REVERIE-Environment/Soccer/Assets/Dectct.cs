using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dectct : MonoBehaviour
{
    public Vector3 point;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Desk"))
        {
            point = collision.contacts[0].point;
            Debug.Log("point:"+ point);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
