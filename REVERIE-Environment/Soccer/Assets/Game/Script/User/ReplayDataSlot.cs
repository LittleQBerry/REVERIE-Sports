using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReplayDataSlot : MonoBehaviour
{
    public TMP_Text projectName;
    public TMP_Text dateTime;
    public TMP_Text level;
    public TMP_Text score;
    public string dirName;
    public void SetData(string data)
    {
        var datas = data.Split('_');
        var dirname = datas[0].Split('\\');
        projectName.text = dirname[1];
        dateTime.text = datas[1];
        level.text = datas[2];
        score.text = datas[3];
        dirName = data.Split('\\')[1];
        Debug.Log(dirName);
    }

    public void JumpReplayScene()
    {
        UserData.currentDir = dirName;
        SceneManager.LoadScene("RelayScene");
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
