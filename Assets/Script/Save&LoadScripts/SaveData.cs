using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int FirstLevel;
    public int FirstWrongCount;
    public float FirstGameTime;
    public int FirstScore;
    public string FirstTitle;

    public int SecondLevel;
    public int SecondWrongCount;
    public float SecondGameTime;
    public int SecondScore;
    public string SecondTitle;

    public int ThirdLevel;
    public int ThirdWrongCount;
    public float ThirdGameTime;
    public int ThirdScore;
    public string ThirdTitle;

    public int EndlessFirstLevel = 0;
    public int EndlessFirstScore = 0;

    public int EndlessSecondLevel = 0;
    public int EndlessSecondScore = 0;

    public int EndlessThirdLevel = 0;
    public int EndlessThirdScore = 0;
}
