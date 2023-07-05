using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    public Text textPrefab;

    public void OnPointEnter_MainGame()
    {
        textPrefab.enabled = true;

        //�e�L�X�g�̓��e��ݒ肷��
        textPrefab.text = "�@���x��100��ڎw���ē����������悤�I�@";
    }

    public void OnPointEnter_EndlessMode()
    {
        textPrefab.enabled = true;

        //�e�L�X�g�̓��e��ݒ肷��
        textPrefab.text = "�m�[�~�X�łǂ��܂ł����邩�ȁH�@�@�@�@�@�@";
    }

    public void OnPointEnter_Ranking()
    {
        textPrefab.enabled = true;

        //�e�L�X�g�̓��e��ݒ肷��
        textPrefab.text = "���܂ł̐��т�������I�@�@�@�@�@�@�@�@�@";
    }


    public void OnPointerExit()
    {
        textPrefab.enabled = false;

        textPrefab.text="";
    }

    
}
