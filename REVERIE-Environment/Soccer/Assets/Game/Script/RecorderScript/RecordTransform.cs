using UnityEngine;

public class RecordTransform : MonoBehaviour
{
    private const int bufferSize = 300; //缓冲区大小
    private int currentIndex = 0;
    private float[] timeBuffer = new float[bufferSize];
    private Vector3[] positionBuffer = new Vector3[bufferSize];
    private Quaternion[] rotationBuffer = new Quaternion[bufferSize];

    private Vector3 lastPosition; // 上一个记录的位置
    private float lastRecordTime; // 上一个记录的时间

    public float m_velocity;
    public Vector3 m_dir;
    void Update()
    {
        Record(Time.time, transform.position, transform.rotation);
        CalculateVelocity();
    }

    private void Record(float time, Vector3 position, Quaternion rotation)
    {
        //将位置和旋转信息存储在缓冲区中
        timeBuffer[currentIndex] = time;
        positionBuffer[currentIndex] = position;
        rotationBuffer[currentIndex] = rotation;

        // 增加当前索引，如果索引到达缓冲区的末尾，重新从0开始
        currentIndex = (currentIndex + 1) % bufferSize;
    }

    // 获取过去n秒内的位置和旋转信息
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

        // 如果j小于缓冲区大小，将剩余的元素设置为默认值
        for (; j < bufferSize; j++)
        {
            positions[j] = Vector3.zero;
            rotations[j] = Quaternion.identity;
        }
    }

    // 计算物体的速度和移动方向
    private void CalculateVelocity()
    {
        float deltaTime = Time.time - lastRecordTime;
        if (deltaTime > 0)
        {
            Vector3 currentPosition = transform.position;
            Vector3 displacement = currentPosition - lastPosition;
            Vector3 velocity = displacement / deltaTime;

            // velocity.magnitude可以获取速度大小，velocity.normalized可以获取移动方向
            //Debug.Log("Velocity: " + velocity.magnitude + " Direction: " + velocity.normalized);
            m_velocity = velocity.magnitude;
            m_dir = velocity.normalized;
            lastPosition = currentPosition;
            lastRecordTime = Time.time;
        }
    }
}
