using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public static class ScreenshotScript
{
    //��ʃL���v�`������
    public static IEnumerator Capture
    (
        //�J����
        Camera target,

        //�X�N�V��
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

    // UI���܂ޑS��ʃL���v�`��
    private static IEnumerator CaptureAll(UnityAction<Texture2D> CaptureTexture)
    {
        // �����_�����O�I���܂őҋ@
        yield return new WaitForEndOfFrame();

        // ��ʑS�̂̃X�N���[���V���b�g�擾
        var screenShot = ScreenCapture.CaptureScreenshotAsTexture();

        // �R�[���o�b�N�Ō��ʂ�Ԃ�
        CaptureTexture?.Invoke(screenShot);
    }

    // UI���܂܂Ȃ�����J�����L���v�`��
    private static IEnumerator CaptureCamera(Camera target, UnityAction<Texture2D> CaptureTexture)
    {
        // RenderTexture���쐬
        var rt = new RenderTexture(target.pixelWidth, target.pixelHeight, 24);

        // �J������RenderTexture���ꎞ�I�ɕύX���ăL���v�`��
        var prev = target.targetTexture;
        target.targetTexture = rt;
        target.Render();
        target.targetTexture = prev;

        // �s�N�Z���f�[�^�擾�p�̃e�N�X�`���쐬
        var screenShot = new Texture2D(
            target.pixelWidth,
            target.pixelHeight,
            TextureFormat.RGB24,
            false
        );

        // �s�N�Z���]��
        prev = RenderTexture.active;
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);
        screenShot.Apply();
        RenderTexture.active = prev;

        // �R�[���o�b�N�Ō��ʂ�Ԃ�
        CaptureTexture?.Invoke(screenShot);

        yield break;
    }
}