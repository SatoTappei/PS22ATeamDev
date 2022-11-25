using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening; 

public class CameraShaker : MonoBehaviour
{
    static Vector3 _shakeAngles;
    Vector3 _defaultAngle;

    void Awake()
    {
        _defaultAngle = transform.localEulerAngles;
    }

    void Start()
    {
        
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    Shake(_duration, _strength, _vibratio);
        //}
    }

    void LateUpdate()
    {
        transform.localEulerAngles = _defaultAngle + _shakeAngles;
    }

    public static void Shake(float duration, Vector3 strength, int vibratio)
    {
        DOTween.Shake(
            () => _shakeAngles,             // �J�n���̒l
            shake => _shakeAngles = shake,  // �p�����[�^�̍X�V
            duration,                       // ��������
            strength,                       // �h��̋���
            vibratio)                       // �ǂ̂��炢�U�����邩
            .OnComplete(() => _shakeAngles = Vector3.zero);
    }

    public static void Shake()
    {
        DOTween.Shake(
            () => _shakeAngles,             // �J�n���̒l
            shake => _shakeAngles = shake,  // �p�����[�^�̍X�V
            0.15f,                       // ��������
            new Vector3(3, 3, 0),                       // �h��̋���
            5)                       // �ǂ̂��炢�U�����邩
            .OnComplete(() => _shakeAngles = Vector3.zero);
    }
}
