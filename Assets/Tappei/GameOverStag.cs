using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// ゲームオーバー時の演出をする
/// </summary>
public class GameOverStag : MonoBehaviour
{
    [SerializeField] Transform _logo;
    [SerializeField] Transform _item1;
    [SerializeField] Transform _item2;

    void Awake()
    {
        _logo.transform.localScale = Vector3.zero;
    }

    void Start()
    {
        DOTween.Sequence()
               .Append(_logo.DOScale(Vector3.one, 1.0f).SetEase(Ease.OutBounce))
               .Append(_item1.DOMoveY(200, 0.5f).SetEase(Ease.OutBounce))
               .Join(_item2.DOMoveY(200, 0.5f).SetEase(Ease.OutBounce));
    }

    void Update()
    {
        
    }
}
