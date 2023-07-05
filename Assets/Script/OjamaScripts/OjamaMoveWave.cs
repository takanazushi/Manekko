using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OjamaMoveWave : MonoBehaviour
{
    //オブジェクトの幅
    private float width = 0;

    //オブジェクトの現在位置
    private Vector3 initialPos;

    Vector2 TopLeft;
    Vector2 BottomLeft;

    //波で使う変数
    public float waveSpeed = 1.0f;
    public float waveDistans = 1.0f;
    public float moveSpeed = 1.0f;
    float yPos = 0.0f;
    private float TimeCounter;

    //private float randomYrange = 0.8f;

    int LRFlag;

    // Start is called before the first frame update
    void Start()
    {
        TopLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
        BottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));

        LRFlag = Random.Range(0, 2);

        if (LRFlag == 1)
        {
            moveSpeed = moveSpeed * -1;
        }


        float randomY = Random.Range(TopLeft.y - 4, BottomLeft.y);

        //現在位置取得
        initialPos = new Vector3(this.transform.position.x, randomY, this.transform.position.z);

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

        if (LRFlag == 0)
        {
            //画面外に出たら左から出てくる
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            if (screenPos.x >= Screen.width + 200) 
            {
                if (screenPos.x - width >= Screen.width)
                {
                    initialPos = new Vector3(-5, initialPos.y, initialPos.z);
                    TimeCounter = 0.0f;
                }
            }
        }
        else if (LRFlag == 1) 
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            Vector2 leftscreenpos = Camera.main.WorldToScreenPoint(TopLeft);

            //右から出てくる
            if (screenPos.x <= leftscreenpos.x - 200) 
            {
                if (screenPos.x - width <= leftscreenpos.x) 
                {
                    initialPos = new Vector3(5, initialPos.y, initialPos.z);
                    TimeCounter = 0.0f;
                }
            }
        }

       
    }

}
