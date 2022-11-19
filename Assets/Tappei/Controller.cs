using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージを動かすスクリプト
/// </summary>
public class Controller : MonoBehaviour
{
    [Header("操作性の設定")]
    [SerializeField] float _sensitivity;
    [SerializeField] float _maxAngle;

    void Start()
    {
        
    }

    void Update()
    {
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        float angleX = transform.localEulerAngles.x + vert * Time.deltaTime * _sensitivity;
        float angleZ = transform.localEulerAngles.z - hori * Time.deltaTime * _sensitivity;

        angleX = ClampAngle(angleX);
        angleZ = ClampAngle(angleZ);

        transform.localEulerAngles = new Vector3(angleX, 0, angleZ);
    }

    /// <summary>Clampした値を返す</summary>
    float ClampAngle(float angle)
    {
        if (angle > 180) angle -= 360;
        return Mathf.Clamp(angle, -1.0f * _maxAngle, _maxAngle);
    }
}
