using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OjamaMove : MonoBehaviour
{
    // 移動速度
    public float speed = 2f;

    // 停止時間
    public float stopDuration = 5f; 

    //初期位置
    private Vector3 initialPosition;

    //止まる位置
    private Vector3 stopPosition;

    //上に上がるかどうか
    private bool isMovingUp = true;

    //タイマー
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // 初期位置を保存
        initialPosition = transform.position;
        // 停止位置を計算
        stopPosition = new Vector3(0f, -2f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // 上方向に移動
        if (isMovingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);

            // 停止位置に到達したら停止する
            if (transform.position.y >= stopPosition.y)
            {
                isMovingUp = false;
                timer = 0f;
            }
        }
        // 停止
        else
        {
            timer += Time.deltaTime;

            // 停止時間経過後、初期位置に戻る
            if (timer >= stopDuration)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
               if( transform.position.y <= initialPosition.y)
                {
                    gameObject.SetActive(false);
                }
                
            }
        }
    }
}
