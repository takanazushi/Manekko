using System.Collections;
using System.IO;
using UnityEngine;

public class ShereScript : MonoBehaviour
{
    //キャプチャ対象カメラ
    //Nullの場合は全画面
    [SerializeField]
    private Camera target;

    //ImgurアプリケーションのClientID
    //最終buildするときは隠す
    [SerializeField]
    private string imgurClientID = "ここにClient IDを入力";

    //ツイート文言
    [SerializeField]
    private string tweetText;

    //ハッシュタグ
    [SerializeField]
    private string[] hashTags;

    public  void DateUpLoad()
    {
        
       StartCoroutine(UploadAndTweet());

    }

    private IEnumerator UploadAndTweet()
    {
        // 画面キャプチャ
        Texture2D image = null;
        yield return ScreenshotScript.Capture(target, x => image = x);

        // Imgurへの画像データアップロード
        string imageUrl = null;
        string errorMessage = null;

        yield return SSUploader.UploadToImgur(
            imgurClientID,
            image,
            x => imageUrl = x,
            x => errorMessage = x
        );

        // アップロードの成否チェック
        if (!string.IsNullOrEmpty(errorMessage))
        {
            // 失敗の場合は処理中断
            Debug.LogError(errorMessage);
            yield break;
        }

        // 拡張子を除いた投稿用URLに加工
        imageUrl = Path.ChangeExtension(imageUrl, null);

        Debug.Log(imageUrl);

        // ツイート画面を開く
        TwitterShare.Share(
            tweetText,
             imageUrl,
            hashTags
        );
    }
}