using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //シングルトン用
    static GameManager _instance;

    //参照
    FadeManager _fadeManager;
    UIManager _uIManager;

    //キャラクターのオブジェクト
    GameObject _player; //プレイヤー
    GameObject[] _enemys;   //敵の配列

    //沸かす敵関連
    [SerializeField, Header("Wave数分の敵の組み合わせのプレハブ")] 
    List<GameObject> _spawners;
    [SerializeField] 
    Transform _spawnPos;   //敵の沸く位置

    //キャラクターを消すためのy座標の限界座標
    [SerializeField,Header("キャラクター落下判定範囲")]
    float _yRange;

    //フラグ関連
    bool _wave1;
    bool _wave2;
    bool _wave3;
    [SerializeField, Header("ゲームが開始されたフラグ")]
    bool _startGame;　//ゲームの開始を判定するフラグ
    [SerializeField, Header("インゲームフラグ")] 
    bool _inGame;　//inGameフラグ
    bool _gameClear;//ゲームのクリアを判定するフラグ
    bool _gameOver;//ゲームの終了を判定するフラグ

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

    }

    void Update()
    {
        //TitleシーンからInGameシーンへ遷移する関数を呼び出している。
        GameStart();

        //TitleシーンからInGameシーンへ遷移したときに１度だけ呼び出される
        OnlyOnceMethod();

        //InGameにいる間実行される
        if (_inGame)
        {
            //キャラクターの処理
            PlayerKill();//playerが死んだときの処理
            //EnemyKill();
            
            //Wave関連
            WaveChange();

            //クリアしたときの処理
            GameClear();

            //UI操作　それぞれの数値をUIに更新していく
            _uIManager.OutputNowWave();//現在のWave出力
            _uIManager.OutputRemainingWave();//残りのWave出力
            _uIManager.OutputEnemyCount();//敵の数出力
        }

    }

    //update内で１回だけ処理したい関数
    void OnlyOnceMethod()
    {
        if (_startGame && _inGame)
        {
            _player = GameObject.FindWithTag("Player");　//プレイヤー取得
            _uIManager = GameObject.Find("MainUI").GetComponent<UIManager>();
            _fadeManager = GetComponent<FadeManager>();　//FadeManager取得
            _startGame = false;
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
        Instantiate(spawn, _spawnPos);
        /*
        //敵の配列を取得
        _enemys = GameObject.FindGameObjectsWithTag("Enemy");
        */
    }

    //プレイヤーが落ちた時のオブジェクト消去とシーン遷移を行う関数
    void PlayerKill()
    {
        if (_player != null &&_player.transform.position.y < _yRange)
        {
            Destroy(_player);   //プレイヤーのkill

            //ゲームオーバーの時の処理
            GameOver();

        }
    }
    /*
    //敵が落ちた時のオブジェクト消去のための関数
    void EnemyKill()
    {
        //敵が落ちたかの判定ここに書く
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
            //_fadeManager.StartFadeOut("");  //fadeする
            //クリア時のロゴ？を出す処理をここに書く



            ////シーンのロード
            if (Input.GetKeyDown(KeyCode.Space) && _gameClear) //Spaceキーを押したら
            {
                SceneManager.LoadScene(_titleSceneName);
                

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
        //_fadeManager.StartFadeOut("");  //fadeする
        //クリア時のロゴ？を出す処理をここに書く



        //シーンのロード
        if (Input.GetKeyDown(KeyCode.Space) && _gameOver) //Spaceキーを押したら
        {
            SceneManager.LoadScene(_titleSceneName);
        }
    }

    //titleシーンからfadeしながらInGameシーンへ遷移する処理
    void GameStart() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_inGame && !_startGame && !_gameClear && !_gameOver) //Spaceキーを押したら
        {
            //シーンのロード
            SceneManager.LoadScene(_inGameSceneName);
            //fade
            //_fadeManager.StartFadeIn();
            //InGameシーンで使う処理を有効化する
            _inGame = true;　//インゲームフラグを有効化
            _startGame = true;　//ゲームがスタートしたフラグを有効化
        }
    }
}
