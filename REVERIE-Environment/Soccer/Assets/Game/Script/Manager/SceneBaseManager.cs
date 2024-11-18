using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBaseManager : MonoBehaviour
{
    public static SceneBaseManager instance;
    //public Transform wearObj;
    public Transform menuObj;
    //public Transform leftLeg;
    //public Transform rightLeg;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
