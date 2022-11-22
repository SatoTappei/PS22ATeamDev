using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //�V���O���g���p
    static GameManager _instance;

    //�Q��
    FadeManager _fadeManager;
    UIManager _uIManager;

    //�L�����N�^�[�̃I�u�W�F�N�g
    GameObject _player; //�v���C���[
    GameObject[] _enemys;   //�G�̔z��

    //�������G�֘A
    [SerializeField, Header("Wave�����̓G�̑g�ݍ��킹�̃v���n�u")] 
    List<GameObject> _spawners;
    [SerializeField] 
    Transform _spawnPos;   //�G�̕����ʒu

    //�L�����N�^�[���������߂�y���W�̌��E���W
    [SerializeField,Header("�L�����N�^�[��������͈�")]
    float _yRange;

    //�t���O�֘A
    bool _wave1;
    bool _wave2;
    bool _wave3;
    [SerializeField, Header("�Q�[�����J�n���ꂽ�t���O")]
    bool _startGame;�@//�Q�[���̊J�n�𔻒肷��t���O
    [SerializeField, Header("�C���Q�[���t���O")] 
    bool _inGame;�@//inGame�t���O
    bool _gameClear;//�Q�[���̃N���A�𔻒肷��t���O
    bool _gameOver;//�Q�[���̏I���𔻒肷��t���O

    //�V�[���̖��O�ύX�̂��߂̕ϐ�
    [SerializeField,Header("�^�C�g���V�[���̖��O")] string _titleSceneName = "Title";
    [SerializeField, Header("�Q�[���V�[���̖��O")] string _inGameSceneName = "InGame";

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

    }

    void Update()
    {
        //Title�V�[������InGame�V�[���֑J�ڂ���֐����Ăяo���Ă���B
        GameStart();

        //Title�V�[������InGame�V�[���֑J�ڂ����Ƃ��ɂP�x�����Ăяo�����
        OnlyOnceMethod();

        //InGame�ɂ���Ԏ��s�����
        if (_inGame)
        {
            //�L�����N�^�[�̏���
            PlayerKill();//player�����񂾂Ƃ��̏���
            //EnemyKill();
            
            //Wave�֘A
            WaveChange();

            //�N���A�����Ƃ��̏���
            GameClear();

            //UI����@���ꂼ��̐��l��UI�ɍX�V���Ă���
            _uIManager.OutputNowWave();//���݂�Wave�o��
            _uIManager.OutputRemainingWave();//�c���Wave�o��
            _uIManager.OutputEnemyCount();//�G�̐��o��
        }

    }

    //update���łP�񂾂������������֐�
    void OnlyOnceMethod()
    {
        if (_startGame && _inGame)
        {
            _player = GameObject.FindWithTag("Player");�@//�v���C���[�擾
            _uIManager = GameObject.Find("MainUI").GetComponent<UIManager>();
            _fadeManager = GetComponent<FadeManager>();�@//FadeManager�擾
            _startGame = false;
        }
    }

    //�G�̐������Ǘ�����֐�
    void WaveChange()
    {
        //Wave���ƂɈႤ����������
        if (_uIManager.NowWave == 1 && !_wave1) //Wave1�̂Ƃ� 
        {
            //Wave1�̓G�𐶐����鏈����`��
            EnemySpawn(_spawners[0]);
            _wave1 = true;
        }
        if (_uIManager.NowWave == 2 && !_wave2)//Wave2�̂Ƃ� 
        {
            //Wave2�̓G�𐶐����鏈����`��
            EnemySpawn(_spawners[1]);
            _wave2 = true;
        }
        if (_uIManager.NowWave == 3 && !_wave3)//Wave3�̂Ƃ� 
        {
            //Wave3�̓G�𐶐����鏈����`��
            EnemySpawn(_spawners[2]);
            _wave3 = true;
        }
    }

    //�w�肵���G�̑g�ݍ��킹�𐶐����鏈��
    void EnemySpawn(GameObject spawn)
    {
        //�G����
        Instantiate(spawn, _spawnPos);
        /*
        //�G�̔z����擾
        _enemys = GameObject.FindGameObjectsWithTag("Enemy");
        */
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
    /*
    //�G�����������̃I�u�W�F�N�g�����̂��߂̊֐�
    void EnemyKill()
    {
        //�G�����������̔��肱���ɏ���
        if (_enemys != null) 
        {
            foreach (GameObject enemy in _enemys)
            {
                if (enemy.transform.position.y < _yRange)
                {
                    Destroy(enemy);
                }
            }
        }
    }
    */
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
                SceneManager.LoadScene(_titleSceneName);
                

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
            SceneManager.LoadScene(_titleSceneName);
        }
    }

    //title�V�[������fade���Ȃ���InGame�V�[���֑J�ڂ��鏈��
    void GameStart() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_inGame && !_startGame && !_gameClear && !_gameOver) //Space�L�[����������
        {
            //�V�[���̃��[�h
            SceneManager.LoadScene(_inGameSceneName);
            //fade
            //_fadeManager.StartFadeIn();
            //InGame�V�[���Ŏg��������L��������
            _inGame = true;�@//�C���Q�[���t���O��L����
            _startGame = true;�@//�Q�[�����X�^�[�g�����t���O��L����
        }
    }
}
