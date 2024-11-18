using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegSetting : MonoBehaviour
{
    public PhysicMaterial footPM;
    public Rigidbody rg;
    public Transform otherFoot;

    public LayerMask layerMask;
    //Collider collider;
    public Transform bottomPoint;
    public float maxHight;
    public float maxDegree;
    public Transform foot;

    bool isVerticalOtherFoot;
    public bool isHighWayMode;

    public Transform HightFootCube;


    public float angle;
    public Vector3 GetHighDir()
    {
        return -HightFootCube.forward;
    }
    private void FixedUpdate()
    {
        if(MainSceneManager.instance.settingOffset.footBounceseMode == FootBounceseMode.Threshold)
        {
            if (rg.velocity.magnitude >= MainSceneManager.instance.settingOffset.footBounceChangeThreshold)
            {
                footPM.bounciness = 0.8f;
            }
            else
            {
                footPM.bounciness = 0.02f;
            }
        }
        else
        {
            footPM.bounciness = Mathf.Clamp(rg.velocity.magnitude * 0.1f,0,1f);
        }
        
        if(GameManager.instance!=null&&GameManager.instance.gameType == GameType.ÉäÃÅ)
        {
            angle = Vector3.Angle(otherFoot.forward, transform.forward);
            if (angle >= 70 && angle <= 120)
            {
                isVerticalOtherFoot = true;
                //HightFootCube.gameObject.SetActive(false);
            }
            else
            {
                isVerticalOtherFoot = false;
                if (angle < MainSceneManager.instance.settingOffset.angleA_max && angle > MainSceneManager.instance.settingOffset.angleA_min)
                {
                    isHighWayMode = true;
                    //HightFootCube.gameObject.SetActive(true);

                }
                else
                {
                    isHighWayMode = false;
                    //HightFootCube.gameObject.SetActive(false);
                }
            }
            if (isVerticalOtherFoot)
            {
                foot.localEulerAngles = Vector3.Lerp(foot.localEulerAngles, Vector3.zero, Time.deltaTime * 10);
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(bottomPoint.position, -bottomPoint.up, out hit, Mathf.Infinity, layerMask))
                {
                    var distance = hit.distance;
                    distance = Mathf.Clamp(distance, 0, maxHight);
                    foot.localEulerAngles = new Vector3(0f, 0f, distance * maxDegree / maxHight);
                }
            }
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
