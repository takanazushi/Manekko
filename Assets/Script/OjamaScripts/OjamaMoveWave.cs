using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OjamaMoveWave : MonoBehaviour
{
    //�I�u�W�F�N�g�̕�
    private float width = 0;

    //�I�u�W�F�N�g�̌��݈ʒu
    private Vector3 initialPos;

    Vector2 TopLeft;
    Vector2 BottomLeft;

    //�g�Ŏg���ϐ�
    public float waveSpeed = 1.0f;
    public float waveDistans = 1.0f;
    public float moveSpeed = 1.0f;
    float yPos = 0.0f;
    private float TimeCounter;

    //private float randomYrange = 0.8f;

    int LRFlag;

    // Start is called before the first frame update
    void Start()
    {
        TopLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
        BottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));

        LRFlag = Random.Range(0, 2);

        if (LRFlag == 1)
        {
            moveSpeed = moveSpeed * -1;
        }


        float randomY = Random.Range(TopLeft.y - 4, BottomLeft.y);

        //���݈ʒu�擾
        initialPos = new Vector3(this.transform.position.x, randomY, this.transform.position.z);

        //�I�u�W�F�N�g�̕��擾
        Renderer renderer = GetComponent<Renderer>();
        width = renderer.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        TimeCounter += Time.deltaTime;

        //�g�̂悤�Ɉړ�����
        yPos = Mathf.Sin(TimeCounter * waveSpeed) * waveDistans;
        Vector3 newPos = new Vector3(initialPos.x + TimeCounter * moveSpeed, initialPos.y + yPos, initialPos.z);
        this.transform.position = newPos;

        if (LRFlag == 0)
        {
            //��ʊO�ɏo���獶����o�Ă���
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            if (screenPos.x >= Screen.width + 200) 
            {
                if (screenPos.x - width >= Screen.width)
                {
                    initialPos = new Vector3(-5, initialPos.y, initialPos.z);
                    TimeCounter = 0.0f;
                }
            }
        }
        else if (LRFlag == 1) 
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            Vector2 leftscreenpos = Camera.main.WorldToScreenPoint(TopLeft);

            //�E����o�Ă���
            if (screenPos.x <= leftscreenpos.x - 200) 
            {
                if (screenPos.x - width <= leftscreenpos.x) 
                {
                    initialPos = new Vector3(5, initialPos.y, initialPos.z);
                    TimeCounter = 0.0f;
                }
            }
        }

       
    }

}
