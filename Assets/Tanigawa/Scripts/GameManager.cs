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
    //SpawnManager _spawnManager;

    //�v���C���[�̃I�u�W�F�N�g
    GameObject _player;
    GameObject[] _enemys;//�G�̔z��
    [SerializeField, Header("Wave�����̓G�̑g�ݍ��킹�̃v���n�u")] List<GameObject> _spawners;
    [SerializeField] Transform _spawnPos;   //�G�̕����ʒu
    //�L�����N�^�[���������߂�y���W�̌��E���W
    [SerializeField,Header("�L�����N�^�[��������͈�")]�@float _yRange;
    //inGame�t���O
    [SerializeField, Header("�C���Q�[���t���O")] bool _inGame;
     bool _gameClear;
     bool _gameOver;

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
        if (_inGame) 
        {
            _player = GameObject.FindWithTag("Player");
            _uIManager = GameObject.Find("MainUI").GetComponent<UIManager>();
            _fadeManager = GetComponent<FadeManager>();
            //_spawnManager = GetComponent<SpawnManager>();
        }
    }

    void Update()
    {
        GameStart();

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
            //_spawnManager.EnemySpawn1();
            EnemySpawn(_spawners[0]);
            //�G�̔z����擾
            _enemys = GameObject.FindGameObjectsWithTag("Enemy");
        }
        if (_uIManager.NowWave == 2)//Wave2�̂Ƃ� 
        {
            //Wave2�̓G�𐶐����鏈����`��
            //_spawnManager.EnemySpawn2();
            EnemySpawn(_spawners[1]);
            //�G�̔z����擾
            _enemys = GameObject.FindGameObjectsWithTag("Enemy");
        }
        if (_uIManager.NowWave == 3)//Wave3�̂Ƃ� 
        {
            //Wave3�̓G�𐶐����鏈����`��
            //_spawnManager.EnemySpawn3();
            EnemySpawn(_spawners[2]);
            //�G�̔z����擾
            _enemys = GameObject.FindGameObjectsWithTag("Enemy");
        }
    }

    //
    void EnemySpawn(GameObject spawn)
    {
        Instantiate(spawn, _spawnPos);
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
            //�N���A�t���O
            _gameClear = true;
            //�Q�[���N���A�̎��̏���
            //_fadeManager.StartFadeOut("");  //fade����
            //�N���A���̃��S�H���o�������������ɏ���



            ////�V�[���̃��[�h
            if (Input.GetKeyDown(KeyCode.Space) && _gameClear) //Space�L�[����������
            {
                SceneManager.LoadScene("Title1");
                

            }


        }
    }

    //GameOver �̎��̏�����`��
    void GameOver() 
    {
        //�Q�[�����~�߂�
        _inGame = false;
        //�Q�[���I�[�o�[�t���O
        _gameOver = true;
        //�Q�[���I�[�o�[�̎��̏���
        //_fadeManager.StartFadeOut("");  //fade����
        //�N���A���̃��S�H���o�������������ɏ���



        //�V�[���̃��[�h
        if (Input.GetKeyDown(KeyCode.Space) && _gameOver) //Space�L�[����������
        {
            SceneManager.LoadScene("Title1");
            


        }


    }

    //title����Q�[�����X�^�[�g����
    void GameStart() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_inGame && !_gameClear && !_gameOver) //Space�L�[����������
        {
            //�V�[���̃��[�h
            SceneManager.LoadScene("InGame1");
            //fade
            //_fadeManager.StartFadeIn();
            //InGame�V�[���Ŏg��������L��������
            _inGame = true;
        }
    }
}
