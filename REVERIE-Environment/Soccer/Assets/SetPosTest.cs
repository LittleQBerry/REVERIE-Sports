using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosTest : MonoBehaviour
{
    public Transform objectA;
    public Transform objectB;
    public Transform objectC;
    public Transform objectD;

    void Start()
    {
        CalculateD();
    }

    void CalculateD()
    {
        // ��ȡA��B��C��λ��
        // ����AB������AB����ת��Ϣ
        Vector3 AB = objectB.transform.position - objectA.transform.position;
        Quaternion ABRotation = Quaternion.FromToRotation(Vector3.right, AB.normalized);

        // ��AB����ת��ϢӦ�õ�C����ת��Ϣ�У��õ�AC����ת��Ϣ
        Quaternion ACRotation = ABRotation * objectC.transform.rotation;

        // ����AD������AD����ת��Ϣ
        Vector3 AD = AB + objectC.transform.position - objectA.transform.position;
        Quaternion ADRotation = ACRotation;

        // ��AD����ת��ϢӦ�õ�D����ת��Ϣ��
        Quaternion DRotation = ADRotation * Quaternion.Inverse(ABRotation);

        // ����D��λ�ú���ת��Ϣ
        objectD.transform.position = objectA.transform.position + AD;
        objectD.transform.rotation = DRotation;
    }
}
