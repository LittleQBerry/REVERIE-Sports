using UnityEngine;

public class RecordTransform : MonoBehaviour
{
    private const int bufferSize = 300; //��������С
    private int currentIndex = 0;
    private float[] timeBuffer = new float[bufferSize];
    private Vector3[] positionBuffer = new Vector3[bufferSize];
    private Quaternion[] rotationBuffer = new Quaternion[bufferSize];

    private Vector3 lastPosition; // ��һ����¼��λ��
    private float lastRecordTime; // ��һ����¼��ʱ��

    public float m_velocity;
    public Vector3 m_dir;
    void Update()
    {
        Record(Time.time, transform.position, transform.rotation);
        CalculateVelocity();
    }

    private void Record(float time, Vector3 position, Quaternion rotation)
    {
        //��λ�ú���ת��Ϣ�洢�ڻ�������
        timeBuffer[currentIndex] = time;
        positionBuffer[currentIndex] = position;
        rotationBuffer[currentIndex] = rotation;

        // ���ӵ�ǰ����������������ﻺ������ĩβ�����´�0��ʼ
        currentIndex = (currentIndex + 1) % bufferSize;
    }

    // ��ȡ��ȥn���ڵ�λ�ú���ת��Ϣ
    public void GetHistory(float duration, out Vector3[] positions, out Quaternion[] rotations)
    {
        positions = new Vector3[bufferSize];
        rotations = new Quaternion[bufferSize];

        int j = 0;
        for (int i = currentIndex - 1; i >= 0 && j < bufferSize; i--, j++)
        {
            float deltaTime = Time.time - timeBuffer[i];
            if (deltaTime <= duration)
            {
                positions[j] = positionBuffer[i];
                rotations[j] = rotationBuffer[i];
            }
            else
            {
                break;
            }
        }

        for (int i = bufferSize - 1; i >= currentIndex && j < bufferSize; i--, j++)
        {
            float deltaTime = Time.time - timeBuffer[i];
            if (deltaTime <= duration)
            {
                positions[j] = positionBuffer[i];
                rotations[j] = rotationBuffer[i];
            }
            else
            {
                break;
            }
        }

        // ���jС�ڻ�������С����ʣ���Ԫ������ΪĬ��ֵ
        for (; j < bufferSize; j++)
        {
            positions[j] = Vector3.zero;
            rotations[j] = Quaternion.identity;
        }
    }

    // ����������ٶȺ��ƶ�����
    private void CalculateVelocity()
    {
        float deltaTime = Time.time - lastRecordTime;
        if (deltaTime > 0)
        {
            Vector3 currentPosition = transform.position;
            Vector3 displacement = currentPosition - lastPosition;
            Vector3 velocity = displacement / deltaTime;

            // velocity.magnitude���Ի�ȡ�ٶȴ�С��velocity.normalized���Ի�ȡ�ƶ�����
            //Debug.Log("Velocity: " + velocity.magnitude + " Direction: " + velocity.normalized);
            m_velocity = velocity.magnitude;
            m_dir = velocity.normalized;
            lastPosition = currentPosition;
            lastRecordTime = Time.time;
        }
    }
}
