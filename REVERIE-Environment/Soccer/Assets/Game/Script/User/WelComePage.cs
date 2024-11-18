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
        welcomeName.text = UserData.userName + "，欢迎使用";
    }
    private void OnEnable()
    {
        welcomeName.text = UserData.userName + "，欢迎使用";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
