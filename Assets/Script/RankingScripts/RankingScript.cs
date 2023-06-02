using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingScript : MonoBehaviour
{
    //[SerializeField]
    //public Text NowLevel;
    //[SerializeField]
    //public Text NowWorongCount;
    //[SerializeField]
    //public Text NowGameTime;
    //[SerializeField]
    //public Text NowTitle;

    [SerializeField]
    public Text FirstLevel;
    [SerializeField]
    public Text FirstWrongCount;
    [SerializeField]
    public Text FirstGameTime;
    [SerializeField]
    public Text FirstTitle;

    [SerializeField]
    public Text SecondLevel;
    [SerializeField]
    public Text SecondWrongCount;
    [SerializeField]
    public Text SecondGameTime;
    [SerializeField]
    public Text SecondTitle;

    [SerializeField]
    public Text ThirdLevel;
    [SerializeField]
    public Text ThirdWrongCount;
    [SerializeField]
    public Text ThirdGameTime;
    [SerializeField]
    public Text ThirdTitle;

    public Read readClass;
    public Save saveClass;

    // Start is called before the first frame update
    void Start()
    {
        
        
        if (PlayerManager.instance != null)
        {
            //プレイヤーマネージャーの初期化
            PlayerManager.instance.NowLevel = 0;
            PlayerManager.instance.NowTitle = "まだ遊んでいません";
            PlayerManager.instance.NowWorongCount = 0;
            PlayerManager.instance.NowGameTime = 0.0f;

            //再セーブ
            DataSave();

            //ロード
            DataRead();

            FirstLevel.text = PlayerManager.instance.FirstLevel.ToString();
            FirstWrongCount.text = PlayerManager.instance.FirstWrongCount.ToString();
            FirstGameTime.text = PlayerManager.instance.FirstGameTime.ToString("F0");
            FirstTitle.text = PlayerManager.instance.FirstTitle.ToString();

            SecondLevel.text = PlayerManager.instance.SecondLevel.ToString();
            SecondWrongCount.text = PlayerManager.instance.SecondWrongCount.ToString();
            SecondGameTime.text = PlayerManager.instance.SecondGameTime.ToString("F0");
            SecondTitle.text = PlayerManager.instance.SecondTitle.ToString();

            ThirdLevel.text = PlayerManager.instance.ThirdLevel.ToString();
            ThirdWrongCount.text = PlayerManager.instance.ThirdWrongCount.ToString();
            ThirdGameTime.text = PlayerManager.instance.ThirdGameTime.ToString("F0");
            ThirdTitle.text = PlayerManager.instance.ThirdTitle.ToString();
        }
        else
        {
            Debug.Log("プレイヤーManagerない");
            Destroy(this);
        }
    }

   
    private void DataRead()
    {
        //データの読み込み
        readClass.enabled = true;

        Debug.Log("読み込みが終わりましたYO～");
    }

    private void DataSave()
    {
        saveClass.enabled = true;

        Debug.Log("セーブができましたYO～");
    }
}
