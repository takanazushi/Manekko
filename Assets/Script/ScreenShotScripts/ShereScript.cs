using System.Collections;
using System.IO;
using UnityEngine;

public class ShereScript : MonoBehaviour
{
    [HeaderAttribute("�ϐ����̏�Ƀ}�E�X�J�[�\���Ő����\��")]
    //�L���v�`���ΏۃJ����
    //Null�̏ꍇ�͑S���
    [SerializeField, HeaderAttribute("�L���v�`���ΏۃJ����"), TooltipAttribute("Null�̏ꍇ�͑S��ʃX�N�V��")]
    private Camera target;

    //Imgur�A�v���P�[�V������ClientID
    //�ŏIbuild����Ƃ��͉B��
    [SerializeField,HeaderAttribute("Imgur��ClientID")]
    private string imgurClientID = "������Client ID�����";

    //�c�C�[�g����
    [SerializeField, HeaderAttribute("�c�C�[�g����ۂ̕���")]
    private string tweetText;

    //�n�b�V���^�O
    [SerializeField, HeaderAttribute("�n�b�V���^�O"), TooltipAttribute("#�͎����œ���̂ŕK�v�Ȃ�")]
    private string[] hashTags;

    public void DateUpLoad()
    {
        
       StartCoroutine(uploadAndTweet());

    }

    private IEnumerator uploadAndTweet()
    {
        // ��ʃL���v�`��
        Texture2D image = null;
        yield return ScreenshotScript.Capture(target, x => image = x);

        // Imgur�ւ̉摜�f�[�^�A�b�v���[�h
        string imageUrl = null;
        string errorMessage = null;

        yield return SSUploader.UploadToImgur(
            imgurClientID,
            image,
            x => imageUrl = x,
            x => errorMessage = x
        );

        // �A�b�v���[�h�̐��ۃ`�F�b�N
        if (!string.IsNullOrEmpty(errorMessage))
        {
            // ���s�̏ꍇ�͏������f
            Debug.LogError(errorMessage);
            yield break;
        }

        // �g���q�����������e�pURL�ɉ��H
        imageUrl = Path.ChangeExtension(imageUrl, null);

        Debug.Log(imageUrl);

        // �c�C�[�g��ʂ��J��
        TwitterShare.Share(
            tweetText,
             imageUrl,
            hashTags
        );
    }
}