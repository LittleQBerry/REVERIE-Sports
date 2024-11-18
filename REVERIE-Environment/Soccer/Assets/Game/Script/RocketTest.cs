using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallState
{
    BS_Ready,
    BS_Start,
    GS_Finish
}

public class RocketTest : MonoBehaviour
{
    public BallState m_curBallState = BallState.GS_Finish;
    public float rocketFrictionCoefficient = 0.1f;//拍摩擦係數
    public float rocketLossParameter = 0.9f; //拍能量恢復係數
    public float tableFrictionCoefficient = 0.1f;//桌子摩擦係數
    public float tableLossParameter = 0.9f; //桌子拍能量恢復係數
    private float m_curFrictionCoefficient = 0.1f; //當前摩擦係數
    private float m_curLossParameter = 0.9f;//當前能量恢復係數
    SphereCollider SphereCollider;
    Rigidbody ballRigidbody;
    Vector3 m_curVelocity;
    Vector3 m_curAngleVelocity;
    //public BallAudio ballAudio;
    public GameObject lastHitObj;
    public TableHitCollider lastHitTableCollider;
    public RocketNew lastHitRocketCollider;
    public float addUpForceValue = 0;
    public bool addTrainingScore = false;

    public bool startLifeTime = false;
    public float lifeTime = 3;
    float m_curLifeTime = 0;
    public bool canDestory = false;
    bool severBall = true;
    public Vector3 releasePos;
    bool isThrowUp = false;

    // Start is called before the first frame update
    void Start()
    {
        //ballAudio = GetComponent<BallAudio>();
        SphereCollider = GetComponent<SphereCollider>();
        ballRigidbody = GetComponent<Rigidbody>();
        
    }

    private void OnEnable()
    {
        //GameManager.Singleton.m_curBall = this;
        m_curBallState = BallState.BS_Ready;
    }

    void Update()
    {
        //Vector3 airResistance = -0.5f * 1.25f * 0.5f * Mathf.Pow(SphereCollider.radius,2) * ballRigidbody.velocity.magnitude * ballRigidbody.velocity;  //空氣阻力
        //Vector3 MagnusEffect = 4 * 0.33f * 3.14f * Mathf.Pow(SphereCollider.radius, 3) * 4 * 3.14f * 1.25f * Vector3.Cross(ballRigidbody.angularVelocity, ballRigidbody.velocity); //馬格努斯效應
        //GetComponent<Rigidbody>().AddForce(airResistance + MagnusEffect, ForceMode.Force);
        //m_curVelocity = GetComponent<Rigidbody>().velocity;
        //m_curAngleVelocity = GetComponent<Rigidbody>().angularVelocity;

        if (startLifeTime)
        {
            if (m_curLifeTime <= lifeTime)
            {
                m_curLifeTime += Time.deltaTime;
                if (m_curLifeTime > lifeTime)
                {
                    if (canDestory)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        gameObject.SetActive(false);
                        startLifeTime = false;
                    }
                }
            }
        }

        if(severBall && !isThrowUp)
        {
            if(transform.position.y > releasePos.y + 0.1f)
            {
                isThrowUp = true;
            }
        }
    }

    
    public float testValue = 2;
    private void FixedUpdate()
    {
        m_curVelocity = GetComponent<Rigidbody>().velocity;
        m_curAngleVelocity = GetComponent<Rigidbody>().angularVelocity;
        Vector3 airResistance = 500  * - 0.5f * 1.25f * 0.5f * Mathf.Pow(SphereCollider.radius, 2) * ballRigidbody.velocity.magnitude * ballRigidbody.velocity;  //空氣阻力
        Vector3 MagnusEffect = testValue * 4 * 0.33f * 3.14f * Mathf.Pow(SphereCollider.radius, 3) * 4 * 3.14f * 1.25f * Vector3.Cross(ballRigidbody.angularVelocity, ballRigidbody.velocity); //馬格努斯效應
        GetComponent<Rigidbody>().AddForce(airResistance+ MagnusEffect, ForceMode.Acceleration);
        //Debug.Log("!!!!!!!!!!!! airResistance "+ airResistance);
        //Debug.Log("!!!!!!!!!!!! MagnusEffect " + MagnusEffect);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Net")
        {
            lastHitObj = collision.gameObject;
            return;//hit net.do not run below logic
        }
        else
        {
            if (GetComponent<Rigidbody>() == null || collision.gameObject.GetComponent<Rigidbody>() == null)
            {
                bool needReSever = false;
                if (severBall && !isThrowUp)
                    needReSever = true;
                if(m_curBallState == BallState.BS_Start)
                   // GameManager.Singleton.OnBallStateFinish(this, needReSever);//hit either table or rocket.then finish
                return;
            }
        }

        bool doBallHitRocketLogic = false;
        bool doBallHitTableLogic =false;
        //if(GameManager.m_singleton.m_curGameType != GameManager.GameType.GT_Match)
        //{
        //    if (lastHitObj == GameManager.Singleton.ballLauncher.gameObject)
        //        doBallHitTableLogic = true;
        //}
       

        //Debug.Log("m_curAngleVelocity:" + m_curAngleVelocity);
        //Debug.Log("m_curVelocity:" + m_curVelocity);
        Vector3 normalDir = collision.contacts[0].normal;
        if (collision.gameObject.tag == "Rocket")
        {
            if(m_curBallState == BallState.BS_Ready)
            {
                m_curBallState = BallState.BS_Start;
            }
            bool isPlayerRocket = collision.gameObject.GetComponent<RocketNew>().isPlayer;
            //RecorderForAniAndVR.instance.gameData.beats.Add(new BeatData("1", Time.time));
            Transform rocketColliderObj;
            if (isPlayerRocket)
            {
                rocketColliderObj = collision.gameObject.GetComponent<RocketNew>().colliderObj;
            }
            else
            {
                rocketColliderObj = collision.gameObject.transform;
            }
            //ballAudio.PlayVoice(VoiceType.rocket);
            m_curFrictionCoefficient = rocketFrictionCoefficient;
            m_curLossParameter = rocketLossParameter;
            float angle = Vector3.Angle(collision.contacts[0].normal, rocketColliderObj.forward);

            if (angle>90)
                normalDir = rocketColliderObj.forward;
            else
                normalDir = -rocketColliderObj.forward;
            //normalDir = - collision.gameObject.transform.forward;
            //if(lastHitObj != null && GameManager.Singleton.PingPongNewAI!=null)
            //   GameManager.Singleton.PingPongNewAI.InvokeGetTargetPosAndMove();

            //if(lastHitRocketCollider == null)
            //{
            //    GameManager.Singleton.OnServe();
            //}
            lastHitRocketCollider = collision.gameObject.GetComponent<RocketNew>();
            //collision.gameObject.GetComponent<Collider>().isTrigger = true;
            //if (lastHitObj != null && lastHitObj.GetComponent<RocketNew>())
            //    lastHitObj.GetComponent<RocketNew>().InvokeCollider();
             lastHitObj = collision.gameObject;
            doBallHitRocketLogic = true;
        }
        else if (collision.gameObject.tag == "Table")
        {
            //ballAudio.PlayVoice(VoiceType.table);
            m_curFrictionCoefficient = tableFrictionCoefficient;
            m_curLossParameter = tableLossParameter;
            normalDir = collision.gameObject.transform.up;
            TableHitCollider curTableHitCollider = collision.gameObject.GetComponent<TableHitCollider>();
            if (curTableHitCollider) 
            {
                if (lastHitObj != null)
                {
                    if (lastHitObj.tag == "Table")
                    {
                        //do nothing
                    }
                    else if(lastHitObj.tag == "Rocket")
                    {
                        curTableHitCollider.OnBallHit();
                    }
                    else if(lastHitObj.tag == "Net")
                    {
                        if (severBall)
                        {
                            bool needReSever = false;
                            if (lastHitTableCollider != null && lastHitTableCollider != curTableHitCollider)
                            {
                                needReSever = true;
                            }
                            //GameManager.Singleton.OnBallStateFinish(this, needReSever);//serve ball hit net.then finish.
                            //return;
                        }
                    }
                    doBallHitTableLogic = true;
                }
                else
                {
                    bool needReSever = false;
                    if (!isThrowUp)
                        needReSever = true;
                    //GameManager.Singleton.OnBallStateFinish(this, needReSever);//serve ball not hit rocket.then finish.
                }

                lastHitObj = collision.gameObject;

                if (lastHitTableCollider != null && lastHitTableCollider == curTableHitCollider)
                {
                    //GameManager.Singleton.OnBallStateFinish(this);//hit the same side twice.then finish.
                }
                else
                {
                    lastHitTableCollider = curTableHitCollider;
                }

                if (severBall)
                {
                    //if (curTableHitCollider.playerSide != GameManager.Singleton.playerServe)
                        severBall = false;
                }

            }
        }
        
        //Vector3 normalDir = collision.gameObject.transform.up;
        //Vector3 normalDir = collision.contacts[0].normal;//碰撞時法向向量（待定）

        Vector3 relativeVelocity = m_curVelocity - collision.gameObject.GetComponent<Rigidbody>().velocity;//球相對速度

        Vector3 lineSpeed = SphereCollider.radius * Vector3.Cross(normalDir, m_curAngleVelocity);//碰撞時綫速度

        Vector3 normalVelocity = Vector3.Dot(normalDir, relativeVelocity) * normalDir;//相對速度分解的法向速度

        Vector3 tangentialVector = relativeVelocity - normalVelocity; //相對速度分解的切向速度

        Vector3 contactPointRelativeVelocity = tangentialVector + lineSpeed;//碰撞點相對速度

        Vector3 afterHitNormalDir = -m_curLossParameter * normalVelocity;//碰撞后法向速度

        float mR2 = ballRigidbody.mass * Mathf.Pow(SphereCollider.radius, 2);

        float momentInertia = 0.66f * mR2; //轉動慣量

        Vector3 afterHitTangentialVelocity;//碰撞后的切向速度

        Vector3 afterHitLineVelocity;//碰撞后的綫速度

        bool isDynamicFriction = IsDynamicFriction(momentInertia, mR2, tangentialVector, contactPointRelativeVelocity);
        if (isDynamicFriction)
        {
            afterHitTangentialVelocity = tangentialVector - contactPointRelativeVelocity.normalized * m_curFrictionCoefficient * (1 + m_curLossParameter) * normalVelocity.magnitude;

            afterHitLineVelocity = lineSpeed - contactPointRelativeVelocity.normalized * m_curFrictionCoefficient * (1 + m_curLossParameter) * normalVelocity.magnitude * mR2 / momentInertia;
        }
        else
        {
            afterHitTangentialVelocity = tangentialVector - contactPointRelativeVelocity * (momentInertia / (momentInertia + mR2));

            afterHitLineVelocity = lineSpeed - mR2 / (momentInertia + mR2) * contactPointRelativeVelocity;
        }

        Vector3 afterHitContactPointRelativeVelocity = afterHitNormalDir + afterHitTangentialVelocity;//碰撞后的碰撞點相對速度

        Vector3 angularVelocityIncrement = Vector3.Cross((afterHitLineVelocity - lineSpeed)/ SphereCollider.radius, normalDir);   //角速度增量

        Vector3 afterHitBallVelocity = afterHitContactPointRelativeVelocity + collision.gameObject.GetComponent<Rigidbody>().velocity;

        Vector3 afterHitAngularVelocity = m_curAngleVelocity + angularVelocityIncrement;

        ballRigidbody.velocity = afterHitBallVelocity;
        ballRigidbody.angularVelocity = afterHitAngularVelocity;

        //if (doBallHitRocketLogic)
        //{
        //    GameManager.Singleton.SetCurBallSpeed(afterHitBallVelocity.magnitude);
        //    ballRigidbody.velocity += addUpForceValue * Vector3.up;
        //}

        //if(doBallHitTableLogic && GameManager.Singleton.PingPongNewAI!=null)
        //{
        //    if (GameManager.Singleton.PingPongNewAI.colliderTable.gameObject == collision.gameObject)
        //    {
        //        GameManager.Singleton.PingPongNewAI.GetTargetPosWhenBallHitTable(ballRigidbody);
        //    }
        //    else
        //    {
        //        if(lastHitRocketCollider && lastHitRocketCollider.isPlayer)
        //           GameManager.Singleton.PingPongNewAI.InvokeGetTargetPosAndMove();
        //    }
        //}
        //Debug.Log("!!!!!!!!!!!!!ballRigidbody.velocity "+ ballRigidbody.velocity);
        //UnityEditor.EditorApplication.isPaused = true;
    }

    bool IsDynamicFriction(float _inertia,float _mr2,Vector3 v1,Vector3 v2)//判斷是否動態摩擦
    {
        float value = 
        m_curFrictionCoefficient * (1 + m_curLossParameter) * (1 + _mr2 / _inertia) * (Vector3.Magnitude(v1) / Vector3.Magnitude(v2));

        if (value <= 1)
            return true;
        else
            return false;
    }

    public void OnReset()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        lastHitObj = null;
        lastHitTableCollider = null;
        lastHitRocketCollider = null;
        addTrainingScore = false;
        m_curLifeTime = 0;
        severBall = true;
        isThrowUp = false;
    }

    


    //Vector3 orgPos;
    //Quaternion orgQuaternion;
    //public float forceValue = 10;
    //public float orgForceValue = 200;
    //public float forceValue1 = 1;
    //public GameObject dirObj;
    //public GameObject ballObj;
    //Vector3 ballOrgPos;
    //Quaternion ballOrgQuaternion;
    //private void Start()
    //{
    //    SetTransform();
    //    GetComponent<Rigidbody>();
    //}

    //void SetTransform()
    //{
    //    orgPos = transform.position;
    //    orgQuaternion = transform.rotation;
    //    ballOrgPos = ballObj.transform.position;
    //    ballOrgQuaternion = ballObj.transform.rotation;
    //}
    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.T))
    //    {
    //        SetTransform();
    //    }

    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        GetComponent<Rigidbody>().AddForce(-forceValue * dirObj.transform.forward, ForceMode.Impulse);
    //    }

    //    if (Input.GetKey(KeyCode.R))
    //    {
    //        transform.position = orgPos;
    //        transform.rotation = orgQuaternion;
    //        GetComponent<Rigidbody>().velocity = Vector3.zero;
    //        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    //        ballObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //        ballObj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    //        ballObj.GetComponent<Rigidbody>().useGravity = false;
    //        ballObj.transform.position = ballOrgPos;
    //        ballObj.transform.rotation = ballOrgQuaternion;
    //    }
    //}
    //public ForceMode forceMode;
    //private void OnCollisionEnter(Collision collision)
    //{
    //    //if (collision.gameObject.tag == "Ball")
    //    //{
    //    //    ballObj = collision.gameObject;
    //    //    //ballObj.GetComponent<Rigidbody>().useGravity = true;
    //    //    float angle = Mathf.Abs( Vector3.Angle(transform.forward, Vector3.up)-90);

    //    //    forceValue1 =  orgForceValue * (1 - angle/90);
    //    //    Debug.Log("!!!!!!!!!!!!!!!angle " + angle + " forceValue1 "+ forceValue1);
    //    //    ballObj.GetComponent<Rigidbody>().AddForce(forceValue1 * GetComponent<Rigidbody>().velocity.normalized, forceMode);
    //    //}

    //}




}
