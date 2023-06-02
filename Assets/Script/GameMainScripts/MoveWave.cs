using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWave : MonoBehaviour
{
    //オブジェクトの幅
    private float width = 0;

    //オブジェクトの現在位置
    private Vector3 initialPos;

    //波で使う変数
    public float waveSpeed = 1.0f;
    public float waveDistans = 1.0f;
    public float moveSpeed = 1.0f;
    float yPos = 0.0f;
    private float TimeCounter;

    // Start is called before the first frame update
    void Start()
    {
        //現在位置取得
        initialPos = this.transform.position;

        //オブジェクトの幅取得
        Renderer renderer = GetComponent<Renderer>();
        width = renderer.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        TimeCounter += Time.deltaTime;

        //波のように移動する
        yPos = Mathf.Sin(TimeCounter * waveSpeed) * waveDistans;
        Vector3 newPos = new Vector3(initialPos.x + TimeCounter * moveSpeed, initialPos.y + yPos, initialPos.z);
        this.transform.position = newPos;

        //画面外に出たら左から出てくる
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        if (screenPos.x >= Screen.width)
        {
            if (screenPos.x - width >= Screen.width)
            {
                initialPos = new Vector3(-4, initialPos.y, initialPos.z);
                TimeCounter = 0.0f;
            }
        }
    }
}
