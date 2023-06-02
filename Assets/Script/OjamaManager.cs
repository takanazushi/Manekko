using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OjamaManager : MonoBehaviour
{
    public List<GameObject> prefabList; // Prefabを管理するリスト
    private List<GameObject> activePrefabs; // ゲーム上に表示されているPrefabのリスト

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
                Debug.Log("今のレベル：" + Level);
                Debug.Log("次のレベル：" + NextLevel);
        }
        
       
    }

    private void SpawnPrefab()
    {

        for (int i = 0; i < 4; i++) 
        {
            // ランダムにPrefabを選択
            int index = Random.Range(0, prefabList.Count);
            GameObject prefab = prefabList[index];

            // Prefabを生成してゲーム上に表示
            GameObject spawnedPrefab = Instantiate(prefab, prefab.transform.position, Quaternion.identity);
            activePrefabs.Add(spawnedPrefab);
        }

        
    }

    public void LevelUp()
    {
        // ゲーム上に表示されているPrefabをすべて削除
        foreach (GameObject prefab in activePrefabs)
        {
            Destroy(prefab);
        }
        activePrefabs.Clear();

        // 新たなPrefabを表示
        SpawnPrefab();

        NextLevel += 1;
    }
}
