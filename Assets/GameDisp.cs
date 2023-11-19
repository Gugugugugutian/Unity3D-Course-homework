using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GameDispCore : MonoBehaviour
{
    public GUIStyle textStyle, countdownStyle; // 文字样式
    public float countdown = 60.0f; // 倒计时秒数

    /* Signal of countdown:
     * 0:   Stop
     * 1:   Normal
     */
    public int signal = 1; 

    private Rect countdownRect; // 倒计时文本位置
    private string text1, text2, text3;     // 3 lines of texts

    private void Start()
    {
        countdownRect = new Rect(Screen.width - 100, 10, 100, 30); // 右上角显示倒计时
        text1 = "Welcome to the game.";
        text2 = "Press R to start.";
        text3 = "Press Q to quit.";
    }

    public void RefreshTextlines()
    {
        // 显示3行文字
        GUI.Label(new Rect(10, 10, 200, 30), text1, textStyle);
        GUI.Label(new Rect(10, 40, 200, 30), text2, textStyle);
        GUI.Label(new Rect(10, 70, 200, 30), text3, textStyle);
    }

    public void ChangeLine(string new_text, int line)
    {
        switch (line)
        {
            case (1): 
                text1 = new_text; break;
            case (2): 
                text2 = new_text; break;
            case (3):
                text3 = new_text; break;
            default: 
                break;
        }
        //RefreshTextlines();
    }

    private void OnGUI()
    {
        RefreshTextlines(); 

        // 显示倒计时
        GUI.Label(countdownRect, Mathf.Round(countdown).ToString(), countdownStyle);
    }

    private void Update()
    {
        // 更新倒计时
        if (signal == 1)
        {
            countdown -= Time.deltaTime;
        }
        if (countdown < 0)
        {
            countdown = 0;
        }
    }
}
