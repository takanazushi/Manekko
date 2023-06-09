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
        //�S�Ă̕`�悪�I���܂őҋ@
        yield return new WaitForEndOfFrame();

        //��ʑS�̂̃X�N�V���擾
        var capture = ScreenCapture.CaptureScreenshotAsTexture();

        //�R�[���o�b�N�Ō��ʂ�Ԃ�
        ScreenTexture?.Invoke(capture);
    }
}
