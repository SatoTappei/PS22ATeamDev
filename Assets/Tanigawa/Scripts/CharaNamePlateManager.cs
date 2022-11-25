using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaNamePlateManager : MonoBehaviour
{
    [SerializeField] Text _nameText;
    //[SerializeField,Header("�L�����̖��O")] string _Name;
    //[SerializeField, Header("�e�L�X�g�̐F")] Color _color;
    [SerializeField] Transform _parentPos;
    [SerializeField] GameObject _uI;

    void Awake()
    {
        //_nameText.color = _color;

        //���͂��ꂽText���Z�b�g����
        //_nameText.text = _Name;

        //��ԏ�̐e�I�u�W�F�N�g���擾
        _parentPos = transform.root.gameObject.transform;          //�e�ł���L�����N�^�[�̍��W
    }
    void Start()
    {
    
    }
    void Update()
    {
        _uI.transform.position = new Vector3(_parentPos.position.x, _parentPos.position.y + 8f, _parentPos.position.z);    //UI�̈ʒu���Z�b�g
    }

    void LateUpdate()
    {
        //UI���J�����̌����Ɍ���������
        transform.rotation = Camera.main.transform.rotation;

    }
}
