using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Duck.Http;
using Newtonsoft.Json;

public class UserAccount : MonoBehaviour
{
    public TMP_Text tips;
    public TMP_InputField accountName;

    public GameObject loginPage;
    public GameObject switchPage;
    public GameObject replayDataPage;
    string url = "http://124.221.101.31";
    // Start is called before the first frame update
    void Start()
    {
        StartScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Login()
    {
        CheckLogin();
    }
    public void StartScene()
    {
        if (UserData.userName != "")
        {
            loginPage.SetActive(false);
            switchPage.SetActive(false);
            replayDataPage.SetActive(true);
            tips.text = "";
        }
        else
        {
            loginPage.SetActive(true);
            switchPage.SetActive(false);
            replayDataPage.SetActive(false);
            tips.text = "";
        }
    }
    public void JumpStartPage()
    {

            loginPage.SetActive(true);
            switchPage.SetActive(false);
            replayDataPage.SetActive(false);
            tips.text = "";
        
    }
    public void JumpSwitchPage()
    {
        loginPage.SetActive(false);
        switchPage.SetActive(true);
        replayDataPage.SetActive(false);
    }
    public void JumpReplayDataPage()
    {
        loginPage.SetActive(false);
        switchPage.SetActive(false);
        replayDataPage.SetActive(true);
    }
    public void JumpVRCrossTranningScene()
    {
        SceneManager.LoadScene("FootBallCrossingForHtc");
    }
    public void JumpVRShootTranningScene()
    {
        SceneManager.LoadScene("FootBallShootingForHtc");
    }
    public void JumpVRHeadTranningScene()
    {
        SceneManager.LoadScene("FootBallHeadForHtc");
    }
    public void JumpVRPassTranningScene()
    {
        SceneManager.LoadScene("FootBallPassCatchingForHtc");
    }
    public void JumpReplayScene()
    {

    }

    void LoadStudentData()
    {
        var para = new Dictionary<string, string>() { { "name", UserData.userName } };
        Http.Post(url + "/school/unity/list", para)
              .OnSuccess(response => DealMessage(response.Text))
              .OnError(response => Debug.Log("获取数据失败！"))
              .OnDownloadProgress(progress => Debug.Log(progress))
              .Send();
    }
    bool CheckLogin()
    {
        if (accountName.text == String.Empty)
        {
            tips.text = "请填写用户名";
            return false;
        }
        UserData.userName = accountName.text;
        LoadStudentData();
        return false;
    }
    void DealMessage(string param)
    {
        AllUserData allUser = JsonConvert.DeserializeObject<AllUserData>(param);
        if (allUser.code == 0)
        {
            JumpSwitchPage();
        }
        else
        {
            tips.text = "无此用户";
            return;
        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
public class AllUserData
{
    public int total;
    public List<TranningSingleData> rows;
    public int code;
    public string msg;
}
public class TranningSingleData
{
    public string beatDataUrl;
    public string realName;
    public string recordDataUrl;
    public string tranType;
    public string tranName;
    public string tranTime;
    public string tranLevel;
    public string tranScore;
}