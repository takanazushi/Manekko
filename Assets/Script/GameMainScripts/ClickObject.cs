using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour
{
    private GameMain gamemainScript;
    private CountDownScript countdownscript; 


    private void Awake()
    {
        gamemainScript = FindObjectOfType<GameMain>();
        countdownscript = FindObjectOfType<CountDownScript>();

        if (gamemainScript == null)
        {
            Debug.LogError("GameMain��������܂���ł����B");
        }

        if (countdownscript == null)
        {
            Debug.LogError("CountDownScript�Ȃ���");
        }
    }

    private void OnMouseDown()
    {
        if (countdownscript.isCountDown)
        {
            return;
        }

        if (this.gameObject.CompareTag(gamemainScript.prefabA.tag))
        {
            
            Debug.Log("�����I");

            gamemainScript.IsGameClear = true;
            //newBehaviourScript.CorrectAnswer();
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.green;
            gamemainScript.WrongAnswer();
        }
    }
}