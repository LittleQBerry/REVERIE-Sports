using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForRecModel : MonoBehaviour
{
    public CameraPos cameraType;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("hip");
            transform.SetParent(player.transform);
            SetPosByType();
        }
    }
    void SetPosByType()
    {
        switch (cameraType)
        {
            case CameraPos.left:
                transform.localPosition = new Vector3(-20,1.5f,0);
                transform.localEulerAngles = new Vector3(4,90,0);
                break;
            case CameraPos.middle:
                transform.localPosition = new Vector3(0, 1.5f, 20);
                transform.localEulerAngles = new Vector3(4, -180, 0);
                break;
            case CameraPos.right:
                transform.localPosition = new Vector3(20, 1.5f, 0);
                transform.localEulerAngles = new Vector3(4, -90, 0);
                break;
        }
    }
}
public enum CameraPos
{
    left,
    right,
    middle
}
