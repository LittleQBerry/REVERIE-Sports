using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootAttr : MonoBehaviour
{
    public PartOfFoot currentPartFoot;
    // Start is called before the first frame update
    public List<FootPointPartDetect> detetctFootPoints;

    
    public PartOfFoot GetPartOfFoot(Vector3 collisonPos)
    {
        float mindistance = 5f;
        foreach(var p in detetctFootPoints)
        {
 
                float dis = Vector3.Distance(collisonPos,p.transform.position);
                if(dis < mindistance)
                {
                    mindistance = dis;
                    currentPartFoot = p.GetFootOfPart();
                    Debug.Log(currentPartFoot.ToString());
                }
                
            
        }
       
        return currentPartFoot;
    }
}
