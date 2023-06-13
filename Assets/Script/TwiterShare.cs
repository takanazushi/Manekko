using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public static class TwitterShare
{
  
    //Share����
    //text�̓c�C�[�g����
    //url��Imgur��URL
    //tag�̓n�b�V���^�O
    public static void Share(string text, string url,IEnumerable<string> tags = null)
    {
        // �c�C�[�g�pURL�쐬
        var tweetURL = "http://twitter.com/intent/tweet?text=" + Uri.EscapeUriString(text + "\n" + url + "\n");

        if (tags != null)
        {
            // �n�b�V���^�O������΃p�����[�^�ɒǉ�
            var strTag = string.Join(",", tags);

            if (!string.IsNullOrEmpty(strTag))
                tweetURL += "&hashtags=" + strTag+ "\n";
        }

        // �c�C�[�g��ʂ�V�����E�B���h�E�ŊJ��
        OpenWindow(tweetURL, 600, 300);
    }

    // �V�����E�B���h�E��URL���J��
#if !UNITY_EDITOR && UNITY_WEBGL
    // WebGL�r���h�ŗL���ɂȂ�
    [DllImport("__Internal")]
    private static extern void OpenWindow(string url, int width, int height);
#else
    // Unity�G�f�B�^��WebGL�ȊO�̃v���b�g�t�H�[���ŗL���ɂȂ�
    private static void OpenWindow(string url, int width, int height) => Application.OpenURL(url);
#endif
}