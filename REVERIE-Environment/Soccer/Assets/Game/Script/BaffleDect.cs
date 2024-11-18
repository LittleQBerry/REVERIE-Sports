using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaffleDect : MonoBehaviour
{
    bool OnState = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetState()
    {
        OnState = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        OnState = false;
        GameManager.instance.AddScore(3);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
