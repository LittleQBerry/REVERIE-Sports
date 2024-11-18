using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeManDect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("Ball"))
        {
            GameManager.instance.MinScore(1);
            GameManager.instance.AddFakeMan();
        }
    }
}
