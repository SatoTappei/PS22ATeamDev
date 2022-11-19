using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Wave,Enemy�֘A
    [SerializeField, Header("���݂̃E�F�[�u��")] int _nowWave = 1;
    [SerializeField, Header("�ő�E�F�[�u��")] int _maxWave = 0;
    [SerializeField,Header("�c��̃E�F�[�u��")] int _remainingWave = 0;
    GameObject[] _enemyArray;

    //Text�֘A
    [SerializeField, Header("���݂̃E�F�[�u�̃e�L�X�g")] Text _nowWaveText;
    [SerializeField, Header("�c��̃E�F�[�u���̃e�L�X�g")] Text _remainingWaveText;
    [SerializeField, Header("�c��̓G�̐��̃e�L�X�g")] Text _enemyCountText;

    //�|�b�v���j���[�֘A
    [SerializeField, Header("�|�b�v���j���[�̃I�u�W�F�N�g")] GameObject _popMenu;
    [SerializeField, Header("�|�b�vTips�̃I�u�W�F�N�g")] GameObject _popTips;
    bool _onPopMenu;

    void Start()
    {
        //PopMenu ��\��
        _popMenu.SetActive(false);
        _onPopMenu = false;
        //PopTips ��\��
        _popTips.SetActive(false);
    }

    void Update()
    {
        //���ꂼ��̐��l���X�V���Ă���
        OutputNowWave();//���݂�Wave�o��
        OutputRemainingWave();//�c���Wave�o��
        OutputEnemyCount();//�G�̐��o��

        //Pop���郁�j���[
        OnPopMenu();
        OnPopTips();

    }

    //���݂̃E�F�[�u���o�͂���֐�
    void OutputNowWave()
    {
        if (EnemyCount() == 0) //�����G�̐����O�Ȃ�
        {
            _nowWave++; //�E�F�[�u�J�E���g�A�b�v
        }
        _nowWaveText.text = "���݂�Wave�F" + _nowWave.ToString();  // Text�ɔ��f
    }

    //�c��̃E�F�[�u�𐔂��ďo�͂���֐�
    void OutputRemainingWave() 
    {
        _remainingWave = _maxWave;  //�c��̃E�F�[�u�̏����l��ݒ�
        if (_remainingWave != 0) //�c�肪�O�o�Ȃ��Ƃ�
        {
            _remainingWave -= _nowWave; //�c��̃E�F�[�u�����炷
        }
        _remainingWaveText.text = "�c���Wave�F" + _remainingWave.ToString();  // Text�ɔ��f
    }

    //�c��̓G�̐����o�͂���֐�
    void OutputEnemyCount() 
    {
        _enemyCountText.text = "�G�F" + EnemyCount().ToString(); // Text�ɔ��f
    }

    //�G�𐔂��Đ���Ԃ��֐�
    int EnemyCount() 
    {
        //�G��z��Ɋi�[
        _enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        //�v�f����Ԃ�
        return _enemyArray.Length;
    }

    //PopMenu�𑀍삷�邽�߂̊֐�
    void OnPopMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //Escape�{�^�������ꂽ��
        {
            if (!_onPopMenu)    //bool�^_onPopMene��false�Ȃ�
            {
                //PopMenu�\��
                _popMenu.SetActive(true);
                _onPopMenu = true;
            }
            else
            {
                //PopMenu��\��
                _popMenu.SetActive(false);
                _onPopMenu = false;
            }
        }
    }

    //PopTips�𑀍삷�邽�߂̊֐�
    void OnPopTips() 
    {
        if (Input.GetKeyDown(KeyCode.Space)) //Spase�{�^����������Ă����
        {
            //PopMenu�\��
            _popTips.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Space))  //Spase�{�^����b������
        {
            //PopMenu�\��
            _popTips.SetActive(false);
        }
    }
}
