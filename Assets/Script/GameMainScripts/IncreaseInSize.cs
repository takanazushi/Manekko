using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IncreaseInSize : MonoBehaviour
{
    //�X�P�[��
    float scale = 0.0f;

    //�����X�P�[��
    float baseScale = 0.05f;

    //���x��
    int level;

    //�T�C�Y�̑�����
    float scaleFactor = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        level = PlayerManager.instance.NowLevel;
      
        
    }

    // Update is called once per frame
    void Update()
    {
        scale = baseScale + (level / 13f) * scaleFactor;
        this.gameObject.transform.localScale = new Vector3(scale, scale, 0);
    }
}