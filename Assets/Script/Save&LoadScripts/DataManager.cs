using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //�N���X�̎Q�Ƃ�����
    public Save saveClass;
    public Read readClass;

    // Start is called before the first frame update
    void Start()
    {

        if (PlayerManager.instance != null)
        {
            Debug.Log("����");

            Debug.Log("1�ʂ̃��x��" + PlayerManager.instance.FirstLevel);
            Debug.Log("1�ʂ̎���" + PlayerManager.instance.FirstGameTime);
            Debug.Log("1�ʂ̊Ԉ������" + PlayerManager.instance.FirstWrongCount);
            Debug.Log("1�ʂ̏̍�" + PlayerManager.instance.FirstTitle);

            DataSave();

            Debug.Log("1�ʂ̃��x��" + PlayerManager.instance.FirstLevel);
            Debug.Log("1�ʂ̎���" + PlayerManager.instance.FirstGameTime);
            Debug.Log("1�ʂ̊Ԉ������" + PlayerManager.instance.FirstWrongCount);
            Debug.Log("1�ʂ̏̍�" + PlayerManager.instance.FirstTitle);

            DataRead();

            Debug.Log("1�ʂ̃��x��" + PlayerManager.instance.FirstLevel);
            Debug.Log("1�ʂ̎���" + PlayerManager.instance.FirstGameTime);
            Debug.Log("1�ʂ̊Ԉ������" + PlayerManager.instance.FirstWrongCount);
            Debug.Log("1�ʂ̏̍�" + PlayerManager.instance.FirstTitle);
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
