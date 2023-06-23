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

    //�Ⴄ�Ƃ���Ŏg�����߂ɂ������Ă�
    public bool IsGameClear
    {
        get { return isGameClear; }
        set { isGameClear = value; }
    }

    private void Start()
    {
        //�R���|�[�l���g�擾
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
        // �O�ɐ������ꂽ prefabA �I�u�W�F�N�g������΍폜����
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
        // ���x����15�ɒB�����ꍇ�A���x����ێ������܂܃��x��1�ɖ߂�
        //���x����14�ɂȂ邲�ƂɁA�������ω�����
        if (level == 15)
        {
            level = 1;
        }

        // �����ς݂̃I�u�W�F�N�g���A�N�e�B�u�ɂ��ă��X�g���N���A����
        foreach (GameObject obj in objectsB)
        {
            obj.SetActive(false);
            Destroy(obj);
        }
        objectsB.Clear();
        prefabBs.Clear();

        // �����_����Prefab��I�����A���X�g�ɒǉ�����
        List<int> usedIndices = new List<int>();
        for (int i = 0; i < 2 * level; i++)
        {
            int bIndex;
            do
            {
                bIndex = UnityEngine.Random.Range(0, prefabs.Count);
            } while (usedIndices.Contains(bIndex));
            usedIndices.Add(bIndex);
            // usedIndices���X�g���N���A����
            usedIndices.Clear();
            prefabBs.Add(prefabs[bIndex]);
        }



        // ���X�g�̒��g�������_���ɓ���ւ���
        prefabBs.Shuffle();

        // �����^�O��Prefab�̌��ƁA���X�g���ōŏ��Ɍ������������^�O��Prefab�̃C���f�b�N�X������������
        int sameTagCount = 0;
        int sameTagIndex = -1;

        // ���X�g���̂��ׂĂ�Prefab�ɂ��āA�^�O��SetPrefabA()�őI�΂ꂽPrefab�̃^�O�Ɠ������̂����邩�ǂ����`�F�b�N����
        for (int i = 0; i < prefabBs.Count; i++)
        {
            if (prefabBs[i].tag == prefabA.tag)
            {
                // �����^�O��Prefab�����������ꍇ�A���̐����J�E���g����
                sameTagCount++;
                // �܂������^�O��Prefab���������Ă��Ȃ��ꍇ�A���̃C���f�b�N�X���L������
                if (sameTagIndex == -1)
                {
                    sameTagIndex = i;
                }
                // ���łɓ����^�O��Prefab���������Ă���ꍇ�A�����_����Prefab��I�сA�����^�O��Prefab��u��������
                else
                {
                    // �����^�O��Prefab��I��ł��ǂ��悤�ɂ��邽�߁A�V����Prefab��I�ԍۂɂ͂��łɑI��Prefab���܂߂�
                    GameObject newPrefab = prefabs[UnityEngine.Random.Range(0, prefabs.Count)];
                    while (newPrefab.tag == prefabA.tag && sameTagCount < prefabs.Count)
                    {
                        newPrefab = prefabs[UnityEngine.Random.Range(0, prefabs.Count)];
                    }

                    // �����^�O��Prefab�������_���ɒu��������
                    prefabBs[i] = newPrefab;
                }
            }
        }

        // �����^�O��Prefab���Ȃ���΁A�����_���Ȉʒu��SetPrefabA()�Ɠ����^�O��Prefab��1�����ǉ�����
        if (sameTagCount == 0)
        {
            // SetPrefabA()�Ɠ����^�O��Prefab��T��
            GameObject prefabToInstantiate = null;
            foreach (GameObject prefab in prefabs)
            {
                if (prefab.CompareTag(prefabA.tag))
                {
                    prefabToInstantiate = prefab;
                    break;
                }
            }
            // SetPrefabA()�Ɠ����^�O��Prefab������΁A�����_���Ȉʒu�ɒǉ�����
            if (prefabToInstantiate != null)
            {
                int index = UnityEngine.Random.Range(0, 2 * level);
                prefabBs[index] = prefabToInstantiate;
            }
        }

        // �I�u�W�F�N�g�𐶐����A���X�g�ɒǉ�����
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
        Debug.Log("�Q�[���I�[�o�[");
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
