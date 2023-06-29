using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameMain : MonoBehaviour
{
    [SerializeField, HeaderAttribute("プレイヤーがクリックするオブジェクトを格納する配列")] 
    protected List<GameObject> clickObjectPrefabs = new List<GameObject>();

    [SerializeField, HeaderAttribute("見本になるオブジェクトを格納する配列")] 
    protected List<GameObject> targetObjectPrefabs = new List<GameObject>();

    [SerializeField, HeaderAttribute("お手付き回数を表示するUIを格納する配列")] 
    protected List<Image> wrongPrehubs = new List<Image>();

    /// <summary>
    /// 見本のオブジェクトを格納する変数。
    /// </summary>
    [HideInInspector]
    public GameObject TargetObject;

    /// <summary>
    /// 生成されるオブジェクトを格納する変数。
    /// </summary>
    [HideInInspector] 
    protected List<GameObject> prefabBs = new List<GameObject>();

    /// <summary>
    /// 生成されたオブジェクトを管理する変数。
    /// </summary>
    [HideInInspector] 
    protected List<GameObject> objectsB = new List<GameObject>();

    [SerializeField, HeaderAttribute("レベルUIテキストを格納する変数")] 
    protected Text levelText;

    [SerializeField,HeaderAttribute("制限時間UIテキストを格納する変数")] 
    protected Text timeText;

    [SerializeField, HeaderAttribute("ゲームの制限時間")] 
    protected float gameTime = 60f;

    [SerializeField, HeaderAttribute("ゲーム開始時のレベル")] 
    protected int level = 1;

    /// <summary>
    /// GameOverフラグ
    /// </summary>
    protected bool isGameOver;

    /// <summary>
    /// ゲームクリアフラグ
    /// </summary>
    protected bool isGameClear;

    /// <summary>
    /// CountdownScriptを格納する変数
    /// </summary>
    protected CountDownScript countdownscript;

    //違うところで使うためにこうしてる
    public bool IsGameClear
    {
        get { return isGameClear; }
        set { isGameClear = value; }
    }

    private void Start()
    {
        //コンポーネント取得
        countdownscript = FindObjectOfType<CountDownScript>();

        SetPrefabTarget();
        SetPrefabClickObject();
        SetTime();
        SetLevel();
    }

    private void Update()
    {
        //GameOverの場合、もしくはゲーム開始前のカウントダウン中の場合は、処理しない
        if (isGameOver|| countdownscript.isCountDown)
        {
            return;
        }
        else
        {
            //レベルが101を超えた場合、スコアシーンを読み込む
            if (PlayerManager.instance.NowLevel > 101)
            {
                SceneManager.LoadScene("ScoreScene");
                Debug.Log("ゲームクリア！");
                return;
            }
        }

        ReduceTime();
        SetLevel();
        SetWrongPrefab();

        //プレイヤーがClickした物が正解だったら
        if (isGameClear == true) 
        {
            CorrectAnswer();
        }

        //制限時間が0になったら
        if (PlayerManager.instance.NowGameTime <= 0)
        {
            GameOver();
        }

        
    }


    /// <summary>
    /// 見本（ターゲット）を設定する関数
    /// </summary>
    public virtual void SetPrefabTarget()
    {
        // 前に生成された TargetObject オブジェクトがあれば削除する
        if (TargetObject != null)
        {
            Destroy(TargetObject);
        }

        //ランダムシードを設定する
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);

        //0から見本オブジェクトの要素の数までのランダムなindex値を取得
        int index = UnityEngine.Random.Range(0, targetObjectPrefabs.Count);

        //indexに対応するPrefabを取得して、代入
        TargetObject = targetObjectPrefabs[index];

        //生成位置を指定
        Vector3 aPos = new Vector3(2, 4, 0);

        //生成して、指定位置に配置する
        TargetObject = Instantiate(TargetObject, aPos, Quaternion.identity);
    }

    /// <summary>
    /// 見本（ターゲット）を削除する関数
    /// </summary>
    public virtual void ClearPrefabTarget()
    {
        //targetObjectが存在していたら
        if (TargetObject != null)
        {
            //削除する
            Destroy(TargetObject);
        }
    }

    /// <summary>
    /// プレイヤーがClickする対象のObjectを生成する処理
    /// </summary>
    public virtual void SetPrefabClickObject()
    {
        //レベルが15に達した場合、レベルを保持したままレベル1に戻す
        //レベルが14になるごとに、動きが変化する
        if (level == 15) 
        {
            level = 1;
        }

        // 生成済みのオブジェクトを非アクティブにしてリストをクリアする
        foreach (GameObject obj in objectsB)
        {
            obj.SetActive(false);
            Destroy(obj);
        }
        objectsB.Clear();
        prefabBs.Clear();

        // ランダムにPrefabを選択し、リストに追加する
        for (int i = 0; i < 2 * level; i++)
        {
            int bIndex;

            //ランダムなindexを選択する
            bIndex = UnityEngine.Random.Range(0, clickObjectPrefabs.Count);

            //生成されたオブジェクトリストに追加する
            prefabBs.Add(clickObjectPrefabs[bIndex]);
        }

        // リストの中身をランダムに入れ替える
        prefabBs.Shuffle();

        // 同じタグのPrefabの個数と、リスト内で最初に見つかった同じタグのPrefabのインデックスを初期化する
        int sameTagCount = 0;
        int sameTagIndex = -1;

        // リスト内のすべてのPrefabについて、タグがSetPrefabA()で選ばれたPrefabのタグと同じものがあるかどうかチェックする
        for (int i = 0; i < prefabBs.Count; i++)
        {
            if (prefabBs[i].tag == TargetObject.tag)
            {
                // 同じタグのPrefabが見つかった場合、その数をカウントする
                sameTagCount++;

                // まだ同じタグのPrefabが見つかっていない場合、そのインデックスを記憶する
                if (sameTagIndex == -1)
                {
                    sameTagIndex = i;
                }

                // すでに同じタグのPrefabが見つかっている場合、ランダムにPrefabを選び、同じタグのPrefabを置き換える
                else
                {
                    // 同じタグのPrefabを選んでも良いようにするため、新しいPrefabを選ぶ際にはすでに選んだPrefabも含める
                    GameObject newPrefab = clickObjectPrefabs[UnityEngine.Random.Range(0, clickObjectPrefabs.Count)];

                    //条件1:新しいプレハブがtargetObjectのタグと同じ
                    //条件2:新しいPrefabのタグがまだ選ばれていない回数が、ClickObjectの総数よりも少ない
                    while (newPrefab.tag == TargetObject.tag && sameTagCount < clickObjectPrefabs.Count)
                    {
                        //新しいPrefabをランダムに選びなおし
                        newPrefab = clickObjectPrefabs[UnityEngine.Random.Range(0, clickObjectPrefabs.Count)];
                    }

                    // 同じタグのPrefabをランダムに置き換える
                    prefabBs[i] = newPrefab;
                }
            }
        }

        // 同じタグのPrefabがなければ、ランダムな位置にSetPrefabA()と同じタグのPrefabを1つだけ追加する
        if (sameTagCount == 0)
        {
            // SetPrefabA()と同じタグのPrefabを探す
            GameObject prefabToInstantiate = null;
            foreach (GameObject prefab in clickObjectPrefabs)
            {
                if (prefab.CompareTag(TargetObject.tag))
                {
                    prefabToInstantiate = prefab;
                    break;
                }
            }

            // SetPrefabA()と同じタグのPrefabがあれば、ランダムな位置に追加する
            if (prefabToInstantiate != null)
            {
                int index = UnityEngine.Random.Range(0, 2 * level);
                prefabBs[index] = prefabToInstantiate;
            }
        }

        // オブジェクトを生成し、リストに追加する
        Vector3 startPos = new Vector3(-1.8f, 2, 0);
        int rowCount = 0;
        int columnCount = 0;

        for (int i = 0; i < 2 * level; i++)
        {
            //生成位置を計算
            Vector3 bPos = new Vector3(startPos.x + (1.2f * columnCount), startPos.y - (1 * rowCount), 0);
            
            //PrefabからObjectを生成
            GameObject obj = Instantiate(prefabBs[i], bPos, Quaternion.identity);
            
            //生成したオブジェクトを管理リストに追加する
            objectsB.Add(obj);

            //親子関係を設定する
            obj.transform.parent = GameObject.Find("ObjectParent").transform;

            //columnのカウントを更新する
            columnCount++;

            //columnが上限に達し場合、rowのカウントを更新する
            if (columnCount >= 4)
            {
                columnCount = 0;
                rowCount++;
            }
        }
    }

    /// <summary>
    /// 制限時間をセットする処理
    /// </summary>
    private void SetTime()
    {
        PlayerManager.instance.NowGameTime = gameTime;
        timeText.text = "Time: " + PlayerManager.instance.NowGameTime.ToString("F0");
    }

    /// <summary>
    /// レベルをセットする処理
    /// </summary>
    private void SetLevel()
    {
        levelText.text = "Level: " + PlayerManager.instance.NowLevel.ToString();
    }

    /// <summary>
    /// 制限時間を更新する処理
    /// </summary>
    private void ReduceTime()
    {
        PlayerManager.instance.NowGameTime -= Time.deltaTime;
        timeText.text = "Time: " + PlayerManager.instance.NowGameTime.ToString("F0");
    }

    /// <summary>
    /// お手付き回数を更新する処理
    /// </summary>
    private void SetWrongPrefab()
    {
        switch (PlayerManager.instance.NowWorongCount)
        {
            case 1:
                wrongPrehubs[4].color = Color.gray;
                break;

            case 2:
                wrongPrehubs[3].color = Color.gray;
                break;

            case 3:
                wrongPrehubs[2].color = Color.gray;
                break;

            case 4:
                wrongPrehubs[1].color = Color.gray;
                break;

            case 5:
                wrongPrehubs[0].color = Color.gray;
                break;
        }
    }

    /// <summary>
    /// GameOver処理
    /// </summary>
    private void GameOver()
    {
        SceneManager.LoadScene("ScoreScene");
        Debug.Log("ゲームオーバー");
        isGameOver = true;
    }

    /// <summary>
    ///　正解をClickしたときの処理
    /// </summary>
    public virtual void CorrectAnswer()
    {
        PlayerManager.instance.NowLevel++;  
        level++;
        PlayerManager.instance.NowGameTime += 5f;
        ClearPrefabTarget();
        SetPrefabTarget();
        SetPrefabClickObject();
        isGameClear = false;
    }

    /// <summary>
    /// 不正解をClickした時の処理
    /// </summary>
    public virtual void WrongAnswer()
    {
        PlayerManager.instance.NowGameTime -= 2f;
        PlayerManager.instance.NowWorongCount++;

        CheckGameOver();
    }

    /// <summary>
    /// GameOverになったかどうかを判定する処理
    /// </summary>
    private void CheckGameOver()
    {
        if (PlayerManager.instance.NowGameTime <= 0f || PlayerManager.instance.NowWorongCount >= 5)
        {
            GameOver();
        }
    }
}
