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
        //セーブファイルのパスを設定
        string SaveFilePath = Application.persistentDataPath + "/save.bytes";

        //セーブファイルがあるか
        if (File.Exists(SaveFilePath))
        {
            //ファイルモードをオープンにする
            FileStream file = new FileStream(SaveFilePath, FileMode.Open, FileAccess.Read);

            try
            {
                //ファイル読み込み
                byte[] arrRead = File.ReadAllBytes(SaveFilePath);

                //復号化
                //byte[] arrDecrypt = AesDecrypt(arrRead);

                //バイト配列を文字列に変換
                string decryptStr = Encoding.UTF8.GetString(arrRead);

                //JSON形式の文字列をセーブデータのクラスに変換
                //SaveData saveData = JsonUtility.FromJson<SaveData>(decryptStr);
                SaveData saveData = new SaveData();
               
                JsonUtility.FromJsonOverwrite(decryptStr, saveData);


                //データの反映
                ReadData(saveData);
            }
            finally
            {
                //ファイルを閉じる
                if(file!=null)
                {
                    file.Close();
                }
            }
        }
        else
        {
            Debug.Log("セーブファイルがありませんYo！");
        }

        this.enabled = false;
        
    }

    //セーブデータの読み込みと反映
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

    //AesManagedマネージャーを取得
    private AesManaged GetAesManager()
    {
        //半角英数字16文字
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
        //Aesマネージャーを取得
        var aes = GetAesManager();

        //復号化
        byte[] decryptText = aes.CreateDecryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return decryptText;
    }
}
