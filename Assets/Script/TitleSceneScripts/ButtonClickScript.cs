using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickScript : MonoBehaviour
{
    //�Q�[���J�n�{�^��������������s����
    public void GameStart()
    {
        SceneManager.LoadScene("MainGame");
    }

    //�Q�[���I���{�^��������������s����
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
