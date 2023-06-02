using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRotation : MonoBehaviour
{
    //âÒì]Ç≈égÇ§ïœêî
    private Rigidbody2D rigidbody2D;
    private Quaternion startRotation;
    private float rotationCount = 1.0f;
    float rotationSpeed = 800.0f; // âÒì]ë¨ìxÅiìx/ïbÅj
    bool LeftRote = false;
    float NewAngle = 0;
    float RightMaxAngle = 0;
    float LeftMaxAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        startRotation = Quaternion.Euler(0, 0, rigidbody2D.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        // Zé≤é¸ÇËÇ…âÒì]
        float rotateAmount = rotationSpeed * Time.deltaTime;
        NewAngle = rigidbody2D.rotation - rotateAmount;
        //êVÇµÇ¢âÒì]äpìxÇÃåvéZ
        rigidbody2D.MoveRotation(NewAngle);

        RightMaxAngle = -360.0f * rotationCount;
        LeftMaxAngle = 360.0f * rotationCount;

        if (LeftRote == true)
        {
            if (NewAngle > LeftMaxAngle)
            {
                rotationCount += 1.0f;
                Debug.Log(rotationCount - 1);
                Debug.Log(NewAngle);
            }
        }
        else if (LeftRote == false)
        {
            if (NewAngle < RightMaxAngle)
            {
                rotationCount += 1.0f;
                Debug.Log(rotationCount - 1);
                Debug.Log(NewAngle);
            }
        }

        if (rotationCount - 1.0f == 2.0f)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            rotationCount = 1.0f;
            rotationSpeed *= -1;
            if (LeftRote == false)
            {
                LeftRote = true;
            }
            else if (LeftRote == true)
            {
                LeftRote = false;
            }

            Debug.Log("ãtâÒì]");

        }
    }
}
