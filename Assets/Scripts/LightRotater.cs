using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotater : MonoBehaviour
{
    public int rotationSpeed;
    void Update()
    {
        transform.Rotate(0, 1f * rotationSpeed * Time.deltaTime, 0f);
    }
}
