using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    [SerializeField]
    private Text LevelText;

    [SerializeField]
    private Text TimeText;

    [SerializeField]
    private Text MissText;

    [SerializeField]
    private Text ScoreText;

    [SerializeField]
    private Text TitleText;

    //�N���X�̎Q�Ƃ�����
    public Save saveClass;

    private int MaxScore = 400;

    // Start is called before the first frame update
    void Start()
    {
       

        if (PlayerManager.instance != null)
        {
            ScoreCalculation();
            TitleDecision();
            DataSave();

            LevelText.text = "Level" + PlayerManager.instance.NowLevel;
            TimeText.text = "Time" + PlayerManager.instance.NowGameTime.ToString("F0");
            MissText.text = "Miss" + PlayerManager.instance.NowWorongCount;
            TitleText.text = "Title" + PlayerManager.instance.NowTitle;
            ScoreText.text = "Score" + PlayerManager.instance.NowScore;


        }
        else
        {
            Debug.Log("�v���C���[Manager�Ȃ�");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void DataSave()
    {
        saveClass.enabled = true;

        Debug.Log("�Z�[�u���ł��܂���YO�`");
    }

    private void ScoreCalculation()
    {
        //int level = PlayerManager.instance.NowLevel;
        //int time = (int)PlayerManager.instance.NowLevel;
        double Score;

        Score = PlayerManager.instance.NowLevel + (int)PlayerManager.instance.NowGameTime;
        Score = Score / MaxScore;
        Score = Score * 100;

        PlayerManager.instance.NowScore = (int)Score;

        Debug.Log(PlayerManager.instance.NowLevel);
        Debug.Log(PlayerManager.instance.NowGameTime);
        Debug.Log((int)Score);
    }

    private void TitleDecision()
    {
        int score = PlayerManager.instance.NowScore;

        if (score >= 0 && score <= 5)
        {
            PlayerManager.instance.NowTitle = "���������撣��܏�";
        }
        else if (score >= 6 && score <= 10)
        {
            PlayerManager.instance.NowTitle = "���������ŏ�";
        }
        else if (score >= 11 && score <= 20)
        {
            PlayerManager.instance.NowTitle = "����Ȃ�ŏ�";
        }
        else if (score >= 21 && score <= 30)
        {
            PlayerManager.instance.NowTitle = "�L�т���ŏ�";
        }
        else if (score >= 31 && score <= 40)
        {
            PlayerManager.instance.NowTitle = "�܂���͒����ŏ�";
        }
        else if (score >= 41 && score <= 49)
        {
            PlayerManager.instance.NowTitle = "����2���ŏ�";
        }
        else if (score == 50) 
        {
            PlayerManager.instance.NowTitle = "�܂�Ԃ��n�_�ŏ�";
        }
        else if (score >= 51 && score <= 60)
        {
            PlayerManager.instance.NowTitle = "�C��������܏�";
        }
        else if (score >= 61 && score <= 70)
        {
            PlayerManager.instance.NowTitle = "�y��_�ŏ�";
        }
        else if (score >= 71 && score <= 80)
        {
            PlayerManager.instance.NowTitle = "�悭�撣�����ŏ�";
        }
        else if (score >= 81 && score <= 90)
        {
            PlayerManager.instance.NowTitle = "���Ə����ŏ�";
        }
        else if (score >= 91 && score <= 99)
        {
            PlayerManager.instance.NowTitle = "�߂�����ɂ����ŏ�";
        }
        else if (score >= 100)
        {
            PlayerManager.instance.NowTitle = "���Ȃ��͓V�˂ŏ�";
        }
    }



}
