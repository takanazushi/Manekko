using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Read : MonoBehaviour
{
    private void OnEnable()
    {
        DoRead();
    }

    private void DoRead()
    {
        //�Z�[�u�t�@�C���̃p�X��ݒ�
        string SaveFilePath = Application.persistentDataPath + "/save.bytes";

        //�Z�[�u�t�@�C�������邩
        if (File.Exists(SaveFilePath))
        {
            //�t�@�C�����[�h���I�[�v���ɂ���
            FileStream file = new FileStream(SaveFilePath, FileMode.Open, FileAccess.Read);

            try
            {
                //�t�@�C���ǂݍ���
                byte[] arrRead = File.ReadAllBytes(SaveFilePath);

                //������
                //byte[] arrDecrypt = AesDecrypt(arrRead);

                //�o�C�g�z��𕶎���ɕϊ�
                string decryptStr = Encoding.UTF8.GetString(arrRead);

                //JSON�`���̕�������Z�[�u�f�[�^�̃N���X�ɕϊ�
                //SaveData saveData = JsonUtility.FromJson<SaveData>(decryptStr);
                SaveData saveData = new SaveData();
               
                JsonUtility.FromJsonOverwrite(decryptStr, saveData);


                //�f�[�^�̔��f
                ReadData(saveData);
            }
            finally
            {
                //�t�@�C�������
                if(file!=null)
                {
                    file.Close();
                }
            }
        }
        else
        {
            Debug.Log("�Z�[�u�t�@�C��������܂���Yo�I");
        }

        this.enabled = false;
        
    }

    //�Z�[�u�f�[�^�̓ǂݍ��݂Ɣ��f
    private void ReadData(SaveData saveData)
    {
        if (PlayerManager.instance != null)
        {
            PlayerManager.instance.FirstLevel = saveData.FirstLevel;
            PlayerManager.instance.FirstGameTime = saveData.FirstGameTime;
            PlayerManager.instance.FirstWrongCount = saveData.FirstWrongCount;
            PlayerManager.instance.FirstScore = saveData.FirstScore;
            PlayerManager.instance.FirstTitle = saveData.FirstTitle;

            PlayerManager.instance.SecondGameTime = saveData.SecondGameTime;
            PlayerManager.instance.SecondLevel = saveData.SecondLevel;
            PlayerManager.instance.SecondWrongCount = saveData.SecondWrongCount;
            PlayerManager.instance.SecondScore = saveData.SecondScore;
            PlayerManager.instance.SecondTitle = saveData.SecondTitle;

            PlayerManager.instance.ThirdGameTime = saveData.ThirdGameTime;
            PlayerManager.instance.ThirdLevel = saveData.ThirdLevel;
            PlayerManager.instance.ThirdWrongCount = saveData.ThirdWrongCount;
            PlayerManager.instance.ThirdScore = saveData.ThirdScore;
            PlayerManager.instance.ThirdTitle = saveData.ThirdTitle;
        }

    }

    //AesManaged�}�l�[�W���[���擾
    private AesManaged GetAesManager()
    {
        //���p�p����16����
        //string aesIv = "1234567890123456";

        //string aesKey = "1234567890123456";

        Save save=GetComponent<Save>();

        AesManaged aes = new AesManaged();
        aes.KeySize = 128;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.IV =Encoding.UTF8.GetBytes(save.iv);
        aes.Key = Encoding.UTF8.GetBytes(save.key);
        aes.Padding=PaddingMode.PKCS7;
        return aes;
    }

    public byte[] AesDecrypt(byte[] byteText)
    {
        //Aes�}�l�[�W���[���擾
        var aes = GetAesManager();

        //������
        byte[] decryptText = aes.CreateDecryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return decryptText;
    }
}
