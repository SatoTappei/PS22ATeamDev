using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

/// <summary>
/// タイトルのスタートボタンをアニメーションさせる
/// </summary>
public class ButtonAnim : MonoBehaviour,IPointerDownHandler,
                                        IPointerUpHandler,
                                        IPointerEnterHandler,
                                        IPointerExitHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(0.9f, 0.9f, 1), 0.15f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.1f, 1.1f, 1), 0.15f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, 0.15f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, 0.15f);
    }
}
