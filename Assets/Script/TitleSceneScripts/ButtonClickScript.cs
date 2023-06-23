using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickScript : MonoBehaviour
{
    //ゲーム開始ボタンを押したら実行する
    public void GameStart()
    {
        SceneManager.LoadScene("MainGame");
    }

    //ゲーム終了ボタンを押したら実行する
    public void GameEnd()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;

        #else
            Application.Quit();

        #endif
    }

    public void ModeSelectLoad()
    {
        SceneManager.LoadScene("ModeSelect");
    }

    public void EndlessModeLoad()
    {
        SceneManager.LoadScene("EndlessMode");
    }

    public void RankingLoad()
    {
        SceneManager.LoadScene("ScoreRanking");
    }

    public void EndlessMondeRankingLoad()
    {
        SceneManager.LoadScene("EndlessMode_ScoreRanking");
    }

   

}
