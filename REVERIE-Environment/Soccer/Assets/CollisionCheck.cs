using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using static UnityEngine.Networking.UnityWebRequest;

public class CollisionCheck : MonoBehaviour
{
    public Vector3 lastPos;
    public Vector3 lastCollsonObjDir;
    public Vector3 lastFootPos;
    public float angleThreshold = 10.0f;
    public bool hasAddCount = false;
    Vector3 GetEndPoint(Vector3 dir)
    {
        //Matrix4x4 worldToLocalMatrix = transform.worldToLocalMatrix;
        //Vector3 directionInLocalSpace = worldToLocalMatrix.MultiplyVector(dir);
        return transform.position + dir;
    }
    Vector3 playerDir = Vector3.zero;
    Vector3 playerPos = Vector3.zero;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!collision:" + collision.gameObject.name);
        if (collision.gameObject.tag == "Leg")
        {
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!collision:" + collision.gameObject.name);
            lastPos = transform.position;
            lastFootPos = collision.gameObject.GetComponent<snapTracker>().dirFoot.transform.position;
            lastCollsonObjDir = collision.gameObject.GetComponent<snapTracker>().dirFoot.m_dir;
            switch (GameManager.instance.gameType)
            {
                case GameType.传球:
                    float collisionForce = collision.impulse.magnitude;
                    //Debug.Log("传球:"+ collisionForce);
                    RecorderForAniAndVR.instance.gameData.AddBeatForce(collisionForce);
                    //RecorderForAniAndVR.instance.AddCustomEvent("PassBall", RecorderForAniAndVR.instance.CurrnetEvIATime);
                    playerPos = Player.instance.playerPos.transform.position; 
                    playerDir = Player.instance.GetPlayerMoveDir();
                    //IndicatorManager.instance.CreatStraightIndicator(transform.position, GetEndPoint(collision.impulse));
                    RecorderForAniAndVR.instance.gameData.SetBeatState(true);
                    CheckFootHorizontal(collision.gameObject.GetComponent<snapTracker>().dirFoot.transform);//是否垂直以地面
                    CheckFootVertical();//是否两脚垂直
                    CheckHitBallOnCenter(collision);//是否碰到球的后中部位

                    CheckOtherFootDistance();//支撑脚距离是否正确
                    //PassSceneManager.instance.gate.position) -GetHorizontalVec3(transform.position)
                    CheckOtherFootIsOtherSideOfBall(GetHorizontalVec3(collision.impulse));
                    CheckIsRightPart(collision.contacts[0].point, collision.gameObject.GetComponent<FootAttr>());

                        //CheckMoveDirForPass();无法获取准确的移动距离
                        //StartCoroutine(GetMoveDirectionCoroutine(collision));//助跑方向是否是直线
                        
                    break;
                case GameType.射门:
                    collisionForce = collision.impulse.magnitude;
                    RecorderForAniAndVR.instance.gameData.AddBeatForce(collisionForce);
                    RecorderForAniAndVR.instance.gameData.SetBeatState(true);
                    //RecorderForAniAndVR.instance.AddCustomEvent("ShootBall", RecorderForAniAndVR.instance.CurrnetEvIATime);
                    CheckIsFootBackInnerSide(collision.contacts[0].point, collision.gameObject.GetComponent<FootAttr>());//是否为脚背内测
                    //StartCoroutine(MoveDirectionIncline(collision));//助跑方向是否是斜45
                    CheckFootIsRightRot();//脚屈伸是否到位
                    //CheckLeftShoulderAtForeThenRightShoulder();//左肩是否在前
                    CheckOtherFootIsOtherBackOfBall(GetHorizontalVec3(collision.impulse));//支撑脚是否在邂逅方
                    CheckOtherFootPosIsRight();//支撑脚位置是否正确
                    CheckHitBallOnDownCenter(collision);//是否碰到球的后低部位}
                    //StartCoroutine(CheckShootFootDirIsRight(collision));//踢球后摆动幅度是否够准确
                    StartCoroutine(HasInertia());//击球后身体向前惯性移动校验
                    break;
                case GameType.运球:
                    collisionForce = collision.impulse.magnitude;
                    if (collisionForce < 1)
                    {
                        if(Time.time - timeRe > cd)
                        {
                            RecorderForAniAndVR.instance.gameData.AddBeatForce(collisionForce);
                            //RecorderForAniAndVR.instance.AddCustomEvent("CrossBall", RecorderForAniAndVR.instance.CurrnetEvIATime);
                            CheckHitBallOnCenter(collision);//是否碰到球的后中部位
                            CheckArmStreet();
                            CheckBodyForward();
                            CheckFootType(collision.contacts[0].point, collision.gameObject.GetComponent<FootAttr>());
                            RecorderForAniAndVR.instance.bpc.CheckLegStreet();
                            RecorderForAniAndVR.instance.bpc.CheckFootDistance();
                            timeRe = Time.time;
                        }
                    }
                    break;
            }
        }else if(collision.gameObject.tag == "Head")
        {
            float collisionForce = collision.impulse.magnitude;
            RecorderForAniAndVR.instance.gameData.AddBeatForce(collisionForce);
            RecorderForAniAndVR.instance.gameData.SetBeatState(true);
            //RecorderForAniAndVR.instance.AddCustomEvent("HeadBall", RecorderForAniAndVR.instance.CurrnetEvIATime);
            GetMaxSpineAngle();//身体有没有后仰
            CheckBodyStreet();//顶球时机
            CheckArmStreetForHead();
            RecorderForAniAndVR.instance.bpc.CheckLegStreetForHead();
        }
    }
    void CheckBodyForward()
    {
        float newangle = Vector3.Angle(RecorderForAniAndVR.instance.bpc.hip.transform.forward, Vector3.right);
        float angle = newangle - RecorderForAniAndVR.instance.HipAndWorldForwardOffsetAngle;
        angle = Math.Abs(angle);
        if(angle> 90)
        {
            angle =180-angle;
        }
        if (CrossSceneManager.instance != null)
        {
            switch (CrossSceneManager.instance.crossType)
            {
                case CrossType.正脚背运球:
                    string result = RecorderForAniAndVR.instance.DealRuleForBeat(56, angle, "", 0);
                    break;
                case CrossType.外脚背运球:
                    RecorderForAniAndVR.instance.DealRuleForBeat(57, angle, "", 0);
                    break;
                case CrossType.脚内侧运球:
                    RecorderForAniAndVR.instance.DealRuleForBeat(53, angle, "", 0);
                    break;
            }
        }

        
        float angle1 = RecorderForAniAndVR.instance.bpc.GetRightFootAndLegAngle();
        float innerAngle = RecorderForAniAndVR.instance.bpc.GetRightFootInnerAngle();
        //TipUI.instance.ShowDebugText("脚屈伸角度：" + Math.Round( angle1,1) + "----脚内扣角度：" + Math.Round(innerAngle, 1) + "----面向方向角度：" + Math.Round(angle, 1));

    }
    void CheckFootType(Vector3 pos, FootAttr footAttr)
    {
        float angle1 = RecorderForAniAndVR.instance.bpc.GetRightFootAndLegAngle();
        float innerAngle = RecorderForAniAndVR.instance.bpc.GetRightFootInnerAngle();
        if (CrossSceneManager.instance != null)
        {
            switch (CrossSceneManager.instance.crossType)
            {
                case CrossType.正脚背运球:
                    string result = RecorderForAniAndVR.instance.DealRuleForBeat(55, angle1, "", 0);
                    break;
                case CrossType.外脚背运球:
                    RecorderForAniAndVR.instance.DealRuleForBeat(54, innerAngle, "", 0);
                    break;
                case CrossType.脚内侧运球:
                     RecorderForAniAndVR.instance.DealRuleForBeat(35, 0, footAttr.GetPartOfFoot(pos).ToString(), 0);
                    break;
            }
        }
    }
    float cd = 0.2f;
    float timeRe = 0;
    void CheckIsRightPart(Vector3 pos, FootAttr footAttr)
    {

        string result = RecorderForAniAndVR.instance.DealRuleForBeat(35, 0, footAttr.GetPartOfFoot(pos).ToString(), 0);
        TipUI.instance.ShowDebugText(result);

    }
    void CheckMoveDirForPass()
    {
        var goalDir = PassSceneManager.instance.gate.position -GetHorizontalVec3(transform.position);
        playerDir = Player.instance.GetHeadDir();
        float angle = Vector3.Angle(goalDir, playerDir);
       // TipUI.instance.ShowDebugText(angle.ToString());
    }
    void CheckOtherFootIsOtherBackOfBall(Vector3 impulse)
    {
        Vector3 ballDir = new Vector3(impulse.x, 0, impulse.z);
        Vector3 rightDirection = Vector3.Cross(Vector3.up, ballDir);
        Vector3 footDir = transform.position - RecorderForAniAndVR.instance.leftFoot.transform.position;
        float angle = Vector3.Angle(Vector3.forward, footDir);
        RecorderForAniAndVR.instance.DealRuleForBeat(37, angle, "", 0);

    }
    void CheckOtherFootIsOtherSideOfBall(Vector3 impulse)
    {
        Vector3 ballDir = new Vector3(impulse.x,0,impulse.z);
        Vector3 rightDirection = Vector3.Cross(Vector3.up, ballDir);
        Vector3 footDir = transform.position - RecorderForAniAndVR.instance.leftFoot.transform.position;
        float angle = Vector3.Angle(rightDirection, footDir);
        string result = RecorderForAniAndVR.instance.DealRuleForBeat(31, angle, "", 0);


    }
    //void GetMaxKneeAngle()
    //{
        
    //    float angle = RecorderForAniAndVR.instance.bpc.maxAngle;
    //    if (RecorderForAniAndVR.instance.bpc.leftLegMaxAngle < 10f&& RecorderForAniAndVR.instance.bpc.rightLegMaxAngle < 10f)
    //    {
    //        RecorderForAniAndVR.instance.AddBeats("顶球前，重心没有放低","左右腿弯曲角度分别为",RecorderForAniAndVR.instance.bpc.leftLegMaxAngle,RecorderForAniAndVR.instance.bpc.leftLegMaxAngle);
    //    }
    //}
    public bool CheckArmStreet()
    {

        float leftelbowAngle = Vector3.Angle(RecorderForAniAndVR.instance.bpc.leftArm.transform.right, RecorderForAniAndVR.instance.bpc.leftForeArm.transform.right);

        float rightelbowAngle = Vector3.Angle(RecorderForAniAndVR.instance.bpc.rightArm.transform.right, RecorderForAniAndVR.instance.bpc.rightForeArm.transform.right);

        RecorderForAniAndVR.instance.DealRuleForBeat(52, leftelbowAngle, "", rightelbowAngle);

        return true ;
    }
    public bool CheckArmStreetForHead()
    {

        float leftelbowAngle = Vector3.Angle(RecorderForAniAndVR.instance.bpc.leftArm.transform.right, RecorderForAniAndVR.instance.bpc.leftForeArm.transform.right);

        float rightelbowAngle = Vector3.Angle(RecorderForAniAndVR.instance.bpc.rightArm.transform.right, RecorderForAniAndVR.instance.bpc.rightForeArm.transform.right);

        RecorderForAniAndVR.instance.DealRuleForBeat(48, leftelbowAngle, "", rightelbowAngle);

        return true;
    }
    void CheckBodyStreet()
    {
        Vector3 dir = RecorderForAniAndVR.instance.bpc.spine.transform.position - RecorderForAniAndVR.instance.bpc.hip.transform.position;

        float angle = Vector3.Angle(RecorderForAniAndVR.instance.bpc.spine.transform.up,Vector3.up);
        RecorderForAniAndVR.instance.DealRuleForBeat(46, angle,"",0);
        //if (similarity > 0.9f)
        //{
        //    RecorderForAniAndVR.instance.AddBeats("顶球时机不对","顶球时机dir:"+ dir.normalized+"_" + -RecorderForAniAndVR.instance.bpc.hip.transform.up,similarity);
        //}
    }
    void GetMaxSpineAngle()
    {
        float angle = RecorderForAniAndVR.instance.bpc.maxAngle;
        RecorderForAniAndVR.instance.DealRuleForBeat(47,angle, "", 0);
        //if (angle<15f)
        //{
        //    RecorderForAniAndVR.instance.AddBeats("顶球前，身体没有后仰","后仰角度为",angle);
        //}
    }
    IEnumerator HasInertia()
    {
        Vector3 lastHipPos = RecorderForAniAndVR.instance.bpc.hip.transform.position;
        yield return new WaitForSeconds(.2f);
        Vector3 hipPos = RecorderForAniAndVR.instance.bpc.hip.transform.position;
        float distance = Vector3.Distance(lastHipPos, hipPos);
        float speed = distance / 0.2f;
        //if(distance < 0.05)
        //{
        //    RecorderForAniAndVR.instance.AddBeats("击球后身体没有惯性移动", "击球前身体位置"+lastHipPos+"_击球后身体位置"+ hipPos, distance);
        //}
        RecorderForAniAndVR.instance.DealRuleForBeat(40,distance, "", 0);
    }
    IEnumerator CheckShootFootDirIsRight(Collision collision)
    {
        yield return new WaitForSeconds(.2f);
        Vector3 dir = collision.transform.position - lastFootPos;

        float similarity = Vector3.Dot(dir.normalized, -RecorderForAniAndVR.instance.leftFoot.transform.right);
        //if (similarity > 0.9f)
        //{
        //    Debug.Log("踢球腿摆动方向相似");
        //}
        //else
        //{
        //    Debug.Log("踢球腿摆动方向不准确");
        //    RecorderForAniAndVR.instance.AddBeats("踢球腿摆动方向不准确", dir.normalized+"_"+ -RecorderForAniAndVR.instance.leftFoot.transform.right);
        //}
    }
    void CheckOtherFootPosIsRight()
    {
        var distance = Mathf.Abs(RecorderForAniAndVR.instance.leftFoot.transform.position.x-transform.position.x);
        string result = RecorderForAniAndVR.instance.DealRuleForBeat(38, distance-0.15f, "", 0);
        TipUI.instance.ShowDebugText(result.ToString());
    }
    //判断是否在左或后 ，distance大于0 ，则在左侧
    public bool IsOnLeft(GameObject objectA, GameObject objectB)
    {
        Vector3 direction = objectA.transform.position - objectB.transform.position;
        Vector3 forward = objectB.transform.forward;
        Vector3 left = Vector3.Cross(forward, Vector3.up).normalized;
        float distance = Vector3.Dot(direction, left);
        return distance > 0;
    }
    //void CheckLeftShoulderAtForeThenRightShoulder()
    //{
    //    // 将左肩和右肩的位置从世界坐标系转换为本地坐标系
    //    Vector3 leftShoulderLocalPos = transform.InverseTransformPoint(RecorderForAniAndVR.instance.bpc.leftShoulder.transform.position);
    //    Vector3 rightShoulderLocalPos = transform.InverseTransformPoint(RecorderForAniAndVR.instance.bpc.rightShoulder.transform.position);

    //    // 比较左肩和右肩的Z坐标值
    //    if (leftShoulderLocalPos.z < rightShoulderLocalPos.z)
    //    {
    //        Debug.Log("左肩在前，右肩在后");
    //    }
    //    else
    //    {
    //        RecorderForAniAndVR.instance.AddBeats("射门时动作应该左肩在前，右肩在后","", leftShoulderLocalPos.z, rightShoulderLocalPos.z);
    //    }
    //}
    void CheckFootIsRightRot()
    {
        //float angle = RecorderForAniAndVR.instance.bpc.GetRightFootAndLegAngle();
        float innerAngle = RecorderForAniAndVR.instance.bpc.GetRightFootInnerAngle();
        RecorderForAniAndVR.instance.DealRuleForBeat(63, -innerAngle, "", 0);
    }
    void CheckHitBallOnCenter(Collision collision) 
    {
        //Debug.Log("进判断");
        Vector3 contactPoint = collision.contacts[0].point;
        Vector3 direction = contactPoint - transform.position;
        Vector3 directionH = new Vector3(direction.x, 0, direction.z);
        float angle = Vector3.Angle(direction, directionH);
        //TipUI.instance.ShowDebugText(angle.ToString());
         RecorderForAniAndVR.instance.DealRuleForBeat(34, angle, "", 0);
    }
    void CheckHitBallOnDownCenter(Collision collision)
    {

        Vector3 contactPoint = collision.contacts[0].point;
        Vector3 direction = contactPoint - transform.position;
        Vector3 directionH = new Vector3(direction.x, 0, direction.z);
        float angle = Vector3.Angle(direction, directionH);
        

        // 检查碰撞点的Y轴坐标是否小于球的中心点的Y轴坐标
        if (contactPoint.y > transform.position.y)
        {
           // Debug.Log("踢球位置在球的中下部位！");
            string result = RecorderForAniAndVR.instance.DealRuleForBeat(41, angle, "中上部",0);
            
        }
        else
        {
            string result = RecorderForAniAndVR.instance.DealRuleForBeat(41, angle, "中下部",0);
          
        }
    }
    //是否是脚弓碰球
    //void CheckIsFootArc(Collision collision)
    //{
    //    ContactPoint[] contacts = collision.contacts;
    //    Collider footCollider = RecorderForAniAndVR.instance.rightFoot.GetComponent<MeshCollider>();
    //    for (int i = 0; i < contacts.Length; i++)
    //    {
    //        ContactPoint contact = contacts[i];

    //        // 计算接触点与脚的中心点之间的向量
    //        Vector3 toContact = contact.point - footCollider.bounds.center;

    //        // 计算脚的up向量
    //        Vector3 footUp = footCollider.transform.up;

    //        // 计算接触点与脚的up向量之间的夹角
    //        float angle = Vector3.Angle(toContact, footUp);

    //        // 计算接触点在脚的局部坐标系中的位置
    //        Vector3 localContact = footCollider.transform.InverseTransformPoint(contact.point);

    //        // 判断接触点是否在脚弓上
    //        if (angle < 30 && localContact.y < 0.1)
    //        {
    //            Debug.Log("碰到了脚弓处");
    //            return;
    //        }
    //        else
    //        {
    //            RecorderForAniAndVR.instance.AddBeats("没有用脚弓内测击球","脚弓内测Angle为："+angle+ ",localContact.y："+ localContact.y, angle, localContact.y);
    //        }
    //    }
    //}

    void CheckIsFootBackInnerSide(Vector3 point, FootAttr footAttr)
    {
        RecorderForAniAndVR.instance.DealRuleForBeat(42,0, footAttr.GetPartOfFoot(point).ToString(),0);
    }
    void CheckOtherFootDistance()
    {
        float distance = Vector3.Distance(RecorderForAniAndVR.instance.leftFoot.transform.position, transform.position);
        //TipUI.instance.ShowDebugText(distance.ToString());
        RecorderForAniAndVR.instance.DealRuleForBeat(32, distance, "", 0);
    }

    void CheckFootHorizontal(Transform foot)
    {
        float angle = Vector3.Angle(Vector3.up, foot.up);
        RecorderForAniAndVR.instance.DealRuleForBeat(51, angle, "", 0);

    }
    void CheckFootVertical()
    {
        float angle = Vector3.Angle(RecorderForAniAndVR.instance.leftFoot.transform.forward, -RecorderForAniAndVR.instance.rightFoot.transform.right);
        RecorderForAniAndVR.instance.DealRuleForBeat(33, angle, "", 0);
    }
    //IEnumerator GetMoveDirectionCoroutine(Collision collision)
    //{
    //    yield return new WaitForSeconds(.2f);
    //    Vector3 dir = transform.position- lastPos;

    //    float similarity = Vector3.Dot(dir.normalized, lastCollsonObjDir.normalized);
    //    if (similarity > 0.9f)
    //    {
    //        //Debug.Log("助跑方向与出球方向一致");
    //    }
    //    else
    //    {
    //        RecorderForAniAndVR.instance.AddBeats("助跑方向与出球方向不一致","助跑方向与出球方向分别为:"+ dir.normalized +"_"+ lastCollsonObjDir.normalized, similarity);
    //    }

    //}
    //IEnumerator MoveDirectionIncline(Collision collision)
    //{
    //    yield return new WaitForSeconds(.2f);
    //    Vector3 dir = transform.position - lastPos;

    //    float angle = Vector3.Angle(dir.normalized, lastCollsonObjDir.normalized);

    //    if (angle > 30f&& angle<50f)
    //    {
    //        Debug.Log("助跑路线为45°左右");
    //    }
    //    else
    //    {
    //        RecorderForAniAndVR.instance.AddBeats("助跑路线不正确", "angle:"+ angle, angle);
    //    }
    //}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 dir = GetHorizontalVec3(PassSceneManager.instance.gate.position) - GetHorizontalVec3(transform.position);
        ////IndicatorManager.instance.CreatStraightIndicator(Vector3.zero, dir);
        //Debug.DrawRay(GetHorizontalVec3(transform.position), dir, Color.red);
        //Debug.DrawRay(playerPos, playerDir, Color.yellow);
    }


    Vector3 GetHorizontalVec3(Vector3 dir)
    {
        return new Vector3(dir.x,0,dir.z);
    }
}
