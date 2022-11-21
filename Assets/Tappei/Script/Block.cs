using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�W���\������u���b�N�𐧌䂷��
/// </summary>
public class Block : MonoBehaviour
{
    [Header("�u���b�N�̗h�ꕝ")]
    [SerializeField] float _amplitude;
    float _value;

    void Start()
    {
        _value = transform.position.y;
    }

    void Update()
    {
        // �u���b�N���㉺������
        _value += Time.deltaTime;
        float posY = transform.localPosition.y + Mathf.Sin(_value) * _amplitude * Time.deltaTime;
        transform.localPosition = new Vector3(transform.localPosition.x, posY, transform.localPosition.z);
    }
}
