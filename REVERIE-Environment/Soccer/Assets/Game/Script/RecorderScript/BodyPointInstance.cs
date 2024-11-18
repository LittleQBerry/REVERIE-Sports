using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPointInstance : MonoBehaviour
{
    [Tooltip("躯干")]
    public GameObject head;
    public GameObject hip;
    public GameObject leftShoulder;
    public GameObject rightShoulder;
    public GameObject spine;
    [Tooltip("手臂")]
    public GameObject leftArm;
    public GameObject leftForeArm;
    public GameObject lefthand;
    public GameObject rightArm;
    public GameObject rightForeArm;
    public GameObject righthand;
    [Tooltip("腿")]
    public GameObject leftUpLeg;
    public GameObject leftLeg;
    public GameObject leftFoot;
    public GameObject rightUpLeg;
    public GameObject rightLeg;
    public GameObject rightFoot;


    [Tooltip("躯干演员")]
    public GameObject headActor;
    public GameObject hipActor;
    public GameObject leftShoulderActor;
    public GameObject rightShoulderActor;
    public GameObject spineActor;
    [Tooltip("手臂演员")]
    public GameObject leftArmActor;
    public GameObject leftForeArmActor;
    public GameObject lefthandActor;
    public GameObject rightArmActor;
    public GameObject rightForeArmActor;
    public GameObject righthandActor;
    [Tooltip("腿演员")]
    public GameObject leftUpLegActor;
    public GameObject leftLegActor;
    public GameObject leftFootActor;
    public GameObject rightUpLegActor;
    public GameObject rightLegActor;
    public GameObject rightFootActor;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (headActor != null)
        {
            head.transform.position = headActor.transform.position;
            head.transform.rotation = headActor.transform.rotation;
        }
        else
        {
            headActor = GameObject.Find("head");
        }
        if (hipActor != null)
        {
            hip.transform.position = hipActor.transform.position;
            hip.transform.rotation = hipActor.transform.rotation;
        }
        else
        {
            hipActor = GameObject.Find("hip");
        }
        if (leftShoulderActor != null)
        {
            leftShoulder.transform.position = leftShoulderActor.transform.position;
            leftShoulder.transform.rotation = leftShoulderActor.transform.rotation;
        }
        else
        {
            leftShoulderActor = GameObject.Find("leftShoulder");
        }

        if (rightShoulderActor != null)
        {
            rightShoulder.transform.position = rightShoulderActor.transform.position;
            rightShoulder.transform.rotation = rightShoulderActor.transform.rotation;
        }
        else
        {
            rightShoulderActor = GameObject.Find("rightShoulder");
        }
        if (spineActor != null)
        {
            spine.transform.position = spineActor.transform.position;
            spine.transform.rotation = spineActor.transform.rotation;
        }
        else
        {
            spineActor = GameObject.Find("spine");
        }
        if (leftArmActor != null)
        {
            leftArm.transform.position = leftArmActor.transform.position;
            leftArm.transform.rotation = leftArmActor.transform.rotation;
        }
        else
        {
            leftArmActor = GameObject.Find("leftArm");

        }
        if (leftForeArmActor != null)
        {
            leftForeArm.transform.position = leftForeArmActor.transform.position;
            leftForeArm.transform.rotation = leftForeArmActor.transform.rotation;
        }
        else
        {
            leftForeArmActor = GameObject.Find("leftForeArm");

        }
        if (lefthandActor != null)
        {
            lefthand.transform.position = lefthandActor.transform.position;
            lefthand.transform.rotation = lefthandActor.transform.rotation;
        }
        else
        {
            lefthandActor = GameObject.Find("lefthand");

        }
        if (rightArmActor != null)
        {
            rightArm.transform.position = rightArmActor.transform.position;
            rightArm.transform.rotation = rightArmActor.transform.rotation;
        }
        else
        {
            rightArmActor = GameObject.Find("rightArm");
        }
        if (rightForeArmActor != null)
        {
            rightForeArm.transform.position = rightForeArmActor.transform.position;
            rightForeArm.transform.rotation = rightForeArmActor.transform.rotation;
        }
        else
        {
            rightForeArmActor = GameObject.Find("rightForeArm");
        }
        if (righthandActor != null)
        {
            righthand.transform.position = righthandActor.transform.position;
            righthand.transform.rotation = righthandActor.transform.rotation;
        }
        else
        {
            righthandActor = GameObject.Find("righthand");
        }
        if (leftUpLegActor != null)
        {
            leftUpLeg.transform.position = leftUpLegActor.transform.position;
            leftUpLeg.transform.rotation = leftUpLegActor.transform.rotation;
        }
        else
        {
            leftUpLegActor = GameObject.Find("leftUpLeg");
        }
        if (leftLegActor != null)
        {
            leftLeg.transform.position = leftLegActor.transform.position;
            leftLeg.transform.rotation = leftLegActor.transform.rotation;
        }
        else
        {
            leftLegActor = GameObject.Find("leftLeg");
        }
        if (leftFootActor != null)
        {
            leftFoot.transform.position = leftFootActor.transform.position;
            leftFoot.transform.rotation = leftFootActor.transform.rotation;
        }
        else
        {
            leftFootActor = GameObject.Find("leftFoot");
        }
        if (rightLegActor != null)
        {
            rightLeg.transform.position = rightLegActor.transform.position;
            rightLeg.transform.rotation = rightLegActor.transform.rotation;
        }
        else
        {
            rightLegActor = GameObject.Find("rightLeg");
        }
        if (rightFootActor != null)
        {
            rightFoot.transform.position = rightFootActor.transform.position;
            rightFoot.transform.rotation = rightFootActor.transform.rotation;
        }
        else
        {
            rightFootActor = GameObject.Find("rightFoot");
        }
        if (rightUpLegActor != null)
        {
            rightUpLeg.transform.position = rightUpLegActor.transform.position;
            rightUpLeg.transform.rotation = rightUpLegActor.transform.rotation;
        }
        else
        {
            rightUpLegActor = GameObject.Find("rightUpLeg");
        }
    }
}
