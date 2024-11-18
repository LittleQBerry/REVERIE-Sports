using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.HandPoser;
using HurricaneVR.Framework.Shared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFootUp : MonoBehaviour
{
    public static SetFootUp instance;


    public HVRHandGrabber LeftGrabber;
    public HVRGrabbable LeftFoot;
    public HVRGrabTrigger LGrabTrigger;
    public HVRPosableGrabPoint LeftFootPoint;


    public HVRHandGrabber RightGrabber;
    public HVRGrabbable RightFoot;
    public HVRGrabTrigger RGrabTrigger;
    public HVRPosableGrabPoint RightFootPoint;


    public Transform L_letOrign;
    public Transform R_letOrign;
    public float offset = 0.15f;
    public Transform leftLeg;
    public Transform rightLeg;

    public float forwardOffset = 0.05f;

    public bool inMenuScene;
    //public Transform ball;
    //GameObject currentBall;
   // Vector3 startPos;
    float timer = 0;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        RightFoot.GetComponent<Rigidbody>().isKinematic = true;
        LeftFoot.GetComponent<Rigidbody>().isKinematic = true;
        //startPos = ball.position;
        //currentBall = ball.gameObject; 
        //StartCoroutine(GrabFoot());
        if (!inMenuScene)
        {
            StartCoroutine(RepairAndStartGame());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Vector3.Distance(currentBall.transform.position, startPos) > 0.3f)
        //{
        //    timer += Time.deltaTime;
        //    if (timer > 5)
        //    {
        //        var newBall = Instantiate(ball);
        //        newBall.position = startPos;
        //        currentBall = newBall.gameObject;
        //        timer = 0;
        //    }
        //}
    }
    public void  GrabFoot()
    {
        RightFoot.GetComponent<Rigidbody>().isKinematic = false;
        LeftFoot.GetComponent<Rigidbody>().isKinematic = false;
        if (!RightGrabber.IsGrabbing)
            RightGrabber.Grab(RightFoot, RGrabTrigger, RightFootPoint);
        if(!LeftGrabber.IsGrabbing)
            LeftGrabber.Grab(LeftFoot, LGrabTrigger, LeftFootPoint);
    }

    public void SettingPos()
    {
        //Debug.Log("sss");
        RaycastHit hit;

        // Vector3 point_dir = L_letOrign.transform.TransformDirection(-Vector3.up);
        Vector3 point_dir = -Vector3.up;// L_letOrign.transform.TransformDirection(-Vector3.up);
        if (Physics.Raycast(L_letOrign.transform.position + new Vector3(0, 0, 0.5f), point_dir, out hit, 1000f, LayerMask.GetMask("SSSS")))
        {
            Quaternion nextRot = Quaternion.LookRotation(Vector3.Cross(hit.normal, Vector3.Cross(-L_letOrign.transform.forward, hit.normal)), hit.normal);
            leftLeg.rotation = nextRot;
            leftLeg.position = new Vector3(leftLeg.position.x, hit.distance - (hit.distance - offset), leftLeg.position.z+ MainSceneManager.instance.GetFootOffset());
        }
        Vector3 point_dir1 = -Vector3.up; // R_letOrign.transform.TransformDirection(-Vector3.up);
        if (Physics.Raycast(R_letOrign.transform.position + new Vector3(0, 0, 0.5f), point_dir1, out hit, 1000f, LayerMask.GetMask("SSSS")))
        {
            Quaternion nextRot = Quaternion.LookRotation(Vector3.Cross(hit.normal, Vector3.Cross(R_letOrign.transform.forward, hit.normal)), hit.normal);
            rightLeg.rotation = nextRot;
            rightLeg.position = new Vector3(rightLeg.position.x, hit.distance - (hit.distance - offset), rightLeg.position.z+ MainSceneManager.instance.GetFootOffset());
        }
    }

    public IEnumerator RepairAndStartGame()
    {
        Debug.Log("TipUI.instance"+TipUI.instance);
        Debug.Log("GameManager.instance" + GameManager.instance);
        TipUI.instance.ShowProgressTip("正在初始化"+GameManager.instance.gameType+ "训练,请站在原地保持不动。",3f);
        yield return new WaitForSeconds(4f);
        GrabFoot();
        TipUI.instance.ShowProgressTip("正在校验脚部位置，请稍后", 2f);
        yield return new WaitForSeconds(3f);
        SettingPos();
        yield return new WaitForSeconds(3f);
        TipUI.instance.ShowProgressTip("3秒后开始训练", 2f);
        GameManager.instance.Init();
    }
    public IEnumerator ResetShoes()
    {
        yield return new WaitForSeconds(2f);
        GrabFoot();
        yield return new WaitForSeconds(3f);
        SettingPos();
    }

}
