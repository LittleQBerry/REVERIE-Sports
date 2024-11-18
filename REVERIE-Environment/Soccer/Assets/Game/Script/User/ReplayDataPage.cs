
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ReplayDataPage : MonoBehaviour
{
    public Transform content;
    public GameObject slot;
    public void FillData()
    {
        Clear();
        var data = GetData();
        for(int i=0;i< data.Length; i++)
        {
            var d = data[i]; 
            if (d == null) continue;
            GameObject go = GetFreeSlot();
            go.gameObject.SetActive(true);
            go.transform.SetParent(content);
            go.transform.localScale = Vector3.one;
            go.GetComponent<ReplayDataSlot>().SetData(d);
        }
    }
   
    public string[] GetData()
    {
        var dirs = Directory.GetDirectories(Application.dataPath + "/StreamingAssets/"+UserData.userName);
        return dirs;

    }
    public GameObject GetFreeSlot()
    {
        for(int i = 0; i < content.childCount; i++)
        {
            if (!content.GetChild(i).gameObject.activeSelf)
            {
                return content.GetChild(i).gameObject;  
            }
        }
        return Instantiate(slot);
    }
    public void Clear()
    {
        for(int i = 0; i < content.childCount; i++)
        {
            content.GetChild(i).gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        FillData();
    }
   
}
