using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

/// <summary>
/// ゲームクリアの演出を行う
/// </summary>
public class GameClearStag : MonoBehaviour
{
    [SerializeField] Transform _logo;
    [SerializeField] Transform _stars;
    [SerializeField] GameObject _particle;

    void Awake()
    {
        _logo.transform.localScale = Vector3.zero;
        foreach (Transform child in _stars)
        {
            child.transform.localScale = Vector3.zero;
        }
    }

    IEnumerator Start()
    {
        _logo.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBounce);

        foreach (Transform child in _stars)
        {
            yield return child.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce).SetDelay(0.5f).WaitForCompletion();
        }

        Instantiate(_particle, Vector3.zero, Quaternion.identity);
    }

    void Update()
    {

    }
}
