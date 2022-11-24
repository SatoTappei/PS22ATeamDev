using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    //�V���O���g���p
    static GameManager _instance;

    //�Q��
    FadeManager _fadeManager;
    UIManager _uIManager;
    GameObject _uIObj;

    //�L�����N�^�[�̃I�u�W�F�N�g
    GameObject _player; //�v���C���[
    
    //�������G�֘A
    [SerializeField, Header("Wave�����̓G�̑g�ݍ��킹�̃v���n�u")] 
    List<GameObject> _spawners;

    //�L�����N�^�[���������߂�y���W�̌��E���W
    [SerializeField,Header("�L�����N�^�[��������͈�")]
    float _yRange;

    //�t���O�֘A
    bool _wave1;        //Wave1�̃t���O
    bool _wave2;        //Wave2�̃t���O
    bool _wave3;        //Wave3�̃t���O 
    bool _inGame;�@     //inGame�t���O
    bool _gameOver;     //GameOver�t���O
    bool _gameClear;    //GameClear�t���O

    //�f�o�b�O�p
    [SerializeField, Tooltip("InGame�V�[���Ńe�X�g�������ꍇ�̓`�F�b�N�����Ă��������B"),Header("�f�o�b�O�p�t���O")]
    bool _debugMode;

    //�V�[���̖��O�ύX�̂��߂̕ϐ�
    [SerializeField,Header("�^�C�g���V�[���̖��O")] string _titleSceneName = "Title";
    [SerializeField, Header("�Q�[���V�[���̖��O")] string _inGameSceneName = "InGame";

    //GameOver��GameClear�̃V�[���̃I�u�W�F�N�g�̃v���n�u
    [SerializeField, Header("GameClear�̃L�����o�X�̃v���n�u")] GameObject _gameClearCanvas;
    [SerializeField, Header("GameOver�̃L�����o�X�̃v���n�u")] GameObject _gameOverCanvas;

    //Menu�̃{�^��
    Button _restartButton;
    Button _quitButton;
    //GameOver��GameClear�̃V�[���̃{�^��
    Button _titleButton;
    Button _retryButton;

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
        //�C�x���g�Ƀ��b�\�h��o�^
        SceneManager.sceneLoaded += OnlyOnceMethod;

        //�t���O�̏�����
        _inGame = false;
        _gameOver = false;
        _gameClear = false;
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
            _uIManager.OutputNowWave();                     //���݂�Wave�o��
            _uIManager.OutputRemainingWave();               //�c���Wave�o��
            _uIManager.OutputEnemyCount();                  //�G�̐��o��
        }

        //InGame�V�[������e�X�g���������̃t���O��On�ɂȂ������ɍs���鏈���@�i�Q�[���ɂ͊֌W�Ȃ������j
        OnlyDebug();

    }

    //Title�V�[������InGame�V�[���֑J�ڂ����Ƃ��ɂP�x�����Ăяo�����֐�
    void OnlyOnceMethod(Scene nextScene, LoadSceneMode mode)
    {
        if (nextScene.name == _inGameSceneName) 
        {
            //Debug.Log("OnlyOnceMethod�����s���ꂽ");
            _player = GameObject.FindWithTag("Player");                                 //�v���C���[�擾
            _uIObj = GameObject.Find("MainUI");                                         //UI�̃Q�[���I�u�W�F�N�g�擾
            _uIManager = _uIObj.GetComponent<UIManager>();                              //UIManager�擾
            _fadeManager = _uIObj.GetComponent<FadeManager>();                          //FadeManager�擾
            _fadeManager.StartFadeIn();                                                 //fadein����
            _restartButton = GameObject.Find("RestartButton").GetComponent<Button>();   //RestartButtonr�擾
            _restartButton.onClick.AddListener(OnClickButton);                          //�{�^���ɃC�x���g���Z�b�g
            _quitButton = GameObject.Find("QuitButton").GetComponent<Button>();         //QuitButtonr�擾
            _quitButton.onClick.AddListener(OnQuitButton);                              //�{�^���ɃC�x���g���Z�b�g
            SoundManager._instance.Play("BGM�Q�[����");                                 // BGM�Đ�
            _inGame = true;                                                             //�C���Q�[���t���O��L����

        }

        if (nextScene.name == _titleSceneName) 
        {
            //�t���O�̏�����
            _inGame = false;
            _gameOver = false;
            _gameClear = false;
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
            //�Q�[���N���A�t���O�L����
            _gameOver = true;
            //�Q�[�����~�߂�
            _inGame = false;
            //BGM��~
            SoundManager._instance.FadeOutBGM();
            //�Q�[���N���A�̎��̏���
            Instantiate(_gameClearCanvas);
            //���������I�u�W�F�N�g����{�^����T���ĂƂ��Ă���
            _titleButton = GameObject.Find("TitleButton").GetComponent<Button>();
            //�{�^���ɃC�x���g���Z�b�g
            _titleButton.onClick.AddListener(OnClickButton);
        }
    }

    //GameOver �̎��̏�����`��
    void GameOver() 
    {
        //�Q�[���I�[�o�[�t���O�L����
        _gameOver = true;
        //�Q�[�����~�߂�
        _inGame = false;
        //BGM��~
        SoundManager._instance.FadeOutBGM();
        //�Q�[���I�[�o�[�̎��̏���
        Instantiate(_gameOverCanvas);
        //���������I�u�W�F�N�g����{�^����T���ĂƂ��Ă���
        _retryButton = GameObject.Find("RetryButton").GetComponent<Button>();
        //�{�^���ɃC�x���g���Z�b�g
        _retryButton.onClick.AddListener(OnClickButton);
    }

    //title�V�[������fade���Ȃ���InGame�V�[���֑J�ڂ��鏈��
    void GameStart() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_inGame�@&& !_gameOver && !_gameClear)       //Space�L�[����������
        {
            //�V�[���̃��[�h
            SceneManager.LoadScene(_inGameSceneName);
        }
    }

    //�Q�[�����I�����邽�߂̃{�^���̊֐�
    public void OnQuitButton()
    {
        //�Q�[�����I������
        Application.Quit();
    }

    //�^�C�g���ɖ߂�{�^���̏���
    public void OnClickButton()
    {
        //�^�C�g���V�[�������[�h����
        SceneManager.LoadScene(_titleSceneName);
        Destroy(gameObject);
    }

    //�f�o�b�O�p�̏���
    void OnlyDebug()
    {
        if (_debugMode�@&& SceneManager.GetActiveScene().name == _inGameSceneName)
        {
            Debug.Log("OnlyDebug�����s���ꂽ");
            _player = GameObject.FindWithTag("Player");                                 //�v���C���[�擾
            _uIObj = GameObject.Find("MainUI");                                         //UI�̃Q�[���I�u�W�F�N�g�擾
            _uIManager = _uIObj.GetComponent<UIManager>();                              //UIManager�擾
            _fadeManager = _uIObj.GetComponent<FadeManager>();                          //FadeManager�擾
            _fadeManager.StartFadeIn();                                                 //fadein����
            _restartButton = GameObject.Find("RestartButton").GetComponent<Button>();   //RestartButtonr�擾
            _restartButton.onClick.AddListener(OnClickButton);                          //�{�^���ɃC�x���g���Z�b�g
            _quitButton = GameObject.Find("QuitButton").GetComponent<Button>();         //QuitButtonr�擾
            _quitButton.onClick.AddListener(OnQuitButton);                              //�{�^���ɃC�x���g���Z�b�g
            _inGame = true;                                                             //�C���Q�[���t���O��L����
            _gameOver = false;                                                          //�Q�[���I�[�o�[�t���O������
            _gameClear = false;                                                         //�Q�[���N���A�t���O������
            _debugMode = false;                                                         //�f�o�b�O�p�t���O������
        }
    }
}
