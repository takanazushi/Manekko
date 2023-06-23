using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickObject : MonoBehaviour
{
    //GameMainスクリプトの参照を格納する変数
    private GameMain gamemainScript;

    //EndressModeScriptの参照を格納する変数
    private EndressModeScript endressmodeScript;

    //CountDownScriptの参照を格納する変数
    private CountDownScript countdownscript;


    /// <summary>
    /// Scriptの初期化時に呼び出す
    /// シーンによって取得するインスタンスを変更する
    /// </summary>
    private void Awake()
    {
        //もし、現在のシーンがMainGameなら
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            //GameMainスクリプトのインスタンスを検索して参照を取得
            gamemainScript = FindObjectOfType<GameMain>();
        }
        //もし、現在のシーンがEndlessModeなら
        else if(SceneManager.GetActiveScene().name == "EndlessMode")
        {
            //EndlessModeスクリプトのインスタンスを検索して参照を取得
            endressmodeScript = FindObjectOfType<EndressModeScript>();
        }

        // CountDownScriptスクリプトのインスタンスを検索して参照を取得
        countdownscript = FindObjectOfType<CountDownScript>();

        //もし現在のシーンがMainGameで、GameScriptがアタッチされていない場合
        if (gamemainScript == null && SceneManager.GetActiveScene().name == "MainGame") 
        {
            //ログの所にエラー文を表示
            Debug.LogError("GameMainが見つかりませんでした。");
        }

        //もし現在のシーンがEndlessModeで、EndlessModeがアタッチされていない場合
        if(endressmodeScript==null&& SceneManager.GetActiveScene().name == "EndlessMode")
        {
            //ログの所にエラー文を表示
            Debug.LogError("EndlessModeが見つかりませんでした。");
        }

        //CountdownScriptがアタッチされていない場合
        if (countdownscript == null)
        {
            //ログの所にエラー文を表示
            Debug.LogError("CountDownScriptないで");
        }
    }

    /// <summary>
    /// オブジェクトをクリックしたときの処理
    /// 正解不正解を判別する
    /// </summary>
    private void OnMouseDown()
    {
        //カウントダウン中は処理を行わない
        if (countdownscript.isCountDown)
        {
            return;
        }

        //もし現在のゲームシーンが、MainGameの時
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            //もしクリックしたオブジェクトが、見本と同じタグなら
            if (this.gameObject.CompareTag(gamemainScript.TargetObject.tag))
            {

                //ログに正解を出す
                Debug.Log("正解！");

                //正解フラグを立てる
                gamemainScript.IsGameClear = true;

            }
            //間違ったら
            else
            {
                //オブジェクトの色を変更する
                GetComponent<Renderer>().material.color = Color.green;

                //間違った場合の処理を呼び出す
                gamemainScript.WrongAnswer();
            }
        }
        //もし現在のゲームシーンが、EndlessModeの時
        else if (SceneManager.GetActiveScene().name == "EndlessMode")
        {
            //もしクリックしたオブジェクトが、見本と同じタグなら
            if (this.gameObject.CompareTag(endressmodeScript.prefabA.tag))
            {
                //ログに正解を出す
                Debug.Log("正解！");

                //正解フラグを立てる
                endressmodeScript.IsGameClear = true;

            }
            //間違ったら
            else
            {
                //オブジェクトの色を変更する
                GetComponent<Renderer>().material.color = Color.green;

                //間違った場合の処理を呼び出す
                endressmodeScript.WrongAnswer();
            }
        }
    }
}