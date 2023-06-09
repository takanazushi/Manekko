using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OjamaMove_RL : MonoBehaviour
{
    // 移動速度
    public float speed = 2f;

    // 停止時間
    public float stopDuration = 5f;

    private Vector3 initialPosition;
    private Vector3 stopPosition;
    private bool isMovingLeft = true;
    private float timer = 0f;

    Vector2 TopLeft;
    Vector2 BottomLeft;

    // Start is called before the first frame update
    void Start()
    {
        TopLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
        BottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));

        UnityEngine.Random.InitState(DateTime.Now.Millisecond);

        float randomY = UnityEngine.Random.Range(TopLeft.y - 4, BottomLeft.y);

        Debug.Log(randomY);

      
        initialPosition = new Vector3(this.transform.position.x, randomY, this.transform.position.z);

        //ランダムY軸を変更したので、現在位置に反映
        Vector3 pos = this.gameObject.transform.position;
        pos.y = randomY;
        this.gameObject.transform.position = pos;

        //現在位置取得
        initialPosition = this.transform.position;
        

        // 停止位置を計算
        stopPosition = new Vector3(2.8f, initialPosition.y, 0f);

        Debug.Log(randomY);
    }

    // Update is called once per frame
    void Update()
    {
        // 左方向に移動
        if (isMovingLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            // 停止位置に到達したら停止する
            if (transform.position.x <= stopPosition.x)
            {
                isMovingLeft = false;
                timer = 0f;
            }
        }
        // 右方向に移動
        else
        {
            timer += Time.deltaTime;

            float x = this.gameObject.transform.position.x;

            float newx = x + speed * Time.deltaTime;

            // 初期位置に向かって移動
            transform.position = new Vector3(newx, initialPosition.y, transform.position.z);

            if (transform.position.x >= initialPosition.x)
            {
                transform.position = initialPosition;
                isMovingLeft = true;
                timer = 0f;
            }


        }
    }
}
