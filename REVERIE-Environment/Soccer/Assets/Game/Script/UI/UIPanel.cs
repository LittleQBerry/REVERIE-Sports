using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIPanel : MonoBehaviour
{
    public Text m_currentSceneText;
    public Text m_timeText;
    public Text m_braffleCountText;
    public Text m_goalCountText;
    public Text m_fakeManCountText;
    public Text m_scoreCountText;

    public int m_braffleCount;
    public int m_goalCount;
    public int m_fakeManCount;
    public int m_score;
    // Start is called before the first frame update
    void Start()
    {
        m_currentSceneText.text = GameManager.instance.gameType.ToString();
        m_braffleCountText.text = GameManager.instance.m_braffleCount.ToString();
        m_goalCountText.text = GameManager.instance.m_goalCount.ToString();
        m_fakeManCountText.text = GameManager.instance.m_fakeManCount.ToString();
        m_scoreCountText.text = GameManager.instance.score.ToString();
        m_braffleCount = GameManager.instance.m_braffleCount;
        m_goalCount = GameManager.instance.m_goalCount;
        m_fakeManCount = GameManager.instance.m_fakeManCount;
        m_score = GameManager.instance.score;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_timeText != null)
        {
            if(GameManager.instance.gameType == GameType.‘À«Ú)
            {
                m_timeText.text = CrossSceneManager.instance.time.ToString() ;
            }
            else
            {
                m_timeText.text = GetTime(GameManager.instance.gameTime);
            }
            
        }
        if (m_braffleCountText != null&& m_braffleCount!= GameManager.instance.m_braffleCount)
        {
            m_braffleCountText.text = GameManager.instance.m_braffleCount.ToString();
        }
        if (m_goalCountText != null && m_goalCount != GameManager.instance.m_goalCount)
        {
            m_goalCountText.text = GameManager.instance.m_goalCount.ToString();
        }
        if (m_fakeManCountText != null && m_fakeManCount != GameManager.instance.m_fakeManCount)
        {
            m_fakeManCountText.text = GameManager.instance.m_fakeManCount.ToString();
        }
        if (m_scoreCountText != null && m_score != GameManager.instance.score)
        {
            m_scoreCountText.text = GameManager.instance.score.ToString();
        }
    }

    string GetTime(float time)
    {
        float h = Mathf.FloorToInt(time / 3600f);
        float m = Mathf.FloorToInt(time / 60f - h * 60f);
        float s = Mathf.FloorToInt(time - m * 60f - h * 3600f);
        return  h.ToString("00") +":"+ m.ToString("00") + ":" + s.ToString("00");
    }
}
