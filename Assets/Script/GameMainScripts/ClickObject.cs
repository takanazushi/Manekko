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
            Debug.LogError("GameMainが見つかりませんでした。");
        }

        if(endressmodeScript==null&& SceneManager.GetActiveScene().name == "EndlessMode")
        {
            Debug.LogError("EndlessModeが見つかりませんでした。");
        }

        if (countdownscript == null)
        {
            Debug.LogError("CountDownScriptないで");
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

                Debug.Log("正解！");

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

                Debug.Log("正解！");

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