using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class SSUploader : MonoBehaviour
{
    //アップロードAPIのレスポンスデータ
    private struct Response
    {
        [Serializable]
        public struct Data
        {
            //アップロードされた画像URL
            public string link;
        }

        public Data data;
        public bool success;
        public int status;
    }

    //画像をImgurにアップロードする
    public static UploadToImagur
    (
        //Imugurに登録したClientID
        string clientID,

        //投稿する画像データ
        Texture2D screenShot,

        //アップロードしたURLを受け取るコールバック
        UnityAction<string> onCompleted,

        //エラーメッセージを受け取るコールバック
        UnityAction<string> onError = null

    )
    {
        //画像データをバイナリ変換する
        var imageBytes = screenShot.EncodeToPNG();

        //バイナリデータをBase64に変換する
        var imageBase64 = Convert.ToBase64String(imageBytes);

        //FromDataの作成
        var fromData = new WWWForm();
        fromData.AddField("screenShot", imageBase64);

        //リクエストの作成
        using var request = UnityWebRequest.Post("https://api.imgur.com/3/screenShot\", formData");
    }
}
