using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OjamaMove : MonoBehaviour
{
    // ˆÚ“®‘¬“x
    public float speed = 2f;

    // ’âŽ~ŽžŠÔ
    public float stopDuration = 5f; 

    //‰ŠúˆÊ’u
    private Vector3 initialPosition;

    //Ž~‚Ü‚éˆÊ’u
    private Vector3 stopPosition;

    //ã‚Éã‚ª‚é‚©‚Ç‚¤‚©
    private bool isMovingUp = true;

    //ƒ^ƒCƒ}[
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // ‰ŠúˆÊ’u‚ð•Û‘¶
        initialPosition = transform.position;
        // ’âŽ~ˆÊ’u‚ðŒvŽZ
        stopPosition = new Vector3(0f, -2f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // ã•ûŒü‚ÉˆÚ“®
        if (isMovingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);

            // ’âŽ~ˆÊ’u‚É“ž’B‚µ‚½‚ç’âŽ~‚·‚é
            if (transform.position.y >= stopPosition.y)
            {
                isMovingUp = false;
                timer = 0f;
            }
        }
        // ’âŽ~
        else
        {
            timer += Time.deltaTime;

            // ’âŽ~ŽžŠÔŒo‰ßŒãA‰ŠúˆÊ’u‚É–ß‚é
            if (timer >= stopDuration)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
               if( transform.position.y <= initialPosition.y)
                {
                    gameObject.SetActive(false);
                }
                
            }
        }
    }
}
