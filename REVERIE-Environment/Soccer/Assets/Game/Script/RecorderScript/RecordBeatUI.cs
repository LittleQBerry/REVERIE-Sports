using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RecordBeatUI : MonoBehaviour
{
    public Transform content;
    public GameObject slot;

    public RecordContrUI recordContrUI;
    public void FillData(GameData gameData)
    {
        Clear();
        var data = gameData.beats;
        for (int i = 0; i < data.Count; i++)
        {
            var d = data[i];
            if (d == null) continue;
            GameObject go = GetFreeSlot();
            go.gameObject.SetActive(true);
            go.transform.SetParent(content);
            go.transform.localScale = Vector3.one;
            //go.GetComponent<BeatDataSlot>().SetData(d);
        }
    }
    public void OnChangeButton(BeatData _beatData)
    {
        recordContrUI.PlayRecord(_beatData);
    }
    public string[] GetData()
    {
        var dirs = Directory.GetDirectories(Application.dataPath + "/StreamingAssets/" + UserData.userName);
        return dirs;

    }
    public GameObject GetFreeSlot()
    {
        for (int i = 0; i < content.childCount; i++)
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
        for (int i = 0; i < content.childCount; i++)
        {
            content.GetChild(i).gameObject.SetActive(false);
        }
    }
    
}
