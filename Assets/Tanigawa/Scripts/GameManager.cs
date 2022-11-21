using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //�V���O���g���p
    static GameManager _instance;
    //�Q��
    UIManager _uIManager;
    FadeManager _fadeManager;
    SpawnManager _spawnManager;

    //�v���C���[�̃I�u�W�F�N�g
    GameObject _player;
    //�G�̔z��
    GameObject[] _enemys;
    //�L�����N�^�[���������߂�y���W�̌��E���W
    [SerializeField,Header("�L�����N�^�[��������͈�")]�@float _yRange;

    bool _inGame;

    void Awake()
    {
        //�V�[���ԂŃI�u�W�F�N�g���L�̂��߂̏���
        if ( _instance == null)//�C���X�^���X���Ȃ�������
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else //�����������
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        GameStart();
        if (_inGame) 
        {
            _player = GameObject.FindWithTag("Player");
            _uIManager = GameObject.Find("MainUI").GetComponent<UIManager>();
            _fadeManager = GetComponent<FadeManager>();
            _spawnManager = GetComponent<SpawnManager>();
        }
        
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
            //�G�̔z����擾
            _enemys = GameObject.FindGameObjectsWithTag("Enemy");
        }
        if (_uIManager.NowWave == 2)//Wave2�̂Ƃ� 
        {
            //Wave2�̓G�𐶐����鏈����`��
            _spawnManager.EnemySpawn2();
            //�G�̔z����擾
            _enemys = GameObject.FindGameObjectsWithTag("Enemy");
        }
        if (_uIManager.NowWave == 3)//Wave3�̂Ƃ� 
        {
            //Wave3�̓G�𐶐����鏈����`��
            _spawnManager.EnemySpawn3();
            //�G�̔z����擾
            _enemys = GameObject.FindGameObjectsWithTag("Enemy");
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
            _fadeManager.StartFadeOut("");
  

        }
    }

    //GameOver �̎��̏�����`��
    void GameOver() 
    {
        //�Q�[�����~�߂�
        _inGame = false;
        //�Q�[���I�[�o�[�̎��̏����������ɏ���
        _fadeManager.StartFadeOut("");


    }

    //title����Q�[�����X�^�[�g����
    void GameStart() 
    {
        if (Input.GetKeyDown(KeyCode.Space)) //Space�L�[����������
        {
            //�V�[���̃��[�h
            SceneManager.LoadScene("InGame");
            //fade in�Ăяo��
            _fadeManager.StartFadeIn();
            //InGame�V�[���Ŏg��������L��������
            _inGame = true;
        }
    }
}
