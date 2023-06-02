using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OjamaMove_LR : MonoBehaviour
{
    // 移動速度
    public float speed = -2f;

    // 停止時間
    public float stopDuration = 5f; 

    private Vector3 initialPosition;
    private Vector3 stopPosition;
    private bool isMovingRight = true;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // 初期位置を保存
        initialPosition = transform.position;

        // 停止位置を計算
        stopPosition = new Vector3(2.8f, 1.8f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // 右方向に移動
        if (isMovingRight)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            // 停止位置に到達したら停止する
            if (transform.position.x <= stopPosition.x)
            {
                isMovingRight = false;
                timer = 0f;
            }
        }
        // 左方向に移動
        else
        {
            timer += Time.deltaTime;

            float x = this.gameObject.transform.position.x;

            float newx = x + speed * Time.deltaTime;

            // 初期位置に向かって移動
            transform.position = new Vector3(newx, transform.position.y, transform.position.z);

            if (transform.position.x >= initialPosition.x)
            {
                transform.position = initialPosition;
                isMovingRight = true;
                timer = 0f;
            }

          
        }
    }
}
