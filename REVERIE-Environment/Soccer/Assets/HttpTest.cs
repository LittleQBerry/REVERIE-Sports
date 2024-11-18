
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        
    }
    void dealSetting(string param)
    {
        List<SettingOffset> list = JsonConvert.DeserializeObject<List<SettingOffset>>(param);
        SettingOffset settingOffset = list[0];
        Debug.Log(settingOffset.angleA_max+ "    "+settingOffset.footBounceseMode);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
