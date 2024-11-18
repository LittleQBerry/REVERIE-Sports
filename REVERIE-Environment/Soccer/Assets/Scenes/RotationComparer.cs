using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotationComparer : MonoBehaviour
{
    public Transform object1; // 第一个物体
    public Transform object2; // 第二个物体

    private Quaternion initRotation1; // 第一个物体的初始旋转角度
    private Quaternion initRotation2; // 第二个物体的初始旋转角度
    private float timer = 0f;
    private const float interval = 1f;

    void Start()
    {
        initRotation1 = object1.localRotation;
        initRotation2 = object2.localRotation;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer -= interval;

            // 计算旋转方向相似度百分比
            float directionSimilarity = CalculateDirectionSimilarity();
            Debug.Log("旋转方向相似度: " + directionSimilarity.ToString("P2"));

        }
    }

    float CalculateDirectionSimilarity()
    {
        Quaternion rotation1 = object1.localRotation;
        Quaternion rotation2 = object2.localRotation;

        // 计算每一帧的旋转角度变化量
        Quaternion deltaRotation1 = rotation1 * Quaternion.Inverse(initRotation1);
        Quaternion deltaRotation2 = rotation2 * Quaternion.Inverse(initRotation2);

        // 计算旋转方向的相似度百分比
        float error = Quaternion.Angle(deltaRotation1, deltaRotation2) / 180.0f;
        float similarity = 1.0f - error;

        return similarity;
    }

}