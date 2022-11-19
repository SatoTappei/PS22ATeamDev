using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>�^�C�g���̃A�j���[�V�������s���X�N���v�g</summary>
public class TitleAnim : MonoBehaviour
{
    [SerializeField] Transform _logoParent;
    [SerializeField] Transform _item1;
    [SerializeField] Transform _item2;

    void Awake()
    {
        foreach (Transform child in _logoParent)
        {
            child.localScale = Vector3.zero;
        }
    }

    void Start()
    {
        StartCoroutine(Play());
    }

    void Update()
    {
        
    }

    /// <summary>���S�A�j���[�V�������Đ�����</summary>
    IEnumerator Play()
    {
        _item1.DOMoveY(50.0f, 1.5f).SetRelative(true).SetLoops(-1, LoopType.Yoyo);
        _item2.DOMoveY(50.0f, 1.5f).SetRelative(true).SetLoops(-1, LoopType.Yoyo);

        foreach (Transform child in _logoParent)
        {
            child.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(0.13f);
        }
    }
}
