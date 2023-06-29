using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndressModeScript : GameMain
{

    private int wrongcount;
   
    private void Start()
    {
        //�R���|�[�l���g�擾
        countdownscript = FindObjectOfType<CountDownScript>();

        wrongcount = 0;
        gameTime = 5.0f;

        SetPrefabTarget();
        SetPrefabClickObject();
        SetTime();
        SetLevel();

        
    }

    private void Update()
    {
        if (isGameOver || countdownscript.isCountDown)
        {
            return;
        }

        ReduceTime();
        SetLevel();
        SetWrongPrefab();

        if (isGameClear == true)
        {
            CorrectAnswer();
        }

        if (gameTime <= 0)
        {
            GameOver();
        }
    }

    //������GameMain����p��
    public override void SetPrefabTarget()
    {
        base.SetPrefabTarget();
    }

    //������GameMain����p��
    public override void ClearPrefabTarget()
    {
        base.ClearPrefabTarget();
    }

    //������GameMain����p��
    public override void SetPrefabClickObject()
    {
        base.SetPrefabClickObject();
    }

    private void SetTime()
    {
        timeText.text = "Time: " + gameTime.ToString("F0");
    }

    private void SetLevel()
    {
        levelText.text = "Level: " + PlayerManager.instance.EndlessMode_NowLevel.ToString();
    }

    private void ReduceTime()
    {
        gameTime -= Time.deltaTime;
        timeText.text = "Time: " + gameTime.ToString("F0");
    }

    private void SetWrongPrefab()
    {
        
        switch (wrongcount)
        {
            case 1:
                wrongPrehubs[0].color = Color.gray;
                break;
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("EndlessMonde_ScoreScene");
        Debug.Log("�Q�[���I�[�o�[");
        isGameOver = true;
    }

    //������GameMain����p��
    //�������㏑�����Ă���
    public override void CorrectAnswer()
    {
        PlayerManager.instance.EndlessMode_NowLevel++;
        gameTime = 5.0f;
        level++;
        ClearPrefabTarget();
        SetPrefabTarget();
        SetPrefabClickObject();
        isGameClear = false;

    }

    //������GameMain����p��
    //�������㏑�����Ă���
    public override void WrongAnswer()
    {
        wrongcount++;

        CheckGameOver();
    }

    private void CheckGameOver()
    {
        if (gameTime <= 0f || wrongcount >= 1)
        {
            GameOver();
        }
    }


}
