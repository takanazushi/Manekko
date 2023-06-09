using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OjamaMove_RL : MonoBehaviour
{
    // �ړ����x
    public float speed = 2f;

    // ��~����
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

        //�����_��Y����ύX�����̂ŁA���݈ʒu�ɔ��f
        Vector3 pos = this.gameObject.transform.position;
        pos.y = randomY;
        this.gameObject.transform.position = pos;

        //���݈ʒu�擾
        initialPosition = this.transform.position;
        

        // ��~�ʒu���v�Z
        stopPosition = new Vector3(2.8f, initialPosition.y, 0f);

        Debug.Log(randomY);
    }

    // Update is called once per frame
    void Update()
    {
        // �������Ɉړ�
        if (isMovingLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            // ��~�ʒu�ɓ��B�������~����
            if (transform.position.x <= stopPosition.x)
            {
                isMovingLeft = false;
                timer = 0f;
            }
        }
        // �E�����Ɉړ�
        else
        {
            timer += Time.deltaTime;

            float x = this.gameObject.transform.position.x;

            float newx = x + speed * Time.deltaTime;

            // �����ʒu�Ɍ������Ĉړ�
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
