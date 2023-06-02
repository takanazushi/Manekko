using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OjamaMove_LR : MonoBehaviour
{
    // �ړ����x
    public float speed = -2f;

    // ��~����
    public float stopDuration = 5f; 

    private Vector3 initialPosition;
    private Vector3 stopPosition;
    private bool isMovingRight = true;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // �����ʒu��ۑ�
        initialPosition = transform.position;

        // ��~�ʒu���v�Z
        stopPosition = new Vector3(2.8f, 1.8f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // �E�����Ɉړ�
        if (isMovingRight)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            // ��~�ʒu�ɓ��B�������~����
            if (transform.position.x <= stopPosition.x)
            {
                isMovingRight = false;
                timer = 0f;
            }
        }
        // �������Ɉړ�
        else
        {
            timer += Time.deltaTime;

            float x = this.gameObject.transform.position.x;

            float newx = x + speed * Time.deltaTime;

            // �����ʒu�Ɍ������Ĉړ�
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
