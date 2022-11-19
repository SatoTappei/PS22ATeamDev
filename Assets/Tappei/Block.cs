using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージを構成するブロックを制御する
/// </summary>
public class Block : MonoBehaviour
{
    [Header("ブロックの揺れ幅")]
    [SerializeField] float _amplitude;
    float _value;

    void Start()
    {
        _value = transform.position.y;
    }

    void Update()
    {
        // ブロックを上下させる
        _value += Time.deltaTime;
        float posY = transform.localPosition.y + Mathf.Sin(_value) * _amplitude * Time.deltaTime;
        transform.localPosition = new Vector3(transform.localPosition.x, posY, transform.localPosition.z);
    }
}
