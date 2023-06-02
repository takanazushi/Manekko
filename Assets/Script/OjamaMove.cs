using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OjamaMove : MonoBehaviour
{
    // �ړ����x
    public float speed = 2f;

    // ��~����
    public float stopDuration = 5f; 

    //�����ʒu
    private Vector3 initialPosition;

    //�~�܂�ʒu
    private Vector3 stopPosition;

    //��ɏオ�邩�ǂ���
    private bool isMovingUp = true;

    //�^�C�}�[
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // �����ʒu��ۑ�
        initialPosition = transform.position;
        // ��~�ʒu���v�Z
        stopPosition = new Vector3(0f, -2f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // ������Ɉړ�
        if (isMovingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);

            // ��~�ʒu�ɓ��B�������~����
            if (transform.position.y >= stopPosition.y)
            {
                isMovingUp = false;
                timer = 0f;
            }
        }
        // ��~
        else
        {
            timer += Time.deltaTime;

            // ��~���Ԍo�ߌ�A�����ʒu�ɖ߂�
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
