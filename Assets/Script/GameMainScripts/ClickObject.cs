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
            Debug.LogError("GameMainÇ™å©Ç¬Ç©ÇËÇ‹ÇπÇÒÇ≈ÇµÇΩÅB");
        }

        if (countdownscript == null)
        {
            Debug.LogError("CountDownScriptÇ»Ç¢Ç≈");
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
            
            Debug.Log("ê≥âÅI");

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