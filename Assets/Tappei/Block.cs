using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�W���\������u���b�N�𐧌䂷��R���|�[�l���g
/// </summary>
public class Block : MonoBehaviour
{
    [Header("�u���b�N�̗h�ꕝ")]
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
