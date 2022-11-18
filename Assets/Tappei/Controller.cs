using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージを動かすスクリプト
/// </summary>
public class Controller : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        float rotX = transform.eulerAngles.x + vert;
        float rotZ = transform.eulerAngles.z  -1.0f * hori;

        transform.eulerAngles = new Vector3(rotX, transform.eulerAngles.y, rotZ);
    }
}
