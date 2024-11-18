using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressUI : MonoBehaviour
{
    public Image circle;
    public Text text;
    float m_time;
    float timer;
    bool isShow = false;

    public void TipWithTime(string content,float time)
    {
        m_time = time;
        isShow = true;
        text.text = content;
        circle.fillAmount = 0;
        ShowUI(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        ShowUI(false);
        //TipWithTime("sasdfasdfasdfa",4);
    }

    // Update is called once per frame
    void Update()
    {
        if (isShow)
        {
            timer += Time.deltaTime;
            circle.fillAmount = timer / m_time;
            if (circle.fillAmount>=1)
            {
                isShow = false;
                timer = 0;
                ShowUI(false);
            }
        }
    }
    public void ShowUI(bool status)
    {
        circle.gameObject.SetActive(status);
        text.gameObject.SetActive(status);
        timer = 0;
    }
}
