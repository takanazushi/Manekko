using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonScript : MonoBehaviour
{
    //�^�C�g���ɖ߂�{�^��������������s����
    public void ClickBackButton()
    {
        Debug.Log("�^�C�g���ɖ߂�");
        SceneManager.LoadScene("TitleScene");
    }
}
