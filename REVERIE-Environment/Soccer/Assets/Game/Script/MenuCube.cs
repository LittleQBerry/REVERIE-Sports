using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuCube : MonoBehaviour
{
    public MenuOBJEnum menuOBJEnum;
    public Text text;
    Vector3 startPos;
    // Use this for initialization
    public void Start()
    {
        //startPos = transform.position;
    }
    public void SetBaseInfo(MenuOBJEnum oBJEnum)
    {
        startPos = transform.position;
        menuOBJEnum = oBJEnum;
        text.text = oBJEnum.ToString();
        //switch (menuOBJEnum)
        //{
        //    case MenuOBJEnum.头球训练:
        //        Destroy(transform.GetComponent<Rigidbody>());
        //        text.text = oBJEnum.ToString()+"暂未开放";
        //        break;
        //    case MenuOBJEnum.射门训练:
        //        Destroy(transform.GetComponent<Rigidbody>());
        //        text.text = oBJEnum.ToString() + "暂未开放";
        //        break;
        //    case MenuOBJEnum.传球训练:
        //        Destroy(transform.GetComponent<Rigidbody>());
        //        text.text = oBJEnum.ToString() + "暂未开放";
        //        break;
        //}
        


    }
    public void Update()
    {
        if(Vector3.Distance(startPos, transform.position) > 0.5)
        {
            //if(menuOBJEnum != MenuOBJEnum.中级训练&& menuOBJEnum != MenuOBJEnum.初级训练 && menuOBJEnum != MenuOBJEnum.高级训练 && menuOBJEnum != MenuOBJEnum.退出菜单 && GameManager.instance != null)
            //{
            //    GameManager.instance.FinishGame();
            //}
            MainSceneManager.instance.JumpScene(menuOBJEnum);   
           
        }
    }

}
