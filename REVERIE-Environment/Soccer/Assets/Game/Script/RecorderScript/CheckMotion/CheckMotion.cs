using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckMotion : MonoBehaviour
{
    public Transform leftFoot;
    public Transform rightFoot;

    public Transform manModel;

    public Transform table;

    public Transform m_point1;
    public Transform m_point2;

    public Transform m_target1;
    public Transform m_target2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(leftFoot.position,rightFoot.position));
        if (Input.GetKeyDown(KeyCode.K))
        {
            CheckObjIsForwardToOther(obj, other, .5f);
            //GetUpAngle(m_target1,m_target2);
            //CreatCube(m_point1, m_point2);
            //Debug.Log(leftFoot.position);
            //CheckFootForward(leftFoot, rightFoot, manModel);
            //if (!CheckFootDistance(leftFoot.position, rightFoot.position, out string message))
            //{
            //    Debug.Log(message);
            //}
        }
    }

    public Transform obj;
    public Transform other;
    public void CheckObjIsForwardToOther(Transform obj,Transform other,float width)
    {
        Vector3 localPos = other.transform.InverseTransformPoint(obj.transform.position);
        Vector3 forward = other.transform.forward;
        float dot = Vector3.Dot(localPos.normalized, forward);
        if (dot > 0 && Mathf.Abs(localPos.x) <= width)
        {
            Debug.Log("��ǰ����" );
        }
    }

    public float offset = 0.1f;

    public void CreatCube(Transform point1, Transform point2)
    {
        Vector3 center, size;
        center = (point1.position + point2.position) / 2;
        center = new Vector3(center.x, center.y / 2, center.z);
        size = new Vector3(.1f,
                           Mathf.Abs(point1.position.y - Vector3.zero.y),
                           1.5f);

        // �����������λ��ȷ��Cube�ķ���
        Vector3 direction = point2.position - point1.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        // ����Cube��������λ�á���ת������
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = center;
        cube.transform.rotation = rotation;
        cube.transform.localScale = size;

    }
    public void GetUpAngle(Transform target1, Transform target2)
    {
        Vector3 direction1 = target1.up;
        Vector3 direction2 = target2.up;
        float angle = Mathf.Acos(Vector3.Dot(direction1, direction2)) * Mathf.Rad2Deg;
        Debug.Log("Angle between targets: " + angle);
    }

    public bool CheckFootForward(Transform lFoot, Transform rFoot,Transform man)
    {
        Vector3 rightFootPos = lFoot.position;
        Vector3 leftFootPos = rFoot.position;

        // �����ҽ�λ����ǰ����10���׺������
        Vector3 forwardVector = rightFootPos + rightFoot.forward ;
        float distanceToLine = Vector3.Distance(leftFootPos, forwardVector);
        Debug.Log(distanceToLine);
        return false;
        // �ж�����Ƿ����ҽ�ǰ��
        //float distanceToLine = Vector3.Distance(leftFootPos, forwardVector);
        //// ��ȡ���ﳯ����
        //Vector3 forwardDirection = man.forward;

        //// ��ȡ���ҽ�λ��
        //Vector3 leftFootPosition = lFoot.position;
        //Vector3 rightFootPosition = rFoot.position;

        //// �����ҽ�λ������ͶӰ�������ﳯ������䷨���������ɵ�ƽ����
        //Vector3 leftFootForward = Vector3.ProjectOnPlane(leftFootPosition - man.position, man.up);
        //Vector3 rightFootForward = Vector3.ProjectOnPlane(rightFootPosition - man.position, man.up);
        //Vector3 leftFootOnPlane = Vector3.ProjectOnPlane(leftFootForward, forwardDirection);
        //Vector3 rightFootOnPlane = Vector3.ProjectOnPlane(rightFootForward, forwardDirection);

        //// �������ҽ�λ�������ĵ�����ж�ǰ���ϵ
        //float dotProduct = Vector3.Dot(leftFootOnPlane, rightFootOnPlane);

        //if (dotProduct > offset)
        //{
        //    Debug.Log("������ҽ�ǰ��:"+ dotProduct);

        //}
        //else if (dotProduct < -offset)
        //{
        //    Debug.Log("�ҽ������ǰ��"+ dotProduct);
        //}
        //else
        //{
        //    Debug.Log("���ҽ�λ����ͬ");
        //}
        //return false;
    }

    public bool CheckFootDistance(Vector3 lFoot, Vector3 rFoot, out string checkMessage)
    {
        var dis = Vector3.Distance(lFoot, rFoot);
        Debug.Log(dis);
        if (dis < RuleConfig.min_distance_of_feet)
        {
            checkMessage = CheckMessage.P_R_01;
            return false;
        }
        if (dis > RuleConfig.max_distance_of_feet)
        {
            checkMessage = CheckMessage.P_R_02;
            return false;
        }
        checkMessage = "";
        return true;
    }
}
