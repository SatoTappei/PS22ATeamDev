using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //シングルトン用
    static GameManager _instance;

    //参照
    UIManager _uIManager;
    FadeManager _fadeManager;

    //キャラクターのオブジェクト
    GameObject _player; //プレイヤー
    
    //沸かす敵関連
    [SerializeField, Header("Wave数分の敵の組み合わせのプレハブ")] 
    List<GameObject> _spawners;

    //キャラクターを消すためのy座標の限界座標
    [SerializeField,Header("キャラクター落下判定範囲")]
    float _yRange;

    //フラグ関連
    bool _wave1;    //Wave1のフラグ
    bool _wave2;    //Wave2のフラグ
    bool _wave3;    //Wave3のフラグ 
    bool _inGame;　      //inGameフラグ
    bool _gameClear;    //ゲームのクリアを判定するフラグ
    bool _gameOver;     //ゲームの終了を判定するフラグ
    [SerializeField, Tooltip("InGameシーンでテストしたい場合はチェックをつけてください。"),Header("デバッグ用フラグ")]
    bool _debugMode;

    //シーンの名前変更のための変数
    [SerializeField,Header("タイトルシーンの名前")] string _titleSceneName = "Title";
    [SerializeField, Header("ゲームシーンの名前")] string _inGameSceneName = "InGame";

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
        //イベントにメッソド追加
        SceneManager.sceneLoaded += OnlyOnceMethod;
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
            _uIManager.OutputNowWave();//現在のWave出力
            _uIManager.OutputRemainingWave();//残りのWave出力
            _uIManager.OutputEnemyCount();//敵の数出力
        }
        
        //InGameシーンからテストしたい時のフラグがOnになった時に行われる処理　（ゲームには関係ない処理）
        if (_debugMode) 
        {
            Debug.Log("OnlyOnceMethodが実行された");
            _player = GameObject.FindWithTag("Player");                             //プレイヤー取得
            _uIManager = GameObject.Find("MainUI").GetComponent<UIManager>();       //UIManager取得
            _fadeManager = GameObject.Find("MainUI").GetComponent<FadeManager>();  //FadeManager取得
            _fadeManager.StartFadeIn();//fadeinする
            _inGame = true; //インゲームフラグを有効化
            _debugMode = false;
        }
        
    }

    //TitleシーンからInGameシーンへ遷移したときに１度だけ呼び出される関数
    void OnlyOnceMethod(Scene nextScene, LoadSceneMode mode)
    {
        Debug.Log("OnlyOnceMethodが実行された");
        _player = GameObject.FindWithTag("Player");                             //プレイヤー取得
        _uIManager = GameObject.Find("MainUI").GetComponent<UIManager>();       //UIManager取得
        _fadeManager = GameObject.Find("MainUI").GetComponent<FadeManager>();  //FadeManager取得
        _fadeManager.StartFadeIn();//fadeinする
        _inGame = true; //インゲームフラグを有効化
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
            //ゲームを止める
            _inGame = false;
            //クリアフラグ
            _gameClear = true;
            //ゲームクリアの時の処理
            //クリア時のロゴ？を出す処理をここに書く



            ////シーンのロード
            if (Input.GetKeyDown(KeyCode.Space) && _gameClear) //Spaceキーを押したら
            {
                //フェードアウトしてタイトルシーンをロード
                _fadeManager.StartFadeOut(_titleSceneName);  
            }


        }
    }

    //GameOver の時の処理を描く
    void GameOver() 
    {
        //ゲームを止める
        _inGame = false;
        //ゲームオーバーフラグ
        _gameOver = true;
        //ゲームオーバーの時の処理
        //クリア時のロゴ？を出す処理をここに書く



        //シーンのロード
        if (Input.GetKeyDown(KeyCode.Space) && _gameOver) //Spaceキーを押したら
        {
            //フェードアウトしてタイトルシーンをロード
            _fadeManager.StartFadeOut(_titleSceneName);
        }
    }

    //titleシーンからfadeしながらInGameシーンへ遷移する処理
    void GameStart() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_inGame && !_gameClear && !_gameOver) //Spaceキーを押したら
        {
            //シーンのロード
            SceneManager.LoadScene(_inGameSceneName);
        }
    }
}
