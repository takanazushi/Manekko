using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWave : MonoBehaviour
{
    //�I�u�W�F�N�g�̕�
    private float width = 0;

    //�I�u�W�F�N�g�̌��݈ʒu
    private Vector3 initialPos;

    //�g�Ŏg���ϐ�
    public float waveSpeed = 1.0f;
    public float waveDistans = 1.0f;
    public float moveSpeed = 1.0f;
    float yPos = 0.0f;
    private float TimeCounter;

    // Start is called before the first frame update
    void Start()
    {
        //���݈ʒu�擾
        initialPos = this.transform.position;

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

        //��ʊO�ɏo���獶����o�Ă���
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        if (screenPos.x >= Screen.width)
        {
            if (screenPos.x - width >= Screen.width)
            {
                initialPos = new Vector3(-4, initialPos.y, initialPos.z);
                TimeCounter = 0.0f;
            }
        }
    }
}
