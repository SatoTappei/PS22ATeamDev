using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 無理やり位置を修正する
/// </summary>
public class HitChecker : MonoBehaviour
{
    readonly Vector3 RayOffset = new Vector3(0.0f, 2.5f, 0.0f);
    readonly float RayRadius = 0.01f;
    readonly float RayDistance = 100.0f;

    [SerializeField] Transform _checker;
    
    //LayerMask _mask;
    float _prevY = 0;

    void Start()
    {
        _prevY = transform.position.y;
    }

    void LateUpdate()
    {
        SetCharacterPosY();
    }

    /// <summary>Y座標をセットする</summary>
    void SetCharacterPosY()
    {
        // フィールドの範囲を設定したコライダーと垂直になるようにRayを飛ばして
        // 当たった場合はフィールド内にいるとみなす
        // この分岐をしないとフィールド外に出たときもRayが当たらないため、座標が前の座標に更新されてしまう
        Ray upRay = new Ray(transform.position, _checker.up);
        if (Physics.Raycast(upRay, RayDistance))
        {
            Vector3 pos = transform.position;
            Ray underRay = new Ray(pos + RayOffset, Vector3.down);

            if (Physics.SphereCast(underRay, RayRadius, out RaycastHit hit, RayDistance/*, _mask*/))
            {
                pos.y = hit.point.y;
                _prevY = hit.point.y;
            }
            else
            {
                pos.y = _prevY;
                transform.position = pos;
            }
        }
    }
}
