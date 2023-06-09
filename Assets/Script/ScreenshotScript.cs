using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenshotScript : MonoBehaviour
{
    public static IEnumerator Capture
    (
        UnityAction<Texture2D> ScreenTexture
    )
    {
        yield return CaptureFullScreen(ScreenTexture);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static IEnumerable CaptureFullScreen(UnityAction<Texture2D> ScreenTexture)
    {
        //全ての描画が終わりまで待機
        yield return new WaitForEndOfFrame();

        //画面全体のスクショ取得
        var capture = ScreenCapture.CaptureScreenshotAsTexture();

        //コールバックで結果を返す
        ScreenTexture?.Invoke(capture);
    }
}
