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
    //�J�E���g�_�E�������ǂ����t���O
    private bool IsCountDown = false;

    //�v���p�e�B�Ƃ��Č��J
    public bool isCountDown
    {
        get { return IsCountDown; }
        set { isCountDown = value; }
    }

    //�J�E���g�_�E���e�L�X�g���Q�Ƃ���ϐ�
    private Text countDownText;

    [SerializeField, HeaderAttribute("�J�E���g�_�E���p�l��"), TooltipAttribute("�Z�b�g�A�N�e�B�u��False�ɂ��Ă��������B")]
    private GameObject Panel;

    // Start is called before the first frame update
    void Start()
    {
        //�J�E���g�_�E���e�L�X�g���Q�Ƃ��Ď擾
        countDownText = GameObject.Find("CountDownText").GetComponent<Text>();

        //�J�E���g�_�E�����t���O��True�ɂ���
        IsCountDown = true;

        //�p�l���̃Z�b�g�A�N�e�B�u��True�ɂ���
        Panel.SetActive(true);

        //�J�E���g�_�E�����J�n
        StartCoroutine(CountDown());
    }

    private void Update()
    {
        //�����J�E���g�_�E�����I�����Ă�����
        if (IsCountDown == false)
        {
            //�p�l�����\���ɂ���
            Panel.SetActive(false);
        }
    }


    /// <summary>
    /// �J�E���g�_�E���̃R���[�`��
    /// 3�b�J�E���g�_�E�����s��
    /// </summary>
    public IEnumerator CountDown()
    {
        //1�b�ҋ@����
        yield return new WaitForSeconds(1f);

        //3��\������
        countDownText.text = "3";

        //1�b�ҋ@����
        yield return new WaitForSeconds(1f);

        //2��\������
        countDownText.text = "2";

        //1�b�ҋ@����
        yield return new WaitForSeconds(1f);

        //1��\������
        countDownText.text = "1";

        //1�b�ҋ@����
        yield return new WaitForSeconds(1f);

        //Start�I��\������
        countDownText.text = "Start�I";

        //�J�E���g�_�E�����t���O��False�ɂ���
        IsCountDown = false;

        //0.5�b�ҋ@����
        yield return new WaitForSeconds(0.5f);

        //�J�E���g�_�E���e�L�X�g���\���ɂ���
        countDownText.gameObject.SetActive(false);

    }
}
