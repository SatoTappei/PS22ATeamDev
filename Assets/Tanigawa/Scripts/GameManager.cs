using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    //シングルトン用
    static GameManager _instance;

    //参照
    FadeManager _fadeManager;
    UIManager _uIManager;
    GameObject _uIObj;

    //キャラクターのオブジェクト
    GameObject _player; //プレイヤー
    
    //沸かす敵関連
    [SerializeField, Header("Wave数分の敵の組み合わせのプレハブ")] 
    List<GameObject> _spawners;

    //キャラクターを消すためのy座標の限界座標
    [SerializeField,Header("キャラクター落下判定範囲")]
    float _yRange;

    //フラグ関連
    bool _wave1;        //Wave1のフラグ
    bool _wave2;        //Wave2のフラグ
    bool _wave3;        //Wave3のフラグ 
    bool _inGame;　     //inGameフラグ
    bool _gameOver;     //GameOverフラグ
    bool _gameClear;    //GameClearフラグ

    //デバッグ用
    [SerializeField, Tooltip("InGameシーンでテストしたい場合はチェックをつけてください。"),Header("デバッグ用フラグ")]
    bool _debugMode;

    //シーンの名前変更のための変数
    [SerializeField,Header("タイトルシーンの名前")] string _titleSceneName = "Title";
    [SerializeField, Header("ゲームシーンの名前")] string _inGameSceneName = "InGame";

    //GameOverとGameClearのシーンのオブジェクトのプレハブ
    [SerializeField, Header("GameClearのキャンバスのプレハブ")] GameObject _gameClearCanvas;
    [SerializeField, Header("GameOverのキャンバスのプレハブ")] GameObject _gameOverCanvas;

    //Menuのボタン
    Button _restartButton;
    Button _quitButton;
    //GameOverとGameClearのシーンのボタン
    Button _titleButton;
    Button _retryButton;

    void Awake()
    {
        //シーン間でオブジェクト共有のための処理
        if ( _instance == null)//インスタンスがなかったら
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else //あったら消す
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        //イベントにメッソドを登録
        SceneManager.sceneLoaded += OnlyOnceMethod;

        //フラグの初期化
        _inGame = false;
        _gameOver = false;
        _gameClear = false;
    }

    void Update()
    {
        //TitleシーンからInGameシーンへ遷移する関数を呼び出している。
        GameStart();

        //InGameにいる間実行される
        if (_inGame)
        {
            //キャラクターの処理
            PlayerKill();//playerが死んだときの処理

            //Wave関連
            WaveChange();

            //クリアしたときの処理
            GameClear();

            //UI操作　それぞれの数値をUIに更新していく
            _uIManager.OutputNowWave();                     //現在のWave出力
            _uIManager.OutputRemainingWave();               //残りのWave出力
            _uIManager.OutputEnemyCount();                  //敵の数出力
        }

        //InGameシーンからテストしたい時のフラグがOnになった時に行われる処理　（ゲームには関係ない処理）
        OnlyDebug();

    }

    //TitleシーンからInGameシーンへ遷移したときに１度だけ呼び出される関数
    void OnlyOnceMethod(Scene nextScene, LoadSceneMode mode)
    {
        if (nextScene.name == _inGameSceneName) 
        {
            //Debug.Log("OnlyOnceMethodが実行された");
            _player = GameObject.FindWithTag("Player");                                 //プレイヤー取得
            _uIObj = GameObject.Find("MainUI");                                         //UIのゲームオブジェクト取得
            _uIManager = _uIObj.GetComponent<UIManager>();                              //UIManager取得
            _fadeManager = _uIObj.GetComponent<FadeManager>();                          //FadeManager取得
            _fadeManager.StartFadeIn();                                                 //fadeinする
            _restartButton = GameObject.Find("RestartButton").GetComponent<Button>();   //RestartButtonr取得
            _restartButton.onClick.AddListener(OnClickButton);                          //ボタンにイベントをセット
            _quitButton = GameObject.Find("QuitButton").GetComponent<Button>();         //QuitButtonr取得
            _quitButton.onClick.AddListener(OnQuitButton);                              //ボタンにイベントをセット
            SoundManager._instance.Play("BGMゲーム中");                                 // BGM再生
            _inGame = true;                                                             //インゲームフラグを有効化

        }

        if (nextScene.name == _titleSceneName) 
        {
            //フラグの初期化
            _inGame = false;
            _gameOver = false;
            _gameClear = false;
        }
        
    }

    //敵の生成を管理する関数
    void WaveChange()
    {
        //Waveごとに違う処理をする
        if (_uIManager.NowWave == 1 && !_wave1) //Wave1のとき 
        {
            //Wave1の敵を生成する処理を描く
            EnemySpawn(_spawners[0]);
            _wave1 = true;
        }
        if (_uIManager.NowWave == 2 && !_wave2)//Wave2のとき 
        {
            //Wave2の敵を生成する処理を描く
            EnemySpawn(_spawners[1]);
            _wave2 = true;
        }
        if (_uIManager.NowWave == 3 && !_wave3)//Wave3のとき 
        {
            //Wave3の敵を生成する処理を描く
            EnemySpawn(_spawners[2]);
            _wave3 = true;
        }
    }

    //指定した敵の組み合わせを生成する処理
    void EnemySpawn(GameObject spawn)
    {
        //敵生成
        Instantiate(spawn);
    }

    //プレイヤーが落ちた時のオブジェクト消去とシーン遷移を行う関数
    void PlayerKill()
    {
        if (_player != null &&_player.transform.position.y < _yRange)
        {
            //プレイヤーのkill
            Destroy(_player);
            //ゲームオーバーの時の処理
            GameOver();
        }
    }
    
    //GameClearの時の処理を描く
    void GameClear() 
    {
        if (_uIManager.NowWave == _uIManager._maxWave && _uIManager.EnemyCount() == 0) 
        {
            //ゲームクリアフラグ有効化
            _gameOver = true;
            //ゲームを止める
            _inGame = false;
            //BGM停止
            SoundManager._instance.FadeOutBGM();
            //ゲームクリアの時の処理
            Instantiate(_gameClearCanvas);
            //生成したオブジェクトからボタンを探してとってくる
            _titleButton = GameObject.Find("TitleButton").GetComponent<Button>();
            //ボタンにイベントをセット
            _titleButton.onClick.AddListener(OnClickButton);
        }
    }

    //GameOver の時の処理を描く
    void GameOver() 
    {
        //ゲームオーバーフラグ有効化
        _gameOver = true;
        //ゲームを止める
        _inGame = false;
        //BGM停止
        SoundManager._instance.FadeOutBGM();
        //ゲームオーバーの時の処理
        Instantiate(_gameOverCanvas);
        //生成したオブジェクトからボタンを探してとってくる
        _retryButton = GameObject.Find("RetryButton").GetComponent<Button>();
        //ボタンにイベントをセット
        _retryButton.onClick.AddListener(OnClickButton);
    }

    //titleシーンからfadeしながらInGameシーンへ遷移する処理
    void GameStart() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_inGame　&& !_gameOver && !_gameClear)       //Spaceキーを押したら
        {
            //シーンのロード
            SceneManager.LoadScene(_inGameSceneName);
        }
    }

    //ゲームを終了するためのボタンの関数
    public void OnQuitButton()
    {
        //ゲームを終了する
        Application.Quit();
    }

    //タイトルに戻るボタンの処理
    public void OnClickButton()
    {
        //タイトルシーンをロードする
        SceneManager.LoadScene(_titleSceneName);
        Destroy(gameObject);
    }

    //デバッグ用の処理
    void OnlyDebug()
    {
        if (_debugMode　&& SceneManager.GetActiveScene().name == _inGameSceneName)
        {
            Debug.Log("OnlyDebugが実行された");
            _player = GameObject.FindWithTag("Player");                                 //プレイヤー取得
            _uIObj = GameObject.Find("MainUI");                                         //UIのゲームオブジェクト取得
            _uIManager = _uIObj.GetComponent<UIManager>();                              //UIManager取得
            _fadeManager = _uIObj.GetComponent<FadeManager>();                          //FadeManager取得
            _fadeManager.StartFadeIn();                                                 //fadeinする
            _restartButton = GameObject.Find("RestartButton").GetComponent<Button>();   //RestartButtonr取得
            _restartButton.onClick.AddListener(OnClickButton);                          //ボタンにイベントをセット
            _quitButton = GameObject.Find("QuitButton").GetComponent<Button>();         //QuitButtonr取得
            _quitButton.onClick.AddListener(OnQuitButton);                              //ボタンにイベントをセット
            _inGame = true;                                                             //インゲームフラグを有効化
            _gameOver = false;                                                          //ゲームオーバーフラグ無効化
            _gameClear = false;                                                         //ゲームクリアフラグ無効化
            _debugMode = false;                                                         //デバッグ用フラグ無効化
        }
    }
}
