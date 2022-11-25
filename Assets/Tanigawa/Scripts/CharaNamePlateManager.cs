using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaNamePlateManager : MonoBehaviour
{
    [SerializeField] Text _nameText;
    //[SerializeField,Header("キャラの名前")] string _Name;
    //[SerializeField, Header("テキストの色")] Color _color;
    [SerializeField] Transform _parentPos;
    [SerializeField] GameObject _uI;

    void Awake()
    {
        //_nameText.color = _color;

        //入力されたTextをセットする
        //_nameText.text = _Name;

        //一番上の親オブジェクトを取得
        _parentPos = transform.root.gameObject.transform;          //親であるキャラクターの座標
    }
    void Start()
    {
    
    }
    void Update()
    {
        _uI.transform.position = new Vector3(_parentPos.position.x, _parentPos.position.y + 8f, _parentPos.position.z);    //UIの位置をセット
    }

    void LateUpdate()
    {
        //UIをカメラの向きに向け続ける
        transform.rotation = Camera.main.transform.rotation;

    }
}
