using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMovement : MonoBehaviour
{
    public int trapMoveSpeed;
    void Update()
    {
        transform.position += Vector3.back * trapMoveSpeed * Time.deltaTime;
        if(transform.position.z <= 0)
        {
            Destroy(gameObject);
        }
    }
}
