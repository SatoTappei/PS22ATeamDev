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

    bool _inGame;

    void Start()
    {
        _inGame = true;
        _player = GameObject.FindWithTag("Player");
        _uIManager = GameObject.Find("MainUI").GetComponent<UIManager>();
        _fadeManager = GetComponent<FadeManager>();
        _spawnManager = GetComponent<SpawnManager>();
    }

    void Update()
    {
        if (_inGame) 
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

            //�N���A�����Ƃ��̏���
            GameClear();
        }
        
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
        if (_player != null &&_player.transform.position.y < _yRange)
        {
            Destroy(_player);   //�v���C���[��kill

            //�Q�[���I�[�o�[�̎��̏���
            GameOver();

        }
    }

    //�G�����������̃I�u�W�F�N�g�����̂��߂̊֐�
    void EnemyKill()
    {
        //�G�̔z����擾
        GameObject[] _enemys = GameObject.FindGameObjectsWithTag("Enemy");
        //�G�����������̔��肱���ɏ���
        foreach (GameObject enemy in _enemys) 
        {
            if (enemy.transform.position.y < _yRange) 
            {
                Destroy(enemy);
            }
        }
    }

    //GameClear�̎��̏�����`��
    void GameClear() 
    {
        if (_uIManager.NowWave == _uIManager._maxWave && _uIManager.EnemyCount() == 0) 
        {
            //�Q�[�����~�߂�
            _inGame = false;
            //�Q�[���N���A�̎��̏����������ɏ���


        }
    }

    //GameOver �̎��̏�����`��
    void GameOver() 
    {
        //�Q�[�����~�߂�
        _inGame = false;
        //�Q�[���I�[�o�[�̎��̏����������ɏ���


    }
}
