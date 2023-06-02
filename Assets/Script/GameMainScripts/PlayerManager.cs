using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance = null;

    [HideInInspector]
    public int NowLevel = 1;

    [HideInInspector]
    public int NowWorongCount = 0;

    [HideInInspector]
    public float NowGameTime;

    [HideInInspector]
    public int NowScore = 0;

    [HideInInspector]
    public string NowTitle="���̃X�R�A";

    [HideInInspector]
    public  int FirstLevel= 0;

    [HideInInspector]
    public  int FirstWrongCount=0;

    [HideInInspector]
    public  float FirstGameTime=0.0f;

    [HideInInspector]
    public int FirstScore = 0;

    [HideInInspector]
    public  string FirstTitle="�܂��V��ł��܂���";

    [HideInInspector]
    public int SecondLevel=0;

    [HideInInspector]
    public int SecondWrongCount=0;

    [HideInInspector]
    public float SecondGameTime=0.0f;

    [HideInInspector]
    public int SecondScore = 0;

    [HideInInspector]
    public string SecondTitle="�܂��V��ł��܂���";

    [HideInInspector]
    public int ThirdLevel=0;

    [HideInInspector]
    public int ThirdWrongCount=0;

    [HideInInspector]
    public int ThirdScore = 0;

    [HideInInspector]
    public float ThirdGameTime=0.0f;

    [HideInInspector]
    public string ThirdTitle="�܂��V��ł��܂���";

    //��Œǉ�������
    //�̍�
    //1�Ԗ�/2�Ԗ�/3�Ԗڂ̑S�ẴX�R�A�Ə̍�

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
