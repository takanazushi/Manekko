using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class AspectKeep : MonoBehaviour
{
    [SerializeField,HeaderAttribute("対象とするカメラ")]
    private Camera targetCamera; 

    [SerializeField,HeaderAttribute("目的解像度")]
    private Vector2 aspectVec; 

    

    void Update()
    {
        //画面のアスペクト比
        var screenAspect = Screen.width / (float)Screen.height;

        //目的のアスペクト比
        var targetAspect = aspectVec.x / aspectVec.y;

        //目的のアスペクト比にするための倍率
        var magRate = targetAspect / screenAspect;

        //Viewport初期値でRectを作成
        var viewportRect = new Rect(0, 0, 1, 1);

        if (magRate < 1)
        {
            //使用する横幅を変更
            viewportRect.width = magRate;

            //中央寄せ
            viewportRect.x = 0.5f - viewportRect.width * 0.5f;
        }
        else
        {
            //使用する縦幅を変更
            viewportRect.height = 1 / magRate;

            //中央寄せ
            viewportRect.y = 0.5f - viewportRect.height * 0.5f;
        }

        //カメラのViewportに適用
        targetCamera.rect = viewportRect; 

       
    }
}
