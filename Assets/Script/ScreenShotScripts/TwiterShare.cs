using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public static class TwitterShare
{


    /// <summary>
    /// ツイッターにShereする際処理
    /// </summary>

    ///<param name="text">
    ///ツイート文章
    ///</param>

    ///<param name="url">
    ///ImgurのURL
    ///</param>

    ///<param name="tags">
    ///ハッシュタグ
    ///</param>
    public static void Share(string text, string url,IEnumerable<string> tags = null)
    {
        // ツイート用URL作成
        var tweetURL = "http://twitter.com/intent/tweet?text=" + Uri.EscapeUriString(text + "\n" + url + "\n");

        if (tags != null)
        {
            // ハッシュタグがあればパラメータに追加
            var strTag = string.Join(",", tags);

            //URLに
            if (!string.IsNullOrEmpty(strTag))
            {
                tweetURL += "&hashtags=" + strTag + "\n";
            }
                
        }

        // ツイート画面を新しいウィンドウで開く
        OpenWindow(tweetURL, 600, 300);
    }

    // 新しいウィンドウでURLを開く
#if !UNITY_EDITOR && UNITY_WEBGL
    // WebGLビルドで有効になる
    [DllImport("__Internal")]
    private static extern void OpenWindow(string url, int width, int height);
#else
    // UnityエディタやWebGL以外のプラットフォームで有効になる
    private static void OpenWindow(string url, int width, int height) => Application.OpenURL(url);
#endif
}