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

        transform.eulerAngles = new Vector3(transform.eulerAngles.x + vert, transform.eulerAngles.y, transform.eulerAngles.z + hori * -1.0f);
    }
}
