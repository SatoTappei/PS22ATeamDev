using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�W�𐶐�����
/// </summary>
public class Generator : MonoBehaviour
{
    /// <summary>�X�e�[�W���\������1�}�X�̃v���n�u</summary>
    [SerializeField] GameObject _massPrefab;

    // �X�e�[�W�̕��ƍ���
    [Header("�X�e�[�W�̑傫���̐ݒ�")]
    [SerializeField] int _width;
    [SerializeField] int _height;
    [Header("�g�̑傫���̐ݒ�")]
    [Range(0, 0.1f), SerializeField] float _noisePower;

    void Start()
    {
        Generate();
    }

    void Update()
    {
        
    }

    /// <summary>������</summary>
    void Init()
    {

    }

    /// <summary>����</summary>
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
