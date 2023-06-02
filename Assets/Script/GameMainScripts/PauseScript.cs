using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField]
    //ポーズメニュー開くボタン
    private GameObject itemButton;

    [SerializeField]
    //ゲーム再開ボタン
    private GameObject ReStartButton;

    [SerializeField]
    //ゲーム再開ボタン
    private GameObject ResetButton;

    [SerializeField]
    //パネル
    private GameObject itemPanel;

    // Start is called before the first frame update
    void Start()
    {
        itemPanel.SetActive(false);
        ReStartButton.SetActive(false);
        ResetButton?.SetActive(false);
        itemButton.SetActive(true);
        Time.timeScale = 1f;
    }

    //TODO:GameRestartは、モード選択（かタイトル画面）に戻るようにする

    public void GameStop()
    {
        Time.timeScale = 0f;
        itemButton.SetActive(false);
        ReStartButton.SetActive(true);
        ResetButton.SetActive(true);
        itemPanel.SetActive(true);
    }

    public void GameRestart()
    {
        itemPanel.SetActive(false);
        ReStartButton.SetActive(false);
        ResetButton.SetActive(false);
        itemButton.SetActive(true);
        Time.timeScale = 1f;
    }

    public void GameReset()
    {
        SceneManager.LoadScene("MainGame");
    }
}
