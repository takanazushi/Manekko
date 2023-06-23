using System.Collections;
using System.IO;
using UnityEngine;

public class ShereScript : MonoBehaviour
{
    [HeaderAttribute("変数名の上にマウスカーソルで説明表示")]
    //キャプチャ対象カメラ
    //Nullの場合は全画面
    [SerializeField, HeaderAttribute("キャプチャ対象カメラ"), TooltipAttribute("Nullの場合は全画面スクショ")]
    private Camera target;

    //ImgurアプリケーションのClientID
    //最終buildするときは隠す
    [SerializeField,HeaderAttribute("ImgurのClientID")]
    private string imgurClientID = "ここにClient IDを入力";

    //ツイート文言
    [SerializeField, HeaderAttribute("ツイートする際の文章")]
    private string tweetText;

    //ハッシュタグ
    [SerializeField, HeaderAttribute("ハッシュタグ"), TooltipAttribute("#は自動で入るので必要ない")]
    private string[] hashTags;

    public void DateUpLoad()
    {
        
       StartCoroutine(uploadAndTweet());

    }

    private IEnumerator uploadAndTweet()
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