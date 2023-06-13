using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public static class ScreenshotScript
{
    //画面キャプチャ処理
    public static IEnumerator Capture
    (
        //カメラ
        Camera target,

        //スクショ
        UnityAction<Texture2D> CaptureTexture
    )
    {
        if (target == null)
        {
            yield return CaptureAll(CaptureTexture);
        }
        else
        {
            yield return CaptureCamera(target, CaptureTexture);
        }
    }

    // UIを含む全画面キャプチャ
    private static IEnumerator CaptureAll(UnityAction<Texture2D> CaptureTexture)
    {
        // レンダリング終了まで待機
        yield return new WaitForEndOfFrame();

        // 画面全体のスクリーンショット取得
        var screenShot = ScreenCapture.CaptureScreenshotAsTexture();

        // コールバックで結果を返す
        CaptureTexture?.Invoke(screenShot);
    }

    // UIを含まない特定カメラキャプチャ
    private static IEnumerator CaptureCamera(Camera target, UnityAction<Texture2D> CaptureTexture)
    {
        // RenderTextureを作成
        var rt = new RenderTexture(target.pixelWidth, target.pixelHeight, 24);

        // カメラのRenderTextureを一時的に変更してキャプチャ
        var prev = target.targetTexture;
        target.targetTexture = rt;
        target.Render();
        target.targetTexture = prev;

        // ピクセルデータ取得用のテクスチャ作成
        var screenShot = new Texture2D(
            target.pixelWidth,
            target.pixelHeight,
            TextureFormat.RGB24,
            false
        );

        // ピクセル転送
        prev = RenderTexture.active;
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);
        screenShot.Apply();
        RenderTexture.active = prev;

        // コールバックで結果を返す
        CaptureTexture?.Invoke(screenShot);

        yield break;
    }
}