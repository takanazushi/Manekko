using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePendulum : MonoBehaviour
{
    //振り子で使う変数
    public float swigSpeed = 1.0f;
    public float swingDistans = 1.0f;
    float xPos = 0.0f;

    //オブジェクトの現在位置
    private Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        //現在位置取得
        initialPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        xPos = Mathf.Sin(Time.time * swigSpeed) * swingDistans;
        Vector3 newPos = new Vector3(initialPos.x + xPos, initialPos.y, initialPos.z);
        this.transform.position = newPos;
    }
}
