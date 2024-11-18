using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountSpeed : MonoBehaviour
{
    public List<string> speedsList;
    public StartCube startCube;
    public Text text;
    public string conten;
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("projectile"))
        {
            //float distance = Vector3.Distance(other.transform.position, startCube.startPos);
            //float time = Time.time - startCube.time;
            //string speed = distance / time + "";
            //speedsList.Add(speed);
            conten = conten+"++"+other.attachedRigidbody.velocity.magnitude;
            text.text = conten;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        speedsList = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
