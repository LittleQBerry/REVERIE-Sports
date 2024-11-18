using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;

public class dirLean : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var dir = target.position - transform.position;
        Debug.DrawRay(transform.position, dir,Color.red);
    }
}
