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
    [SerializeField, HeaderAttribute("�v���C���[���N���b�N����I�u�W�F�N�g���i�[����z��")] 
    protected List<GameObject> clickObjectPrefabs = new List<GameObject>();

    [SerializeField, HeaderAttribute("���{�ɂȂ�I�u�W�F�N�g���i�[����z��")] 
    protected List<GameObject> targetObjectPrefabs = new List<GameObject>();

    [SerializeField, HeaderAttribute("����t���񐔂�\������UI���i�[����z��")] 
    protected List<Image> wrongPrehubs = new List<Image>();

    /// <summary>
    /// ���{�̃I�u�W�F�N�g���i�[����ϐ��B
    /// </summary>
    [HideInInspector]
    public GameObject TargetObject;

    /// <summary>
    /// ���������I�u�W�F�N�g���i�[����ϐ��B
    /// </summary>
    [HideInInspector] 
    protected List<GameObject> prefabBs = new List<GameObject>();

    /// <summary>
    /// �������ꂽ�I�u�W�F�N�g���Ǘ�����ϐ��B
    /// </summary>
    [HideInInspector] 
    protected List<GameObject> objectsB = new List<GameObject>();

    [SerializeField, HeaderAttribute("���x��UI�e�L�X�g���i�[����ϐ�")] 
    protected Text levelText;

    [SerializeField,HeaderAttribute("��������UI�e�L�X�g���i�[����ϐ�")] 
    protected Text timeText;

    [SerializeField, HeaderAttribute("�Q�[���̐�������")] 
    protected float gameTime = 60f;

    [SerializeField, HeaderAttribute("�Q�[���J�n���̃��x��")] 
    protected int level = 1;

    /// <summary>
    /// GameOver�t���O
    /// </summary>
    protected bool isGameOver;

    /// <summary>
    /// �Q�[���N���A�t���O
    /// </summary>
    protected bool isGameClear;

    /// <summary>
    /// CountdownScript���i�[����ϐ�
    /// </summary>
    protected CountDownScript countdownscript;

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

        SetPrefabTarget();
        SetPrefabClickObject();
        SetTime();
        SetLevel();
    }

    private void Update()
    {
        //GameOver�̏ꍇ�A�������̓Q�[���J�n�O�̃J�E���g�_�E�����̏ꍇ�́A�������Ȃ�
        if (isGameOver|| countdownscript.isCountDown)
        {
            return;
        }
        else
        {
            //���x����101�𒴂����ꍇ�A�X�R�A�V�[����ǂݍ���
            if (PlayerManager.instance.NowLevel > 101)
            {
                SceneManager.LoadScene("ScoreScene");
                Debug.Log("�Q�[���N���A�I");
                return;
            }
        }

        ReduceTime();
        SetLevel();
        SetWrongPrefab();

        //�v���C���[��Click��������������������
        if (isGameClear == true) 
        {
            CorrectAnswer();
        }

        //�������Ԃ�0�ɂȂ�����
        if (PlayerManager.instance.NowGameTime <= 0)
        {
            GameOver();
        }

        
    }


    /// <summary>
    /// ���{�i�^�[�Q�b�g�j��ݒ肷��֐�
    /// </summary>
    public virtual void SetPrefabTarget()
    {
        // �O�ɐ������ꂽ TargetObject �I�u�W�F�N�g������΍폜����
        if (TargetObject != null)
        {
            Destroy(TargetObject);
        }

        //�����_���V�[�h��ݒ肷��
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);

        //0���猩�{�I�u�W�F�N�g�̗v�f�̐��܂ł̃����_����index�l���擾
        int index = UnityEngine.Random.Range(0, targetObjectPrefabs.Count);

        //index�ɑΉ�����Prefab���擾���āA���
        TargetObject = targetObjectPrefabs[index];

        //�����ʒu���w��
        Vector3 aPos = new Vector3(2, 4, 0);

        //�������āA�w��ʒu�ɔz�u����
        TargetObject = Instantiate(TargetObject, aPos, Quaternion.identity);
    }

    /// <summary>
    /// ���{�i�^�[�Q�b�g�j���폜����֐�
    /// </summary>
    public virtual void ClearPrefabTarget()
    {
        //targetObject�����݂��Ă�����
        if (TargetObject != null)
        {
            //�폜����
            Destroy(TargetObject);
        }
    }

    /// <summary>
    /// �v���C���[��Click����Ώۂ�Object�𐶐����鏈��
    /// </summary>
    public virtual void SetPrefabClickObject()
    {
        //���x����15�ɒB�����ꍇ�A���x����ێ������܂܃��x��1�ɖ߂�
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
        for (int i = 0; i < 2 * level; i++)
        {
            int bIndex;

            //�����_����index��I������
            bIndex = UnityEngine.Random.Range(0, clickObjectPrefabs.Count);

            //�������ꂽ�I�u�W�F�N�g���X�g�ɒǉ�����
            prefabBs.Add(clickObjectPrefabs[bIndex]);
        }

        // ���X�g�̒��g�������_���ɓ���ւ���
        prefabBs.Shuffle();

        // �����^�O��Prefab�̌��ƁA���X�g���ōŏ��Ɍ������������^�O��Prefab�̃C���f�b�N�X������������
        int sameTagCount = 0;
        int sameTagIndex = -1;

        // ���X�g���̂��ׂĂ�Prefab�ɂ��āA�^�O��SetPrefabA()�őI�΂ꂽPrefab�̃^�O�Ɠ������̂����邩�ǂ����`�F�b�N����
        for (int i = 0; i < prefabBs.Count; i++)
        {
            if (prefabBs[i].tag == TargetObject.tag)
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
                    GameObject newPrefab = clickObjectPrefabs[UnityEngine.Random.Range(0, clickObjectPrefabs.Count)];

                    //����1:�V�����v���n�u��targetObject�̃^�O�Ɠ���
                    //����2:�V����Prefab�̃^�O���܂��I�΂�Ă��Ȃ��񐔂��AClickObject�̑����������Ȃ�
                    while (newPrefab.tag == TargetObject.tag && sameTagCount < clickObjectPrefabs.Count)
                    {
                        //�V����Prefab�������_���ɑI�тȂ���
                        newPrefab = clickObjectPrefabs[UnityEngine.Random.Range(0, clickObjectPrefabs.Count)];
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
            foreach (GameObject prefab in clickObjectPrefabs)
            {
                if (prefab.CompareTag(TargetObject.tag))
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
            //�����ʒu���v�Z
            Vector3 bPos = new Vector3(startPos.x + (1.2f * columnCount), startPos.y - (1 * rowCount), 0);
            
            //Prefab����Object�𐶐�
            GameObject obj = Instantiate(prefabBs[i], bPos, Quaternion.identity);
            
            //���������I�u�W�F�N�g���Ǘ����X�g�ɒǉ�����
            objectsB.Add(obj);

            //�e�q�֌W��ݒ肷��
            obj.transform.parent = GameObject.Find("ObjectParent").transform;

            //column�̃J�E���g���X�V����
            columnCount++;

            //column������ɒB���ꍇ�Arow�̃J�E���g���X�V����
            if (columnCount >= 4)
            {
                columnCount = 0;
                rowCount++;
            }
        }
    }

    /// <summary>
    /// �������Ԃ��Z�b�g���鏈��
    /// </summary>
    private void SetTime()
    {
        PlayerManager.instance.NowGameTime = gameTime;
        timeText.text = "Time: " + PlayerManager.instance.NowGameTime.ToString("F0");
    }

    /// <summary>
    /// ���x�����Z�b�g���鏈��
    /// </summary>
    private void SetLevel()
    {
        levelText.text = "Level: " + PlayerManager.instance.NowLevel.ToString();
    }

    /// <summary>
    /// �������Ԃ��X�V���鏈��
    /// </summary>
    private void ReduceTime()
    {
        PlayerManager.instance.NowGameTime -= Time.deltaTime;
        timeText.text = "Time: " + PlayerManager.instance.NowGameTime.ToString("F0");
    }

    /// <summary>
    /// ����t���񐔂��X�V���鏈��
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
    /// GameOver����
    /// </summary>
    private void GameOver()
    {
        SceneManager.LoadScene("ScoreScene");
        Debug.Log("�Q�[���I�[�o�[");
        isGameOver = true;
    }

    /// <summary>
    ///�@������Click�����Ƃ��̏���
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
    /// �s������Click�������̏���
    /// </summary>
    public virtual void WrongAnswer()
    {
        PlayerManager.instance.NowGameTime -= 2f;
        PlayerManager.instance.NowWorongCount++;

        CheckGameOver();
    }

    /// <summary>
    /// GameOver�ɂȂ������ǂ����𔻒肷�鏈��
    /// </summary>
    private void CheckGameOver()
    {
        if (PlayerManager.instance.NowGameTime <= 0f || PlayerManager.instance.NowWorongCount >= 5)
        {
            GameOver();
        }
    }
}
