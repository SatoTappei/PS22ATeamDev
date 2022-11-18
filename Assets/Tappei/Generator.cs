using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージを生成する
/// </summary>
public class Generator : MonoBehaviour
{
    /// <summary>ステージを構成する1マスのプレハブ</summary>
    [SerializeField] GameObject _massPrefab;

    // ステージの幅と高さ
    [Header("ステージの大きさの設定")]
    [SerializeField] int _width;
    [SerializeField] int _height;
    [Header("波の大きさの設定")]
    [Range(0, 0.1f), SerializeField] float _noisePower;

    void Start()
    {
        Generate();
    }

    void Update()
    {
        
    }

    /// <summary>初期化</summary>
    void Init()
    {

    }

    /// <summary>生成</summary>
    void Generate()
    {
        for(int z = 0; z < _width; z++)
        {
            for(int x = 0; x < _height; x++)
            {
                float valueX = x;
                float valueZ = z;
                float height = Mathf.PerlinNoise(valueX * _noisePower, valueZ * _noisePower);
                Vector3 pos = new Vector3(valueX - _width / 2, 10 * height, valueZ - _height / 2);

                Instantiate(_massPrefab, pos, Quaternion.identity, transform);
            }
        }
    }
}
