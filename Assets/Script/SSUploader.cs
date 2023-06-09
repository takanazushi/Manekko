using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class SSUploader : MonoBehaviour
{
    //�A�b�v���[�hAPI�̃��X�|���X�f�[�^
    private struct Response
    {
        [Serializable]
        public struct Data
        {
            //�A�b�v���[�h���ꂽ�摜URL
            public string link;
        }

        public Data data;
        public bool success;
        public int status;
    }

    //�摜��Imgur�ɃA�b�v���[�h����
    public static UploadToImagur
    (
        //Imugur�ɓo�^����ClientID
        string clientID,

        //���e����摜�f�[�^
        Texture2D screenShot,

        //�A�b�v���[�h����URL���󂯎��R�[���o�b�N
        UnityAction<string> onCompleted,

        //�G���[���b�Z�[�W���󂯎��R�[���o�b�N
        UnityAction<string> onError = null

    )
    {
        //�摜�f�[�^���o�C�i���ϊ�����
        var imageBytes = screenShot.EncodeToPNG();

        //�o�C�i���f�[�^��Base64�ɕϊ�����
        var imageBase64 = Convert.ToBase64String(imageBytes);

        //FromData�̍쐬
        var fromData = new WWWForm();
        fromData.AddField("screenShot", imageBase64);

        //���N�G�X�g�̍쐬
        using var request = UnityWebRequest.Post("https://api.imgur.com/3/screenShot\", formData");
    }
}
