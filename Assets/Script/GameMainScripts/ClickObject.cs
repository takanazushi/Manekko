using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickObject : MonoBehaviour
{
    //GameMain�X�N���v�g�̎Q�Ƃ��i�[����ϐ�
    private GameMain gamemainScript;

    //EndressModeScript�̎Q�Ƃ��i�[����ϐ�
    private EndressModeScript endressmodeScript;

    //CountDownScript�̎Q�Ƃ��i�[����ϐ�
    private CountDownScript countdownscript;


    /// <summary>
    /// Script�̏��������ɌĂяo��
    /// �V�[���ɂ���Ď擾����C���X�^���X��ύX����
    /// </summary>
    private void Awake()
    {
        //�����A���݂̃V�[����MainGame�Ȃ�
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            //GameMain�X�N���v�g�̃C���X�^���X���������ĎQ�Ƃ��擾
            gamemainScript = FindObjectOfType<GameMain>();
        }
        //�����A���݂̃V�[����EndlessMode�Ȃ�
        else if(SceneManager.GetActiveScene().name == "EndlessMode")
        {
            //EndlessMode�X�N���v�g�̃C���X�^���X���������ĎQ�Ƃ��擾
            endressmodeScript = FindObjectOfType<EndressModeScript>();
        }

        // CountDownScript�X�N���v�g�̃C���X�^���X���������ĎQ�Ƃ��擾
        countdownscript = FindObjectOfType<CountDownScript>();

        //�������݂̃V�[����MainGame�ŁAGameScript���A�^�b�`����Ă��Ȃ��ꍇ
        if (gamemainScript == null && SceneManager.GetActiveScene().name == "MainGame") 
        {
            //���O�̏��ɃG���[����\��
            Debug.LogError("GameMain��������܂���ł����B");
        }

        //�������݂̃V�[����EndlessMode�ŁAEndlessMode���A�^�b�`����Ă��Ȃ��ꍇ
        if(endressmodeScript==null&& SceneManager.GetActiveScene().name == "EndlessMode")
        {
            //���O�̏��ɃG���[����\��
            Debug.LogError("EndlessMode��������܂���ł����B");
        }

        //CountdownScript���A�^�b�`����Ă��Ȃ��ꍇ
        if (countdownscript == null)
        {
            //���O�̏��ɃG���[����\��
            Debug.LogError("CountDownScript�Ȃ���");
        }
    }

    /// <summary>
    /// �I�u�W�F�N�g���N���b�N�����Ƃ��̏���
    /// ����s�����𔻕ʂ���
    /// </summary>
    private void OnMouseDown()
    {
        //�J�E���g�_�E�����͏������s��Ȃ�
        if (countdownscript.isCountDown)
        {
            return;
        }

        //�������݂̃Q�[���V�[�����AMainGame�̎�
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            //�����N���b�N�����I�u�W�F�N�g���A���{�Ɠ����^�O�Ȃ�
            if (this.gameObject.CompareTag(gamemainScript.TargetObject.tag))
            {

                //���O�ɐ������o��
                Debug.Log("�����I");

                //�����t���O�𗧂Ă�
                gamemainScript.IsGameClear = true;

            }
            //�Ԉ������
            else
            {
                //�I�u�W�F�N�g�̐F��ύX����
                GetComponent<Renderer>().material.color = Color.green;

                //�Ԉ�����ꍇ�̏������Ăяo��
                gamemainScript.WrongAnswer();
            }
        }
        //�������݂̃Q�[���V�[�����AEndlessMode�̎�
        else if (SceneManager.GetActiveScene().name == "EndlessMode")
        {
            //�����N���b�N�����I�u�W�F�N�g���A���{�Ɠ����^�O�Ȃ�
            if (this.gameObject.CompareTag(endressmodeScript.prefabA.tag))
            {
                //���O�ɐ������o��
                Debug.Log("�����I");

                //�����t���O�𗧂Ă�
                endressmodeScript.IsGameClear = true;

            }
            //�Ԉ������
            else
            {
                //�I�u�W�F�N�g�̐F��ύX����
                GetComponent<Renderer>().material.color = Color.green;

                //�Ԉ�����ꍇ�̏������Ăяo��
                endressmodeScript.WrongAnswer();
            }
        }
    }
}