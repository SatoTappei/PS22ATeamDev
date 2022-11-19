using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�W�𐶐�����
/// </summary>
public class Generator : MonoBehaviour
{
    [Header("�X�e�[�W���\������u���b�N�̐ݒ�")]
    [SerializeField] GameObject _blockPrefab;
    [SerializeField] int _size;

    [Header("�X�e�[�W�̐ݒ�")]
    [SerializeField] int _width;
    [SerializeField] int _offsetY;

    [Header("�g�̑傫���̐ݒ�")]
    [Range(0, 0.1f), SerializeField] float _noisePower;

    /// <summary>
    /// �p�[�����m�C�Y�̑傫����������l
    /// �傫������ƃu���b�N���m�̏c�����L����
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

    /// <summary>������</summary>
    void Init()
    {
        // �I�t�Z�b�g��Y���W�����炷
        transform.position += Vector3.up * _offsetY;
    }

    /// <summary>����</summary>
    void Generate()
    {
        for(int z = 0; z < _width; z++)
        {
            for(int x = 0; x < _width; x++)
            {
                // �p�[�����m�C�Y���g�p���ău���b�N�̍��������߂�
                float height = Mathf.PerlinNoise(x * _noisePower, z * _noisePower);
                // �u���b�N�̃T�C�Y�ƃX�e�[�W�̕������炵�Ĕz�u����
                float posX = x * _size - _width / 2 * _size;
                float posZ = z * _size - _width / 2 * _size;
                Vector3 pos = new Vector3(posX, NoiseBase * height, posZ);

                Instantiate(_blockPrefab, pos, Quaternion.identity, transform);
            }
        }
    }
}
