using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TipUI : MonoBehaviour
{
    public static TipUI instance;
    public ProgressUI progressUI;
    public Text debugText;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    public void ShowDebugText(string content)
    {
        debugText.text = content;
    }
    public void ShowProgressTip(string content,float time = 1f)
    {
        progressUI.TipWithTime(content, time);
    }
    public void ShowTip(bool status)
    {
        progressUI.ShowUI(status);
    }
    // Update is called once per frame
    void Update()
    {
        //float angle = Vector3.Angle(RecorderForAniAndVR.instance.leftFoot.transform.forward, -RecorderForAniAndVR.instance.rightFoot.transform.right);
        //ShowDebugText(angle.ToString());
    }
}
