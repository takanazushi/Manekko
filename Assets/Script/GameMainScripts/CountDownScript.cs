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
    //カウントダウン中かどうかフラグ
    private bool IsCountDown = false;

    //プロパティとして公開
    public bool isCountDown
    {
        get { return IsCountDown; }
        set { isCountDown = value; }
    }

    //カウントダウンテキストを参照する変数
    private Text countDownText;

    [SerializeField, HeaderAttribute("カウントダウンパネル"), TooltipAttribute("セットアクティブはFalseにしてください。")]
    private GameObject Panel;

    // Start is called before the first frame update
    void Start()
    {
        //カウントダウンテキストを参照して取得
        countDownText = GameObject.Find("CountDownText").GetComponent<Text>();

        //カウントダウン中フラグをTrueにする
        IsCountDown = true;

        //パネルのセットアクティブをTrueにする
        Panel.SetActive(true);

        //カウントダウンを開始
        StartCoroutine(CountDown());
    }

    private void Update()
    {
        //もしカウントダウンが終了していたら
        if (IsCountDown == false)
        {
            //パネルを非表示にする
            Panel.SetActive(false);
        }
    }


    /// <summary>
    /// カウントダウンのコルーチン
    /// 3秒カウントダウンを行う
    /// </summary>
    public IEnumerator CountDown()
    {
        //1秒待機する
        yield return new WaitForSeconds(1f);

        //3を表示する
        countDownText.text = "3";

        //1秒待機する
        yield return new WaitForSeconds(1f);

        //2を表示する
        countDownText.text = "2";

        //1秒待機する
        yield return new WaitForSeconds(1f);

        //1を表示する
        countDownText.text = "1";

        //1秒待機する
        yield return new WaitForSeconds(1f);

        //Start！を表示する
        countDownText.text = "Start！";

        //カウントダウン中フラグをFalseにする
        IsCountDown = false;

        //0.5秒待機する
        yield return new WaitForSeconds(0.5f);

        //カウントダウンテキストを非表示にする
        countDownText.gameObject.SetActive(false);

    }
}
