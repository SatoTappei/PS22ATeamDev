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
            () => _shakeAngles,             // 開始時の値
            shake => _shakeAngles = shake,  // パラメータの更新
            duration,                       // 持続時間
            strength,                       // 揺れの強さ
            vibratio)                       // どのくらい振動するか
            .OnComplete(() => _shakeAngles = Vector3.zero);
    }

    public static void Shake()
    {
        DOTween.Shake(
            () => _shakeAngles,             // 開始時の値
            shake => _shakeAngles = shake,  // パラメータの更新
            0.15f,                       // 持続時間
            new Vector3(3, 3, 0),                       // 揺れの強さ
            5)                       // どのくらい振動するか
            .OnComplete(() => _shakeAngles = Vector3.zero);
    }
}
