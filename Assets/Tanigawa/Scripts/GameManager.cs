using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //参照
    UIManager _uIManager;
    FadeManager _fadeManager;
    SpawnManager _spawnManager;

    //プレイヤーのオブジェクト
    GameObject _player;
    //キャラクターを消すためのy座標の限界座標
    [SerializeField,Header("キャラクター落下判定範囲")]　float _yRange;

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
            _spawnManager.EnemySpawn1();
        }
        if (_uIManager.NowWave == 2)//Wave2のとき 
        {
            //Wave2の敵を生成する処理を描く
            _spawnManager.EnemySpawn2();
        }
        if (_uIManager.NowWave == 3)//Wave3のとき 
        {
            //Wave3の敵を生成する処理を描く
            _spawnManager.EnemySpawn3();
        }
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
        //敵の配列を取得
        GameObject[] _enemys = GameObject.FindGameObjectsWithTag("Enemy");
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
            //ゲームクリアの時の処理をここに書く


        }
    }

    //GameOver の時の処理を描く
    void GameOver() 
    {
        //ゲームを止める
        _inGame = false;
        //ゲームオーバーの時の処理をここに書く


    }
}
