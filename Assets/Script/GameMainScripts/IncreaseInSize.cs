using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IncreaseInSize : MonoBehaviour
{
    //スケール
    float scale = 0.0f;

    //初期スケール
    float baseScale = 0.05f;

    //レベル
    int level;

    //サイズの増加量
    float scaleFactor = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        level = PlayerManager.instance.NowLevel;
    }

    // Update is called once per frame
    void Update()
    {
        //初期スケールに、レベルに応じたサイズの増加量をかけた計算をする
        scale = baseScale + (level / 13f) * scaleFactor;

        //ゲームオブジェクトのスケールを設定する
        this.gameObject.transform.localScale = new Vector3(scale, scale, 0);
    }
}
