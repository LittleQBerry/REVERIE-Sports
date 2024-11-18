using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPose : MonoBehaviour
{
    public float offset;
    public Transform handTransform;
    private float hight;
    public Transform LegModel;
    float timer = 0;
    float takeTime = 3;
    bool isSettingPos = false;
    public Transform foot;
    // Start is called before the first frame update
    void Start()
    {
        
        ////hight = handTransform.position.y - offset;
        //hight = handTransform.position.y - offset;
        //grabPoint.localPosition = new Vector3(grabPoint.localPosition.x, hight, grabPoint.localPosition.z);
    }
    void SettingPos()
    {
        RaycastHit hit;
        Vector3 point_dir = transform.TransformDirection(-Vector3.up);
        if (Physics.Raycast(transform.position+new Vector3(0,0,0.5f), point_dir, out hit, 1000f, LayerMask.GetMask("Ground")))
        {
            Quaternion nextRot = Quaternion.LookRotation(Vector3.Cross(hit.normal, Vector3.Cross(transform.forward, hit.normal)), hit.normal);
            LegModel.rotation = Quaternion.Lerp(LegModel.rotation, nextRot, 0.2f);
            LegModel.position = new Vector3(LegModel.position.x, hit.distance - (hit.distance - offset), LegModel.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().centerOfMass = foot.localPosition;
        Vector3 point_dir = transform.TransformDirection(-Vector3.up);
        //Debug.DrawRay(transform.position+new Vector3(0, 0, 0.5f), point_dir*1000,Color.red);
        //if (InputBridge.Instance.BButtonUp) {
        ////if (Input.GetKeyDown(KeyCode.Space)) { 
        //    if(!isSettingPos)
        //        isSettingPos = true;
        //}
        if (isSettingPos)
        {
            timer += Time.deltaTime;
            SettingPos();
            if (timer> takeTime)
            {
                isSettingPos = false;
                timer = 0;
            }
        }
    }
}
