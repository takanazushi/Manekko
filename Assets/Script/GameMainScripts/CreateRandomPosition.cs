using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandomPosition : MonoBehaviour
{
    [SerializeField]
    [Tooltip("生成するGameObject")]
    private GameObject[] createPrefab;
    [SerializeField]
    [Tooltip("生成する範囲A")]
    private Transform rangeA;
    [SerializeField]
    [Tooltip("生成する範囲B")]
    private Transform rangeB;

    private int number;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CircleCount");
    }

    // Update is called once per frame
    IEnumerator CircleCount()
    {
        int counta = 0;

        while (counta != 5)
        {
            number = Random.Range(0, createPrefab.Length);
            // rangeAとrangeBのx座標の範囲内でランダムな数値を作成
            float x = Random.Range(rangeA.position.x, rangeB.position.x);
            // rangeAとrangeBのy座標の範囲内でランダムな数値を作成
            float y = Random.Range(rangeA.position.y, rangeB.position.y);


            Vector2 pos = new Vector2(x, y);
            Vector2 size = new Vector2(1, 1);

            Collider2D collider = Physics2D.OverlapBox(pos, size, 0f);

            if (!collider)
            {
                Instantiate(createPrefab[number], pos, Quaternion.identity);
                counta += 1;
                yield return new WaitForSeconds(0.0f);
            }
           
        }
        //for(int count = 0; count < 5; count++)
        //{
           
        //}
    }
}
