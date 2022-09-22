using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    Transform ball;
    float extraY = 2f;

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 ballVector = new Vector3(0, ball.position.y+extraY, transform.position.z);
        transform.position = ballVector;
    }
}
