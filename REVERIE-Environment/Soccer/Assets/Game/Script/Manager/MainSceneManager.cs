using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Newtonsoft.Json;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager instance;
    public GameType gameType = GameType.运球;
    Transform player;
    public Transform Player { get { if (player != null) return player; else return GameObject.Find("Player").transform; } }

    public GameObject menuCubes;
    public Transform[] menuPoints;

    bool OnPause = false;
    List<GameObject> menuList;
    public List<MenuOBJEnum> menuOBJEnums;

    public SettingOffset settingOffset;


    public void HttpTest()
    {
        //var request = Http.Get("http://mywebapi.com/")
        //.SetHeader("Authorization", "username:password")
        //.OnSuccess(response => Debug.Log(response.Text))
        //.OnError(response => Debug.Log(response.StatusCode))
        //.OnDownloadProgress(progress => Debug.Log(progress))
        //.Send();
        //JsonConvert.DeserializeObject<List<SubjctInfo>>(param);
    }
    public BallAttr GetBallAttr()
    {
        if (GameManager.instance != null)
        {
          
            for(int i=0;i< settingOffset.ballAttrs.Count; i++)
            {
                if(settingOffset.ballAttrs[i].gameType == GameManager.instance.gameType)
                {
                    return settingOffset.ballAttrs[i];
                }
            }
        }
        return null;
    }

    public float GetBallLifeTime()
    {
        if (GameManager.instance != null)
        {

            for (int i = 0; i < settingOffset.ballAttrs.Count; i++)
            {
                if (settingOffset.ballLifeTimes[i].gameType == GameManager.instance.gameType&& settingOffset.ballLifeTimes[i].level ==settingOffset.gameLevel)
                {
                    return settingOffset.ballLifeTimes[i].lifeTime;
                }
            }
        }
        return 20;
    }
    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start()
    {
        menuList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (InputBridge.Instance.XButtonUp)
        //{
        //    if (!OnPause)
        //    {
        //        ShowMenu(true);
        //        GameManager.instance.SetPause(true);
        //        OnPause = true;
        //    }
        //    else
        //    {
        //        ShowMenu(false);
        //        GameManager.instance.SetPause(false);
        //        OnPause = false;
        //    }
        //}
    }
    public void JumpScene(MenuOBJEnum menuOBJEnum)
    {
        switch (menuOBJEnum)
        {
            case MenuOBJEnum.运球训练:
                SceneManager.LoadScene("FootBallCrossingForHtc");
                break;
            case MenuOBJEnum.传球训练:
                SceneManager.LoadScene("FootBallPassCatchingForHtc");
                break;
            case MenuOBJEnum.射门训练:
                SceneManager.LoadScene("FootBallShootingForHtc");
                break;
            case MenuOBJEnum.头球训练:
                SceneManager.LoadScene("FootBallHeadForHtc");
                break;
            case MenuOBJEnum.初始场景:  
                SceneManager.LoadScene("MenuScene");
                break;
            case MenuOBJEnum.重新开始:
                GameManager.instance.ResetCurrentScene();
                break;
            case MenuOBJEnum.退出菜单:
                ShowMenu(false);
                break;
            case MenuOBJEnum.退出训练:
                GameManager.instance.QuitGame();
                break;
            case MenuOBJEnum.初级训练:
                Debug.Log("in 初级训练");
                SceneBaseManager.instance.menuObj.gameObject.SetActive(true);
                settingOffset.gameLevel = 1;
                this.gameObject.SetActive(false);
                break;
            case MenuOBJEnum.中级训练:
                SceneBaseManager.instance.menuObj.gameObject.SetActive(true);
                settingOffset.gameLevel = 2;
                this.gameObject.SetActive(false);
                break;
            case MenuOBJEnum.高级训练:
                SceneBaseManager.instance.menuObj.gameObject.SetActive(true);
                settingOffset.gameLevel = 3;
                this.gameObject.SetActive(false);
                break;


        }
    }

    internal float GetFootOffset()
    {
        return settingOffset.forwardFootOffset;
    }

    public  void ShowMenu(bool status)
    {
        Debug.Log("ShowMenu + "+ status);
        if (menuList.Count == 0)
        {
            if (status)
            {
                if (menuOBJEnums.Count == menuPoints.Length)
                {
                    for (int i = 0; i < menuOBJEnums.Count; i++)
                    {
                        var menuObj = Instantiate(menuCubes);
                        menuObj.transform.parent = transform;
                        menuObj.transform.localPosition = menuPoints[i].localPosition;
                        menuObj.transform.localRotation = menuPoints[i].localRotation;
                        menuObj.GetComponent<MenuCube>().SetBaseInfo(menuOBJEnums[i]);
                        menuList.Add(menuObj);
                        Debug.Log("for + " + status);
                    }
                }
            }
        }
        
        if(menuList.Count>0&&!status)
        {
            foreach(var obj in menuList)
            {
                Destroy(obj);
            }
            menuList.Clear();
        }

    }

    }
    public enum MenuOBJEnum
{
    运球训练,
    传球训练,
    射门训练,
    头球训练,
    重新开始,
    退出训练,
    退出菜单,
    初始场景,
    初级训练,
    中级训练,
    高级训练
}
