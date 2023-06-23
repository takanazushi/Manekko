using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class AspectKeep : MonoBehaviour
{
    [SerializeField,HeaderAttribute("�ΏۂƂ���J����")]
    private Camera targetCamera; 

    [SerializeField,HeaderAttribute("�ړI�𑜓x")]
    private Vector2 aspectVec; 

    

    void Update()
    {
        //��ʂ̃A�X�y�N�g��
        var screenAspect = Screen.width / (float)Screen.height;

        //�ړI�̃A�X�y�N�g��
        var targetAspect = aspectVec.x / aspectVec.y;

        //�ړI�̃A�X�y�N�g��ɂ��邽�߂̔{��
        var magRate = targetAspect / screenAspect;

        //Viewport�����l��Rect���쐬
        var viewportRect = new Rect(0, 0, 1, 1);

        if (magRate < 1)
        {
            //�g�p���鉡����ύX
            viewportRect.width = magRate;

            //������
            viewportRect.x = 0.5f - viewportRect.width * 0.5f;
        }
        else
        {
            //�g�p����c����ύX
            viewportRect.height = 1 / magRate;

            //������
            viewportRect.y = 0.5f - viewportRect.height * 0.5f;
        }

        //�J������Viewport�ɓK�p
        targetCamera.rect = viewportRect; 

       
    }
}
