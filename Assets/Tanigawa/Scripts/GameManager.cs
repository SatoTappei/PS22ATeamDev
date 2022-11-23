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

    //�L�����N�^�[�̃I�u�W�F�N�g
    GameObject _player; //�v���C���[
    
    //�������G�֘A
    [SerializeField, Header("Wave�����̓G�̑g�ݍ��킹�̃v���n�u")] 
    List<GameObject> _spawners;

    //�L�����N�^�[���������߂�y���W�̌��E���W
    [SerializeField,Header("�L�����N�^�[��������͈�")]
    float _yRange;

    //�t���O�֘A
    bool _wave1;    //Wave1�̃t���O
    bool _wave2;    //Wave2�̃t���O
    bool _wave3;    //Wave3�̃t���O 
    bool _inGame;�@      //inGame�t���O
    bool _gameClear;    //�Q�[���̃N���A�𔻒肷��t���O
    bool _gameOver;     //�Q�[���̏I���𔻒肷��t���O
    [SerializeField, Tooltip("InGame�V�[���Ńe�X�g�������ꍇ�̓`�F�b�N�����Ă��������B"),Header("�f�o�b�O�p�t���O")]
    bool _debugMode;

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
        //�C�x���g�Ƀ��b�\�h�ǉ�
        SceneManager.sceneLoaded += OnlyOnceMethod;
    }

    void Update()
    {
        //Title�V�[������InGame�V�[���֑J�ڂ���֐����Ăяo���Ă���B
        GameStart();

        //InGame�ɂ���Ԏ��s�����
        if (_inGame)
        {
            //�L�����N�^�[�̏���
            PlayerKill();//player�����񂾂Ƃ��̏���
            
            //Wave�֘A
            WaveChange();

            //�N���A�����Ƃ��̏���
            GameClear();

            //UI����@���ꂼ��̐��l��UI�ɍX�V���Ă���
            _uIManager.OutputNowWave();//���݂�Wave�o��
            _uIManager.OutputRemainingWave();//�c���Wave�o��
            _uIManager.OutputEnemyCount();//�G�̐��o��
        }
        
        //InGame�V�[������e�X�g���������̃t���O��On�ɂȂ������ɍs���鏈���@�i�Q�[���ɂ͊֌W�Ȃ������j
        if (_debugMode) 
        {
            Debug.Log("OnlyOnceMethod�����s���ꂽ");
            _player = GameObject.FindWithTag("Player");                             //�v���C���[�擾
            _uIManager = GameObject.Find("MainUI").GetComponent<UIManager>();       //UIManager�擾
            _fadeManager = GameObject.Find("MainUI").GetComponent<FadeManager>();  //FadeManager�擾
            _fadeManager.StartFadeIn();//fadein����
            _inGame = true; //�C���Q�[���t���O��L����
            _debugMode = false;
        }
        
    }

    //Title�V�[������InGame�V�[���֑J�ڂ����Ƃ��ɂP�x�����Ăяo�����֐�
    void OnlyOnceMethod(Scene nextScene, LoadSceneMode mode)
    {
        Debug.Log("OnlyOnceMethod�����s���ꂽ");
        _player = GameObject.FindWithTag("Player");                             //�v���C���[�擾
        _uIManager = GameObject.Find("MainUI").GetComponent<UIManager>();       //UIManager�擾
        _fadeManager = GameObject.Find("MainUI").GetComponent<FadeManager>();  //FadeManager�擾
        _fadeManager.StartFadeIn();//fadein����
        _inGame = true; //�C���Q�[���t���O��L����
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
        Instantiate(spawn);
    }

    //�v���C���[�����������̃I�u�W�F�N�g�����ƃV�[���J�ڂ��s���֐�
    void PlayerKill()
    {
        if (_player != null &&_player.transform.position.y < _yRange)
        {
            //�v���C���[��kill
            Destroy(_player);
            //�Q�[���I�[�o�[�̎��̏���
            GameOver();
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
            //�N���A���̃��S�H���o�������������ɏ���



            ////�V�[���̃��[�h
            if (Input.GetKeyDown(KeyCode.Space) && _gameClear) //Space�L�[����������
            {
                //�t�F�[�h�A�E�g���ă^�C�g���V�[�������[�h
                _fadeManager.StartFadeOut(_titleSceneName);  
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
        //�N���A���̃��S�H���o�������������ɏ���



        //�V�[���̃��[�h
        if (Input.GetKeyDown(KeyCode.Space) && _gameOver) //Space�L�[����������
        {
            //�t�F�[�h�A�E�g���ă^�C�g���V�[�������[�h
            _fadeManager.StartFadeOut(_titleSceneName);
        }
    }

    //title�V�[������fade���Ȃ���InGame�V�[���֑J�ڂ��鏈��
    void GameStart() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_inGame && !_gameClear && !_gameOver) //Space�L�[����������
        {
            //�V�[���̃��[�h
            SceneManager.LoadScene(_inGameSceneName);
        }
    }
}
