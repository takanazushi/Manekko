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

        PlayerManager.instance.EndlessMode_NowLevel = 1;
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
        timeText.text = gameTime.ToString("F0");
    }

    private void SetLevel()
    {
        levelText.text = PlayerManager.instance.EndlessMode_NowLevel.ToString();
    }

    private void ReduceTime()
    {
        gameTime -= Time.deltaTime;
        timeText.text = gameTime.ToString("F0");
    }

    private void SetWrongPrefab()
    {
        
        switch (wrongcount)
        {
            case 1:
                wrongEndPrehubs[0].enabled = true;
                wrongEndPrehubs[0].color = Color.gray;
                wrongPrehubs[0].enabled = false;
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
