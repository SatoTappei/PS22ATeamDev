using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //�Q��
    UIManager _uIManager;
    FadeManager _fadeManager;
    SpawnManager _spawnManager;

    //�v���C���[�̃I�u�W�F�N�g
    GameObject _player;
    //�L�����N�^�[���������߂�y���W�̌��E���W
    [SerializeField,Header("�L�����N�^�[��������͈�")]�@float _yRange;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _uIManager = GameObject.Find("MainUI").GetComponent<UIManager>();
        _fadeManager = GetComponent<FadeManager>();
        _spawnManager = GetComponent<SpawnManager>();
    }

    void Update()
    {
        //UI����@���ꂼ��̐��l��UI�ɍX�V���Ă���
        _uIManager.OutputNowWave();//���݂�Wave�o��
        _uIManager.OutputRemainingWave();//�c���Wave�o��
        _uIManager.OutputEnemyCount();//�G�̐��o��

        //�L�����N�^�[�̏���
        PlayerKill();//player�����񂾂Ƃ��̏���
        EnemyKill();//�G�����񂾂Ƃ��̏���

        //Wave���֘A
        WaveChange();//
    }

    //�G�̐������Ǘ�����֐�
    void WaveChange()
    {
        //Wave���ƂɈႤ����������
        if (_uIManager.NowWave == 1)//Wave1�̂Ƃ� 
        {
            //Wave1�̓G�𐶐����鏈����`��
            _spawnManager.EnemySpawn1();
        }
        if (_uIManager.NowWave == 2)//Wave2�̂Ƃ� 
        {
            //Wave2�̓G�𐶐����鏈����`��
            _spawnManager.EnemySpawn2();
        }
        if (_uIManager.NowWave == 3)//Wave3�̂Ƃ� 
        {
            //Wave3�̓G�𐶐����鏈����`��
            _spawnManager.EnemySpawn3();
        }
    }

    //�v���C���[�����������̃I�u�W�F�N�g�����ƃV�[���J�ڂ��s���֐�
    void PlayerKill()
    {
        if (_player.transform.position.y < _yRange && _player != null)
        {
            Destroy(_player);   //�v���C���[��kill

            //�Q�[���I�[�o�[�̏�����`��


        }
    }

    //�G�����������̃I�u�W�F�N�g�����̂��߂̊֐�
    void EnemyKill()
    {
        
    }
}
