using HurricaneVR.Framework.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPoint : MonoBehaviour
{

    float countTime = 0;
    public float timer = 3f;
    bool showSceneStatus = false;
    
    // Start is called before the first frame update
    void Start()
    {
        showSceneStatus = false;
        countTime = 0;

    }

    // Update is called once per frame
    void Update()
    {


    }
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Leg"))
        {
            Debug.Log("OnTriggerStay");
            countTime += Time.deltaTime;
            if (countTime > 3&&!showSceneStatus)
            {
                MainSceneManager.instance.ShowMenu(true);
                showSceneStatus = true;
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Leg"))
        {
            TipUI.instance.ShowProgressTip("«Î±£≥÷3√Î", 2.5f);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Leg"))
        {
            countTime = 0;
            showSceneStatus = false;
            TipUI.instance.ShowTip(false);
        }
    }
}
