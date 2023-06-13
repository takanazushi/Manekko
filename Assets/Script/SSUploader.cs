using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public static class SSUploader
{
    // アップロードAPIのレスポンスデータ(必要分のみ定義)
    [Serializable]
    private struct Response
    {
        [Serializable]
        public struct Data
        {
            // アップロードされた画像URL
            public string link;
        }

        public Data data;
        public bool success;
        public int status;
    }

    //アップロード処理
    public static IEnumerator UploadToImgur(

        //Imgurに登録したClient ID
        string clientID,

        //投稿する画像データ
        Texture2D image,

        //アップロードしたURLを受け取るコールバック
        UnityAction<string> onCompleted,

        //エラーメッセージを受け取るコールバック
        UnityAction<string> onError = null
    )
    {
        // Texture2D→バイナリ変換
        var imageBytes = image.EncodeToPNG();
        // バイナリ→Base64変換
        var imageBase64 = Convert.ToBase64String(imageBytes);

        // Form Dataの作成
        var formData = new WWWForm();
        formData.AddField("image", imageBase64);

        // リクエスト作成
        using var request = UnityWebRequest.Post("https://api.imgur.com/3/image", formData);
        request.SetRequestHeader("AUTHORIZATION", "Client-ID " + clientID);

        // リクエスト実行
        yield return request.SendWebRequest();

        // レスポンスチェック
        if (request.result != UnityWebRequest.Result.Success)
        {
            onError?.Invoke(request.error);
            yield break;
        }

        // レスポンスデータ(JSON)をパース
        var response = JsonUtility.FromJson<Response>(request.downloadHandler.text);

        // 成否チェック
        if (!response.success)
        {
            onError?.Invoke($"アップロードエラー (status : ${response.status})");
            yield break;
        }

        // コールバックでリンクを返す
        onCompleted?.Invoke(response.data.link);
    }
}