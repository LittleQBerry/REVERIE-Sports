using NPOI.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    public static Player instance;
    public GameObject head;

    public GameObject playerPos;
    public Transform rightFoot;
    public Transform leftFoot;
    public bool showDebugPlayerPos;

    private Vector3[] positions;
    private int currentIndex = 0;

    public float sampleRate = 0.02f; // 采样频率，每0.02秒采样一次
    public float duration = 0.2f; // 总计算时间为0.2秒
    public int sampleCount; // 采样次数
    Vector3 averageDirection;
    private void Awake()
    {
        instance = this;
        showDebugPlayerPos = false;
        playerPos = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        if (showDebugPlayerPos)
        {
            playerPos.GetComponent<Collider>().enabled = true;
            playerPos.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            playerPos.GetComponent<Collider>().enabled = false;
            playerPos.GetComponent<MeshRenderer>().enabled = false;
        }
        sampleCount = Mathf.CeilToInt(duration / sampleRate);
        positions = new Vector3[sampleCount];

    }
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        CalculatePlayerPos();
        float angle = Vector3.Angle(RecorderForAniAndVR.instance.bpc.spine.transform.up, Vector3.up);
        //TipUI.instance.ShowDebugText(angle.ToString());
    }
    void CalculatePlayerPos()
    {
        if (rightFoot != null && leftFoot != null)
        {
            Vector3 leftFootPos = leftFoot.position;
            Vector3 rightFootPos = rightFoot.position;
            Vector3 characterPos = (GetPosOnGround(leftFootPos) + GetPosOnGround(rightFootPos)) / 2f;
            Vector3 leftFootForward = leftFoot.forward;
            Vector3 rightFootForward = rightFoot.forward;
            Vector3 characterForward = (leftFootForward + rightFootForward) / 2f;

            // 设置人物的位置和朝向
            playerPos.transform.position = characterPos;
            playerPos.transform.rotation = Quaternion.LookRotation(characterForward);
        }
        // 采样当前帧的位置
        positions[currentIndex] = playerPos.transform.position;

        // 更新当前索引
        currentIndex = (currentIndex + 1) % sampleCount;

        // 计算平均方向
        averageDirection = CalculateAverageDirection();

    }
    public Vector3 GetHeadDir()
    {
        return GetPosOnGround(head.transform.forward);
    }
    public Vector3 GetPlayerMoveDir()
    {
        return averageDirection;
    }
    private Vector3 CalculateAverageDirection()
    {
        Vector3 sum = Vector3.zero;

        for (int i = 0; i < sampleCount; i++)
        {
            sum += positions[i];
        }

        return (sum / sampleCount - transform.position).normalized;
    }
    Vector3 GetPosOnGround(Vector3 pos)
    {
        return new Vector3(pos.x,0,pos.z);
    }
}
