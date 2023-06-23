using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public static class SSUploader
{
    // �A�b�v���[�hAPI�̃��X�|���X�f�[�^(�K�v���̂ݒ�`)
    [Serializable]
    private struct Response
    {
        [Serializable]
        public struct Data
        {
            // �A�b�v���[�h���ꂽ�摜URL
            public string link;
        }

        public Data data;
        public bool success;
        public int status;
    }

    //�A�b�v���[�h����
    public static IEnumerator UploadToImgur(

        //Imgur�ɓo�^����Client ID
        string clientID,

        //���e����摜�f�[�^
        Texture2D image,

        //�A�b�v���[�h����URL���󂯎��R�[���o�b�N
        UnityAction<string> onCompleted,

        //�G���[���b�Z�[�W���󂯎��R�[���o�b�N
        UnityAction<string> onError = null
    )
    {
        // Texture2D���o�C�i���ϊ�
        var imageBytes = image.EncodeToPNG();
        // �o�C�i����Base64�ϊ�
        var imageBase64 = Convert.ToBase64String(imageBytes);

        // Form Data�̍쐬
        var formData = new WWWForm();
        formData.AddField("image", imageBase64);

        // ���N�G�X�g�쐬
        using var request = UnityWebRequest.Post("https://api.imgur.com/3/image", formData);
        request.SetRequestHeader("AUTHORIZATION", "Client-ID " + clientID);

        // ���N�G�X�g���s
        yield return request.SendWebRequest();

        // ���X�|���X�`�F�b�N
        if (request.result != UnityWebRequest.Result.Success)
        {
            onError?.Invoke(request.error);
            yield break;
        }

        // ���X�|���X�f�[�^(JSON)���p�[�X
        var response = JsonUtility.FromJson<Response>(request.downloadHandler.text);

        // ���ۃ`�F�b�N
        if (!response.success)
        {
            onError?.Invoke($"�A�b�v���[�h�G���[ (status : ${response.status})");
            yield break;
        }

        // �R�[���o�b�N�Ń����N��Ԃ�
        onCompleted?.Invoke(response.data.link);
    }
}