using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OjamaManager : MonoBehaviour
{
    public List<GameObject> prefabList; // Prefab���Ǘ����郊�X�g
    private List<GameObject> activePrefabs; // �Q�[����ɕ\������Ă���Prefab�̃��X�g

    private int Level;
    private int NextLevel;

    // Start is called before the first frame update
    void Start()
    {
        activePrefabs = new List<GameObject>();
        Level = PlayerManager.instance.NowLevel;
        NextLevel = PlayerManager.instance.NowLevel + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Level = Level + 1;
        }

        //Level = PlayerManager.instance.NowLevel;

        if (Level == NextLevel)
        {
                LevelUp();
                Debug.Log("���̃��x���F" + Level);
                Debug.Log("���̃��x���F" + NextLevel);
        }
        
       
    }

    private void SpawnPrefab()
    {

        for (int i = 0; i < 4; i++) 
        {
            // �����_����Prefab��I��
            int index = Random.Range(0, prefabList.Count);
            GameObject prefab = prefabList[index];

            // Prefab�𐶐����ăQ�[����ɕ\��
            GameObject spawnedPrefab = Instantiate(prefab, prefab.transform.position, Quaternion.identity);
            activePrefabs.Add(spawnedPrefab);
        }

        
    }

    public void LevelUp()
    {
        // �Q�[����ɕ\������Ă���Prefab�����ׂč폜
        foreach (GameObject prefab in activePrefabs)
        {
            Destroy(prefab);
        }
        activePrefabs.Clear();

        // �V����Prefab��\��
        SpawnPrefab();

        NextLevel += 1;
    }
}
