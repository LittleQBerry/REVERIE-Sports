using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCatchManager : MonoBehaviour
{
    public static PassCatchManager instance;
    public PlayMakerFSM fSM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Next()
    {
        fSM.SendEvent("Next");

    }
}
