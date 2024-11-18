using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSetUp : MonoBehaviour
{

    public Transform XRMan;
    public Transform VRController;
    public Transform targetTransform;
    Quaternion targetPoint;



    // Start is called before the first frame update
    void Start()
    {
        targetPoint = Quaternion.Euler(0, 180, 0);
        XRMan.position = transform.position;
        TurnTarget();
    }
    void TurnTarget()
    {
        //Vector3 v = Vector3.zero - targer.position;                               //首先获得目标方向
        //v.z = 0;                                                                                            //这里一定要将z设置为0
        //float angle = Vector3.SignedAngle(Vector3.up, v, Vector3.forward);//得到围绕z轴旋转的角度
        //Quaternion rotation = Quaternion.Euler(0, 0, angle);                     //利用角度得到rotation
        //XRMan.rotation = rotation;
        targetTransform.rotation = Quaternion.Slerp(VRController.rotation, targetTransform.rotation, 2);
        //平滑移动   targetTransform.position  目标位置
        //VRController.position = Vector3.MoveTowards(transform.position, targetTransform.position, 2 * Time.deltaTime);

        //以该法向量为轴进行旋转
        //transform.Rotate(normalize, Time.deltaTime * rotateSpeed, Space.Self);
        //Debug.Log(angles);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
