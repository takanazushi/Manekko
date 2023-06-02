using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonScript : MonoBehaviour
{
    //タイトルに戻るボタンを押したら実行する
    public void ClickBackButton()
    {
        Debug.Log("タイトルに戻る");
        SceneManager.LoadScene("TitleScene");
    }
}
