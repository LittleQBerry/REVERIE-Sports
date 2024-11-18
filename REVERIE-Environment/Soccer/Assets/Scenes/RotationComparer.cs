using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotationComparer : MonoBehaviour
{
    public Transform object1; // ��һ������
    public Transform object2; // �ڶ�������

    private Quaternion initRotation1; // ��һ������ĳ�ʼ��ת�Ƕ�
    private Quaternion initRotation2; // �ڶ�������ĳ�ʼ��ת�Ƕ�
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

            // ������ת�������ƶȰٷֱ�
            float directionSimilarity = CalculateDirectionSimilarity();
            Debug.Log("��ת�������ƶ�: " + directionSimilarity.ToString("P2"));

        }
    }

    float CalculateDirectionSimilarity()
    {
        Quaternion rotation1 = object1.localRotation;
        Quaternion rotation2 = object2.localRotation;

        // ����ÿһ֡����ת�Ƕȱ仯��
        Quaternion deltaRotation1 = rotation1 * Quaternion.Inverse(initRotation1);
        Quaternion deltaRotation2 = rotation2 * Quaternion.Inverse(initRotation2);

        // ������ת��������ƶȰٷֱ�
        float error = Quaternion.Angle(deltaRotation1, deltaRotation2) / 180.0f;
        float similarity = 1.0f - error;

        return similarity;
    }

}