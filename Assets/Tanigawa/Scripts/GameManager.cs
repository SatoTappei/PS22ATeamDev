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
    //SpawnManager _spawnManager;

    //プレイヤーのオブジェクト
    GameObject _player;
    GameObject[] _enemys;//敵の配列
    [SerializeField, Header("Wave数分の敵の組み合わせのプレハブ")] List<GameObject> _spawners;
    [SerializeField] Transform _spawnPos;   //敵の沸く位置
    //キャラクターを消すためのy座標の限界座標
    [SerializeField,Header("キャラクター落下判定範囲")]　float _yRange;
    //inGameフラグ
    [SerializeField, Header("インゲームフラグ")] bool _inGame;
     bool _gameClear;
     bool _gameOver;

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
            //UI操作　それぞれの数値をUIに更新していく
            _uIManager.OutputNowWave();//現在のWave出力
            _uIManager.OutputRemainingWave();//残りのWave出力
            _uIManager.OutputEnemyCount();//敵の数出力

            //キャラクターの処理
            PlayerKill();//playerが死んだときの処理
            EnemyKill();//敵が死んだときの処理

            //Wave部関連
            WaveChange();//

            //クリアしたときの処理
            GameClear();
        }
        
    }

    //敵の生成を管理する関数
    void WaveChange()
    {
        //Waveごとに違う処理をする
        if (_uIManager.NowWave == 1)//Wave1のとき 
        {
            //Wave1の敵を生成する処理を描く
            //_spawnManager.EnemySpawn1();
            EnemySpawn(_spawners[0]);
            //敵の配列を取得
            _enemys = GameObject.FindGameObjectsWithTag("Enemy");
        }
        if (_uIManager.NowWave == 2)//Wave2のとき 
        {
            //Wave2の敵を生成する処理を描く
            //_spawnManager.EnemySpawn2();
            EnemySpawn(_spawners[1]);
            //敵の配列を取得
            _enemys = GameObject.FindGameObjectsWithTag("Enemy");
        }
        if (_uIManager.NowWave == 3)//Wave3のとき 
        {
            //Wave3の敵を生成する処理を描く
            //_spawnManager.EnemySpawn3();
            EnemySpawn(_spawners[2]);
            //敵の配列を取得
            _enemys = GameObject.FindGameObjectsWithTag("Enemy");
        }
    }

    //
    void EnemySpawn(GameObject spawn)
    {
        Instantiate(spawn, _spawnPos);
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

    //敵が落ちた時のオブジェクト消去のための関数
    void EnemyKill()
    {
        //敵が落ちたかの判定ここに書く
        foreach (GameObject enemy in _enemys) 
        {
            if (enemy.transform.position.y < _yRange) 
            {
                Destroy(enemy);
            }
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
            //_fadeManager.StartFadeOut("");  //fadeする
            //クリア時のロゴ？を出す処理をここに書く



            ////シーンのロード
            if (Input.GetKeyDown(KeyCode.Space) && _gameClear) //Spaceキーを押したら
            {
                SceneManager.LoadScene("Title1");
                

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
            SceneManager.LoadScene("Title1");
            


        }


    }

    //titleからゲームをスタートする
    void GameStart() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_inGame && !_gameClear && !_gameOver) //Spaceキーを押したら
        {
            //シーンのロード
            SceneManager.LoadScene("InGame1");
            //fade
            //_fadeManager.StartFadeIn();
            //InGameシーンで使う処理を有効化する
            _inGame = true;
        }
    }
}
