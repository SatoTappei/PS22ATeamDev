using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージを構成するブロックを制御するコンポーネント
/// </summary>
public class Block : MonoBehaviour
{
    [Header("ブロックの揺れ幅")]
    [Range(0, 0.1f), SerializeField] float _amplitude;
    float _value;

    void Start()
    {
        _value = transform.position.y;
    }

    void Update()
    {
        _value += Time.deltaTime;
        float posY = transform.localPosition.y + Mathf.Sin(_value) * _amplitude;
        transform.localPosition = new Vector3(transform.localPosition.x, posY, transform.localPosition.z);
    }
}
