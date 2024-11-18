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
        // 获取A、B、C的位置
        // 计算AB向量和AB的旋转信息
        Vector3 AB = objectB.transform.position - objectA.transform.position;
        Quaternion ABRotation = Quaternion.FromToRotation(Vector3.right, AB.normalized);

        // 将AB的旋转信息应用到C的旋转信息中，得到AC的旋转信息
        Quaternion ACRotation = ABRotation * objectC.transform.rotation;

        // 计算AD向量和AD的旋转信息
        Vector3 AD = AB + objectC.transform.position - objectA.transform.position;
        Quaternion ADRotation = ACRotation;

        // 将AD的旋转信息应用到D的旋转信息中
        Quaternion DRotation = ADRotation * Quaternion.Inverse(ABRotation);

        // 设置D的位置和旋转信息
        objectD.transform.position = objectA.transform.position + AD;
        objectD.transform.rotation = DRotation;
    }
}
