using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atodekesu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("�ꎞ�̈�̃p�X�F"+Application.temporaryCachePath);
        Debug.Log("�X�g���[�~���O�A�Z�b�g�̃p�X�F" + Application.streamingAssetsPath);
        Debug.Log("Unity�����p����f�[�^���ۑ������p�X�F" + Application.dataPath);
        Debug.Log("���s���ɕۑ������t�@�C��������p�X�F" + Application.persistentDataPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
