using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessMode_ScoreScript : MonoBehaviour
{
    [SerializeField]
    private Text LevelText;

    [SerializeField]
    private Text ScoreText;

    //クラスの参照をする
    public Save saveClass;

    private int MaxScore = 400;

    // Start is called before the first frame update
    void Start()
    {


        if (PlayerManager.instance != null)
        {
            ScoreCalculation();
            DataSave();

            LevelText.text = "Level" + PlayerManager.instance.EndlessMode_NowLevel;
            ScoreText.text = "Score" + PlayerManager.instance.EndlessMode_NowScore;
        }
        else
        {
            Debug.Log("プレイヤーManagerない");
            Destroy(this);
        }
    }

    private void ScoreCalculation()
    {
        //int level = PlayerManager.instance.NowLevel;
        //int time = (int)PlayerManager.instance.NowLevel;
        double Score;

        Score = PlayerManager.instance.EndlessMode_NowLevel;
        Score = Score / MaxScore;
        Score = Score * 100;

        PlayerManager.instance.EndlessMode_NowScore = (int)Score;

        Debug.Log(PlayerManager.instance.EndlessMode_NowScore);
        Debug.Log((int)Score);
    }

    private void DataSave()
    {
        saveClass.enabled = true;

        Debug.Log("セーブができましたYO～");
    }
}
