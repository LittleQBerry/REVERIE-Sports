using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WelComePage : MonoBehaviour
{
    public TMP_Text welcomeName;
    // Start is called before the first frame update
    void Start()
    {
        welcomeName.text = UserData.userName + "����ӭʹ��";
    }
    private void OnEnable()
    {
        welcomeName.text = UserData.userName + "����ӭʹ��";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
