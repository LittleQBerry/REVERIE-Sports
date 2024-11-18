using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearShose : MonoBehaviour
{
    float countTime = 0;
    bool hasWearShose = false;
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("XRMan"))
        {
            countTime = 0;
            hasWearShose = false;
            TipUI.instance.ShowTip(false);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("XRMan"))
        {
            countTime += Time.deltaTime;
            if (countTime > 3&& !hasWearShose)
            {
                hasWearShose = true;
                StartCoroutine(SetFootUp.instance.ResetShoes());
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("XRMan"))
        {
            TipUI.instance.ShowProgressTip("正在重新校验脚步，请稍等5秒中",4.5f);
        }
    }


}
