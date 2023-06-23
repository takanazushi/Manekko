using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessMode_RankingScript : MonoBehaviour
{
    [SerializeField]
    public Text EndlessMode_FirstLevel;
    [SerializeField]
    public Text EndlessMode_FirstScore;

    [SerializeField]
    public Text EndlessMode_SecondLevel;
    [SerializeField]
    public Text EndlessMode_SecondScore;

    [SerializeField]
    public Text EndlessMode_ThirdLevel;
    [SerializeField]
    public Text EndlessMode_ThirdScore;

    public Read readClass;
    public Save saveClass;

    // Start is called before the first frame update
    void Start()
    {


        if (PlayerManager.instance != null)
        {
            //�v���C���[�}�l�[�W���[�̏�����
            PlayerManager.instance.EndlessMode_NowLevel = 0;
            PlayerManager.instance.EndlessMode_NowScore = 0;

            //�ăZ�[�u
            DataSave();

            //���[�h
            DataRead();

            EndlessMode_FirstLevel.text = PlayerManager.instance.EndlessFirstLevel.ToString();
            EndlessMode_FirstScore.text = PlayerManager.instance.EndlessFirstScore.ToString();


            EndlessMode_SecondLevel.text = PlayerManager.instance.EndlessSecondLevel.ToString();
            EndlessMode_SecondScore.text = PlayerManager.instance.EndlessSecondScore.ToString();


            EndlessMode_ThirdLevel.text = PlayerManager.instance.EndlessThirdLevel.ToString();
            EndlessMode_ThirdScore.text = PlayerManager.instance.EndlessThirdScore.ToString();

        }
        else
        {
            Debug.Log("�v���C���[Manager�Ȃ�");
            Destroy(this);
        }
    }


    private void DataRead()
    {
        //�f�[�^�̓ǂݍ���
        readClass.enabled = true;

        Debug.Log("�ǂݍ��݂��I���܂���YO�`");
    }

    private void DataSave()
    {
        saveClass.enabled = true;

        Debug.Log("�Z�[�u���ł��܂���YO�`");
    }
}
