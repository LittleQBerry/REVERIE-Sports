using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCube : MonoBehaviour
{
    public float time;
    public Vector3 startPos;
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("projectile"))
        {
            startPos = other.gameObject.transform.position;
            time = Time.time;
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
