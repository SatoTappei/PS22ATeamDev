using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�W�𓮂����X�N���v�g
/// </summary>
public class Controller : MonoBehaviour
{
    [Header("���쐫�̐ݒ�")]
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

    /// <summary>Clamp�����l��Ԃ�</summary>
    float ClampAngle(float angle)
    {
        if (angle > 180) angle -= 360;
        return Mathf.Clamp(angle, -1.0f * _maxAngle, _maxAngle);
    }
}
