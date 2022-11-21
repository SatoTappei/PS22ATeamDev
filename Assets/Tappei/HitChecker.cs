using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������ʒu���C������
/// </summary>
public class HitChecker : MonoBehaviour
{
    readonly Vector3 RayOffset = new Vector3(0.0f, 2.5f, 0.0f);
    readonly float RayRadius = 0.01f;
    readonly float RayDistance = 100.0f;

    [SerializeField] Transform _checker;
    [SerializeField] LayerMask _mask;

    float _prevY = 0;

    void Start()
    {
        _prevY = transform.position.y;
    }

    void LateUpdate()
    {
        SetCharacterPosY();
    }

    /// <summary>Y���W���Z�b�g����</summary>
    void SetCharacterPosY()
    {
        // �t�B�[���h�͈̔͂�ݒ肵���R���C�_�[�Ɛ����ɂȂ�悤��Ray���΂���
        // ���������ꍇ�̓t�B�[���h���ɂ���Ƃ݂Ȃ�
        // ���̕�������Ȃ��ƃt�B�[���h�O�ɏo���Ƃ���Ray��������Ȃ����߁A���W���O�̍��W�ɍX�V����Ă��܂�
        Ray upRay = new Ray(transform.position, _checker.up);
        if (Physics.Raycast(upRay, RayDistance))
        {
            Vector3 pos = transform.position;
            Ray underRay = new Ray(pos + RayOffset, Vector3.down);

            // �t�B�[���h���ɋ���Ή�������Ray���΂��ău���b�N�̏�ɂ��邩���肷��
            if (Physics.SphereCast(underRay, RayRadius, out RaycastHit hit, RayDistance, _mask))
            {
                // Ray���q�b�g���Ă���΂��̈ʒu��ێ�����
                pos.y = hit.point.y;
                _prevY = pos.y;
            }
            else
            {
                // ������Ȃ�������Y���W��O�񓖂������ʒu�ɖ߂�
                pos.y = _prevY;
                transform.position = pos;
            }
        }
        else
        {
            enabled = false;
        }
    }
}
