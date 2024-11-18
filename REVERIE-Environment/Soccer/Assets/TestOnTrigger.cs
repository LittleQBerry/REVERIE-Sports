using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOnTrigger : MonoBehaviour
{
    
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Cube")
        {
            Debug.Log("OnTriggerEnter");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Cube")
        {
            Debug.Log("OnCollisionEnter");
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
