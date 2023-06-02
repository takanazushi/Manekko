using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private Vector3 start;
    private Vector3 Goal;
    private float Time;

    // Start is called before the first frame update
    void Start()
    {
        start = this.gameObject.transform.position;
        Goal = new Vector3(1.5f, -0.5f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
