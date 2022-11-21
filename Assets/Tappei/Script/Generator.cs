using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージを生成する
/// </summary>
public class Generator : MonoBehaviour
{
    [Header("ステージを構成するブロックの設定")]
    [SerializeField] GameObject _blockPrefab;
    [SerializeField] int _size;

    [Header("ステージの設定")]
    [SerializeField] int _width;
    [SerializeField] int _offsetY;

    [Header("波の大きさの設定")]
    [Range(0, 0.1f), SerializeField] float _noisePower;

    /// <summary>
    /// パーリンノイズの大きさをかける値
    /// 大きくするとブロック同士の縦幅が広がる
    /// </summary>
    readonly int NoiseBase = 5;

    void Start()
    {
        Init();
        Generate();
    }

    void Update()
    {

    }

    /// <summary>初期化</summary>
    void Init()
    {
        // オフセット分Y座標をずらす
        transform.position += Vector3.up * _offsetY;
    }

    /// <summary>生成</summary>
    void Generate()
    {
        for(int z = 0; z < _width; z++)
        {
            for(int x = 0; x < _width; x++)
            {
                // パーリンノイズを使用してブロックの高さを決める
                float height = Mathf.PerlinNoise(x * _noisePower, z * _noisePower);
                // ブロックのサイズとステージの幅分ずらして配置する
                float posX = x * _size - _width / 2 * _size;
                float posZ = z * _size - _width / 2 * _size;
                Vector3 pos = new Vector3(posX, NoiseBase * height, posZ);

                Instantiate(_blockPrefab, pos, Quaternion.identity, transform);
            }
        }
    }
}
