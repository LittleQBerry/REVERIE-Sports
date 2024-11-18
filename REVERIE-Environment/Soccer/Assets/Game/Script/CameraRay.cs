using System.Collections;
using UnityEngine;


public class CameraRay : MonoBehaviour
{
    bool state = false;
    public LayerMask layer;
    // Use this for initialization
    void Start()
    {

    }
    public void Oninit()
    {
        state = true;
    }
    public void OnExit()
    {
        state = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (state)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position,transform.forward,out hit,Mathf.Infinity, layer))
            {
                hit.transform.GetComponent<FakeTarget>().OnRayCast();
            }
        }
    }
}
