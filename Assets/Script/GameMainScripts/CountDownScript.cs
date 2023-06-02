using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountDownScript : MonoBehaviour
{
    private bool IsCountDown = false;

    public bool isCountDown
    {
        get { return IsCountDown; }
        set { isCountDown = value; }
    }

    private Text countDownText;

    [SerializeField]
    //ÉpÉlÉã
    private GameObject Panel;

    // Start is called before the first frame update
    void Start()
    {
        countDownText = GameObject.Find("CountDownText").GetComponent<Text>();
        IsCountDown = true;
        Panel.SetActive(true);
        StartCoroutine(CountDown());
    }

    private void Update()
    {
        if (IsCountDown == false)
        {
            Panel.SetActive(false);
        }
    }

    public IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1f);
        countDownText.text = "3";
        yield return new WaitForSeconds(1f);
        countDownText.text = "2";
        yield return new WaitForSeconds(1f);
        countDownText.text = "1";
        yield return new WaitForSeconds(1f);
        countDownText.text = "StartÅI";

        IsCountDown = false;
        yield return new WaitForSeconds(0.5f);
        countDownText.gameObject.SetActive(false);

    }
}
