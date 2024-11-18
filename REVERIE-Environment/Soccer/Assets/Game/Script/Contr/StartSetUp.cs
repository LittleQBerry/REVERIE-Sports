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
        //Vector3 v = Vector3.zero - targer.position;                               //���Ȼ��Ŀ�귽��
        //v.z = 0;                                                                                            //����һ��Ҫ��z����Ϊ0
        //float angle = Vector3.SignedAngle(Vector3.up, v, Vector3.forward);//�õ�Χ��z����ת�ĽǶ�
        //Quaternion rotation = Quaternion.Euler(0, 0, angle);                     //���ýǶȵõ�rotation
        //XRMan.rotation = rotation;
        targetTransform.rotation = Quaternion.Slerp(VRController.rotation, targetTransform.rotation, 2);
        //ƽ���ƶ�   targetTransform.position  Ŀ��λ��
        //VRController.position = Vector3.MoveTowards(transform.position, targetTransform.position, 2 * Time.deltaTime);

        //�Ը÷�����Ϊ�������ת
        //transform.Rotate(normalize, Time.deltaTime * rotateSpeed, Space.Self);
        //Debug.Log(angles);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
