using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndressModeScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> prefabsA = new List<GameObject>();
    [SerializeField] private List<Image> Wrongprefab = new List<Image>();
    [SerializeField] public GameObject prefabA;
    [SerializeField] private List<GameObject> prefabBs = new List<GameObject>();
    [SerializeField] private List<GameObject> objectsB = new List<GameObject>();
    [SerializeField] private Text levelText;
    [SerializeField] private Text timeText;
    [SerializeField] private float gameTime = 5.0f;
    [SerializeField] private int level = 1;

    private bool isGameOver;
    private bool isGameClear;

    private CountDownScript countdownscript;

    private int wrongcount;

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

        wrongcount = 0;

        SetPrefabA();
        SetPrefabBs();
        SetTime();
        SetLevel();
    }

    private void Update()
    {
        if (isGameOver || countdownscript.isCountDown)
        {
            return;
        }

        ReduceTime();
        SetLevel();
        SetWrongPrefab();

        if (isGameClear == true)
        {
            CorrectAnswer();
        }

        if (gameTime <= 0)
        {
            GameOver();
        }
    }

    public void SetPrefabA()
    {
        // 前に生成された prefabA オブジェクトがあれば削除する
        if (prefabA != null)
        {
            Destroy(prefabA);
        }

        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        int index = UnityEngine.Random.Range(0, prefabsA.Count);
        prefabA = prefabsA[index];
        Vector3 aPos = new Vector3(2, 4, 0);
        prefabA = Instantiate(prefabA, aPos, Quaternion.identity);
    }

    public void ClearPrefabA()
    {
        if (prefabA != null)
        {
            Destroy(prefabA);
        }
    }

    public void SetPrefabBs()
    {
        // レベルが15に達した場合、レベルを保持したままレベル1に戻す
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
        List<int> usedIndices = new List<int>();
        for (int i = 0; i < 2 * level; i++)
        {
            int bIndex;
            do
            {
                bIndex = UnityEngine.Random.Range(0, prefabs.Count);
            } while (usedIndices.Contains(bIndex));
            usedIndices.Add(bIndex);
            // usedIndicesリストをクリアする
            usedIndices.Clear();
            prefabBs.Add(prefabs[bIndex]);
        }



        // リストの中身をランダムに入れ替える
        prefabBs.Shuffle();

        // 同じタグのPrefabの個数と、リスト内で最初に見つかった同じタグのPrefabのインデックスを初期化する
        int sameTagCount = 0;
        int sameTagIndex = -1;

        // リスト内のすべてのPrefabについて、タグがSetPrefabA()で選ばれたPrefabのタグと同じものがあるかどうかチェックする
        for (int i = 0; i < prefabBs.Count; i++)
        {
            if (prefabBs[i].tag == prefabA.tag)
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
                    GameObject newPrefab = prefabs[UnityEngine.Random.Range(0, prefabs.Count)];
                    while (newPrefab.tag == prefabA.tag && sameTagCount < prefabs.Count)
                    {
                        newPrefab = prefabs[UnityEngine.Random.Range(0, prefabs.Count)];
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
            foreach (GameObject prefab in prefabs)
            {
                if (prefab.CompareTag(prefabA.tag))
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
            Vector3 bPos = new Vector3(startPos.x + (1.2f * columnCount), startPos.y - (1 * rowCount), 0);
            GameObject obj = Instantiate(prefabBs[i], bPos, Quaternion.identity);
            objectsB.Add(obj);
            columnCount++;
            if (columnCount >= 4)
            {
                columnCount = 0;
                rowCount++;
            }
        }
    }

    private void SetTime()
    {
        timeText.text = "Time: " + gameTime.ToString("F0");
    }

    private void SetLevel()
    {
        levelText.text = "Level: " + PlayerManager.instance.EndlessMode_NowLevel.ToString();
    }

    private void ReduceTime()
    {
        gameTime -= Time.deltaTime;
        timeText.text = "Time: " + gameTime.ToString("F0");
    }

    private void SetWrongPrefab()
    {
        switch (wrongcount)
        {
            case 1:
                Wrongprefab[4].color = Color.gray;
                break;

            case 2:
                Wrongprefab[3].color = Color.gray;
                break;

            case 3:
                Wrongprefab[2].color = Color.gray;
                break;

            case 4:
                Wrongprefab[1].color = Color.gray;
                break;

            case 5:
                Wrongprefab[0].color = Color.gray;
                break;
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("EndlessMonde_ScoreScene");
        Debug.Log("ゲームオーバー");
        isGameOver = true;
    }

    public void CorrectAnswer()
    {
        PlayerManager.instance.EndlessMode_NowLevel++;
        gameTime = 5.0f;
        level++;
        ClearPrefabA();
        SetPrefabA();
        SetPrefabBs();
        isGameClear = false;
    }

    public void WrongAnswer()
    {
       wrongcount++;

        CheckGameOver();
    }

    private void CheckGameOver()
    {
        if (gameTime <= 0f || wrongcount >= 1)
        {
            GameOver();
        }
    }


}
