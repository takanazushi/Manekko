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
    public string NowTitle="今のスコア";

    [HideInInspector]
    public  int FirstLevel= 0;

    [HideInInspector]
    public  int FirstWrongCount=0;

    [HideInInspector]
    public  float FirstGameTime=0.0f;

    [HideInInspector]
    public int FirstScore = 0;

    [HideInInspector]
    public  string FirstTitle="まだ遊んでいません";

    [HideInInspector]
    public int SecondLevel=0;

    [HideInInspector]
    public int SecondWrongCount=0;

    [HideInInspector]
    public float SecondGameTime=0.0f;

    [HideInInspector]
    public int SecondScore = 0;

    [HideInInspector]
    public string SecondTitle="まだ遊んでいません";

    [HideInInspector]
    public int ThirdLevel=0;

    [HideInInspector]
    public int ThirdWrongCount=0;

    [HideInInspector]
    public int ThirdScore = 0;

    [HideInInspector]
    public float ThirdGameTime=0.0f;

    [HideInInspector]
    public string ThirdTitle="まだ遊んでいません";

    //後で追加するやつ
    //称号
    //1番目/2番目/3番目の全てのスコアと称号

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
