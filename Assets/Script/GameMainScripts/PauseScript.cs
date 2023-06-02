using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField]
    //�|�[�Y���j���[�J���{�^��
    private GameObject itemButton;

    [SerializeField]
    //�Q�[���ĊJ�{�^��
    private GameObject ReStartButton;

    [SerializeField]
    //�Q�[���ĊJ�{�^��
    private GameObject ResetButton;

    [SerializeField]
    //�p�l��
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

    //TODO:GameRestart�́A���[�h�I���i���^�C�g����ʁj�ɖ߂�悤�ɂ���

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
