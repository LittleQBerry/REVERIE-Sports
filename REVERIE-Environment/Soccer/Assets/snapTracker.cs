using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class snapTracker : MonoBehaviour
{
    Rigidbody rigidbody;
    public Transform tracker;
    public AudioSource audioS;
    public AudioClip ac;
    public LayerMask layer;
    public Transform foot;
    public Transform vrOrigin;
    public Transform grass;
    public RecordTransform dirFoot;
    bool onSnap;
    // Start is called before the first frame update
    private void Start()
    {
        onSnap = false;
    }
    public void SetFoot()
    {
        onSnap = true;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = true;
        StartCoroutine(ResetEiFoot());
    }
    IEnumerator ResetEiFoot()
    {
        yield return new WaitForSeconds(0.1f);
        ResetFoot();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetFoot();
        }
        //if(gameObject.name == "RightFoot")
        //{
        //    float angle = Vector3.Angle(Vector3.up, dirFoot.transform.up);
        //    TipUI.instance.ShowDebugText(angle.ToString());
        //}
        
    }
    public void ResetFoot()
    {
        if (foot != null)
        {
            RaycastHit hitInfo;   
            if(Physics.Raycast(transform.position+new Vector3(0,2,0),-Vector3.up, out hitInfo, Mathf.Infinity, layer))
            {
                foot.position = hitInfo.point + new Vector3(0, 0.1f, 0);
            }


            // ��ȡ����B���Ϸ�����������C�ķ���������
            Vector3 upVectorB = grass.transform.up;
            Vector3 directionC = (vrOrigin.transform.position - foot.transform.position).normalized;

            // ������A���Ϸ���������Ϊ����B���Ϸ�������
            foot.transform.up = upVectorB;

            // ʹ��������˼��������A���ҷ���������
            Vector3 rightVectorA = Vector3.Cross(directionC, upVectorB).normalized;

            // ʹ��������˼��������A��ǰ����������
            //Vector3 forwardVectorA = Vector3.Cross(upVectorB, rightVectorA).normalized;
            Vector3 horizontalForward = Vector3.ProjectOnPlane(vrOrigin.forward, Vector3.up).normalized;

            // ������A�ı�������ϵ����Ϊ��ǰ������Ϊǰ���Ϸ�����Ϊ�ϣ��ҷ�����Ϊ�ҵ�����ϵ��
            foot.transform.rotation = Quaternion.LookRotation(horizontalForward, upVectorB);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.layer== layer.value)
        //{
        //    audioS.PlayOneShot(ac);
        //}
    }
    private void FixedUpdate()
    {
        if (onSnap)
        {
            rigidbody.MovePosition(tracker.position);
            rigidbody.MoveRotation(tracker.rotation);
        }
        
    }

}
