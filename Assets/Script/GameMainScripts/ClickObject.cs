using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickObject : MonoBehaviour
{
    private GameMain gamemainScript;
    private EndressModeScript endressmodeScript;
    private CountDownScript countdownscript; 


    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            gamemainScript = FindObjectOfType<GameMain>();
        }
        else if(SceneManager.GetActiveScene().name == "EndlessMode")
        {
            endressmodeScript = FindObjectOfType<EndressModeScript>();
        }
            
        countdownscript = FindObjectOfType<CountDownScript>();

        if (gamemainScript == null && SceneManager.GetActiveScene().name == "MainGame") 
        {
            Debug.LogError("GameMain��������܂���ł����B");
        }

        if(endressmodeScript==null&& SceneManager.GetActiveScene().name == "EndlessMode")
        {
            Debug.LogError("EndlessMode��������܂���ł����B");
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

        if (SceneManager.GetActiveScene().name == "MainGame")
        {
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
        else if (SceneManager.GetActiveScene().name == "EndlessMode")
        {
            if (this.gameObject.CompareTag(endressmodeScript.prefabA.tag))
            {

                Debug.Log("�����I");

                endressmodeScript.IsGameClear = true;
                //newBehaviourScript.CorrectAnswer();
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.green;
                endressmodeScript.WrongAnswer();
            }
        }
    }
}