using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using UnityEngine;

public class Save : MonoBehaviour
{
    private string Iv;
    private string Key;

    public string iv
    {
        get { return Iv; }
        set { Iv = value; }
    }

    public string key
    {
        get { return Key; }
        set { Key = value; }
    }

    struct StocData
    {
        public int[] Level;

        public int[] WrongCount;

        public float[] GameTime;

        public int[]  Score;

        public string[] Title;

    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        DoSave();
    }

    private void DoSave()
    {
        //セーブデータのパスを設定
        string SaveFilePath = Application.persistentDataPath + "/save.bytes";

        //セーブデータの作成
        SaveData saveData = CreateSaveData();

        //セーブデータをJSON形式の文字列に変換
        string jsonString=JsonUtility.ToJson(saveData);

        //文字列をbyte配列に変換
        byte[] bytes=Encoding.UTF8.GetBytes(jsonString);

        //AES暗号化
        //byte[] arrEncypted= AesEncrypt(bytes);

        //指定したパスにファイルを作成
        FileStream file = new FileStream(SaveFilePath, FileMode.Create, FileAccess.Write);

        //ファイルに保存する
        try
        {
            //ファイルに保存
            file.Write(bytes, 0, bytes.Length);

        }
        finally
        {
            //ファイルを閉じる
            if(file != null)
            {
                file.Close();
            }
        }

        this.enabled = false;
    }

    //セーブデータの作成
    private SaveData CreateSaveData()
    {
        SaveData saveData = new SaveData();

        if (PlayerManager.instance != null)
        {
            //セーブデータのインスタンス化
            StocData stoc;
            stoc.Level = new int[4];
            stoc.WrongCount = new int[4];
            stoc.GameTime = new float[4];
            stoc.Score = new int[4];
            stoc.Title = new string[4];

            stoc.Level[0] = PlayerManager.instance.NowLevel;
            stoc.WrongCount[0] = PlayerManager.instance.NowWorongCount;
            stoc.GameTime[0] = PlayerManager.instance.NowGameTime;
            stoc.Score[0] = PlayerManager.instance.NowScore;
            stoc.Title[0] = PlayerManager.instance.NowTitle;

            stoc.Level[1] = PlayerManager.instance.FirstLevel;
            stoc.WrongCount[1] = PlayerManager.instance.FirstWrongCount;
            stoc.GameTime[1] = PlayerManager.instance.FirstGameTime;
            stoc.Score[1] = PlayerManager.instance.FirstScore;
            stoc.Title[1] = PlayerManager.instance.FirstTitle;

            stoc.Level[2] = PlayerManager.instance.SecondLevel;
            stoc.WrongCount[2] = PlayerManager.instance.SecondWrongCount;
            stoc.GameTime[2] = PlayerManager.instance.SecondGameTime;
            stoc.Score[2] = PlayerManager.instance.SecondScore;
            stoc.Title[2] = PlayerManager.instance.SecondTitle;

            stoc.Level[3] = PlayerManager.instance.ThirdLevel;
            stoc.WrongCount[3] = PlayerManager.instance.ThirdWrongCount;
            stoc.GameTime[3] = PlayerManager.instance.ThirdGameTime;
            stoc.Score[3] = PlayerManager.instance.ThirdScore;
            stoc.Title[3] = PlayerManager.instance.ThirdTitle;

            int tmpLevel;
            int tmpWrongCount;
            float tmpGametime;
            int tmpScore;
            string tmpTitle;

            for (int i = 0; i < 4; i++)
            {
                for (int j = i + 1; j < 4; j++)
                {
                    if (stoc.Level[i] < stoc.Level[j])
                    {
                        tmpLevel = stoc.Level[i];
                        stoc.Level[i] = stoc.Level[j];
                        stoc.Level[j] = tmpLevel;

                        tmpWrongCount = stoc.WrongCount[i];
                        stoc.WrongCount[i] = stoc.WrongCount[j];
                        stoc.WrongCount[j] = tmpWrongCount;

                        tmpGametime = stoc.GameTime[i];
                        stoc.GameTime[i] = stoc.GameTime[j];
                        stoc.GameTime[j] = tmpGametime;

                        tmpScore = stoc.Score[i];
                        stoc.Score[i] = stoc.Score[j];
                        stoc.Score[j] = tmpScore;

                        tmpTitle = stoc.Title[i];
                        stoc.Title[i] = stoc.Title[j];
                        stoc.Title[j] = tmpTitle;
                    }
                }
            }

            saveData.FirstLevel = stoc.Level[0];
            saveData.FirstGameTime = stoc.GameTime[0];
            saveData.FirstWrongCount = stoc.WrongCount[0];
            saveData.FirstScore = stoc.Score[0];
            saveData.FirstTitle = stoc.Title[0];

            saveData.SecondLevel = stoc.Level[1];
            saveData.SecondGameTime = stoc.GameTime[1];
            saveData.SecondWrongCount = stoc.WrongCount[1];
            saveData.SecondScore = stoc.Score[1];
            saveData.SecondTitle = stoc.Title[1];

            saveData.ThirdLevel = stoc.Level[2];
            saveData.ThirdGameTime = stoc.GameTime[2];
            saveData.ThirdWrongCount = stoc.WrongCount[2];
            saveData.ThirdScore = stoc.Score[2];
            saveData.ThirdTitle = stoc.Title[2];

            //saveData.FirstLevel = PlayerManager.instance.FirstLevel;
            //saveData.FirstGameTime = PlayerManager.instance.FirstGameTime;
            //saveData.FirstWrongCount = PlayerManager.instance.FirstWrongCount;
            //saveData.FirstTitle = PlayerManager.instance.FirstTitle;

            //saveData.SecondLevel = PlayerManager.instance.SecondLevel;
            //saveData.SecondGameTime = PlayerManager.instance.SecondGameTime;
            //saveData.SecondWrongCount = PlayerManager.instance.SecondWrongCount;
            //saveData.SecondTitle = PlayerManager.instance.SecondTitle;

            //saveData.ThirdLevel = PlayerManager.instance.ThirdLevel;
            //saveData.ThirdGameTime = PlayerManager.instance.ThirdGameTime;
            //saveData.ThirdWrongCount = PlayerManager.instance.ThirdWrongCount;
            //saveData.ThirdTitle = PlayerManager.instance.ThirdTitle;
        }

        return saveData;
    }

    //AesManagedマネージャーを取得
    private AesManaged GetAesManager()
    {
        //任意の半角英数字16文字
        //string aesIv = "1234567890123456";
        //string aeskey = "1234567890123456";

        Iv = "1234567890123456";
        Key = "1234567890123456";

        AesManaged aes = new AesManaged();
        aes.KeySize = 128;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.IV=Encoding.UTF8.GetBytes(Iv);
        aes.Key = Encoding.UTF8.GetBytes(Key);
        aes.Padding = PaddingMode.PKCS7;
        return aes;
    }

    //AES暗号化
    public byte[] AesEncrypt(byte[] byteText)
    {
        //AESマネージャーを取得
        AesManaged aes = GetAesManager();

        //暗号化
        byte[] encryptText = aes.CreateEncryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return encryptText;
    }
}
