using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRotate : MonoBehaviour
{
    public float rotateSpeed;
    void Update()
    {
        transform.Rotate(0, 0, 50f * Time.deltaTime * rotateSpeed);
    }
}
