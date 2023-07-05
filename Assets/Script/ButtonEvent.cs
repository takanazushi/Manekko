using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    public Text textPrefab;

    public void OnPointEnter_MainGame()
    {
        textPrefab.enabled = true;

        //テキストの内容を設定する
        textPrefab.text = "　レベル100を目指して動物を見つけよう！　";
    }

    public void OnPointEnter_EndlessMode()
    {
        textPrefab.enabled = true;

        //テキストの内容を設定する
        textPrefab.text = "ノーミスでどこまでいけるかな？　　　　　　";
    }

    public void OnPointEnter_Ranking()
    {
        textPrefab.enabled = true;

        //テキストの内容を設定する
        textPrefab.text = "今までの成績が見れるよ！　　　　　　　　　";
    }


    public void OnPointerExit()
    {
        textPrefab.enabled = false;

        textPrefab.text="";
    }

    
}
