using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameriaTrack : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 3, 3);//����������ҵ�λ��
    public Transform target;
    private Vector3 pos;
    public float speed = 2;

    void FixedUpdate()
    {
        pos = target.position + offset;
        this.transform.position = Vector3.Lerp(this.transform.position, pos, speed * Time.deltaTime);//������������֮��ľ���
        Quaternion angel = Quaternion.LookRotation(target.position - this.transform.position);//��ȡ��ת�Ƕ�
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, angel, speed * Time.deltaTime);

    }
    //private void Update()
    //{
    //    transform.LookAt(target);
    //}
}

