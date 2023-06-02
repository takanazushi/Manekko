using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveScaleChange : MonoBehaviour
{
    //�X�P�[���ω��Ŏg���ϐ�
    public float scaleSpeed = 0;
    public float minScale = 0.05f;
    public float maxScale = 0.1f;
    float scale = 0.0f;
    float time = 0.0f;

    // Update is called once per frame
    void Update()
    {
        scaleSpeed = 0.5f;

        // ���Ԃɉ����ăX�P�[����ω�������
        time = Mathf.PingPong(Time.time * scaleSpeed, 1.0f);
        scale = Mathf.Lerp(minScale, maxScale, time);
        this.gameObject.transform.localScale = new Vector3(scale, scale, scale);
    }

   
}
