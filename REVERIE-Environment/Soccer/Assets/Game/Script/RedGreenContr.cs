using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGreenContr : MonoBehaviour
{
    FlagDetect flagDetect;
    public WallType wallType;
  
    // Start is called before the first frame update
    void Start()
    {
       
        flagDetect = GetComponentInParent<FlagDetect>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball")) 
        {
            if(wallType == WallType.green)
            {
                flagDetect.OnRight();
            }
            else
            {
                flagDetect.OnWrong();
            }

        }  
    }


    
}
public enum WallType
{
    red,
    green
}
