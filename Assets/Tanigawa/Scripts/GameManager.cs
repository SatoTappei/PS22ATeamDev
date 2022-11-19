using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject _player;
    GameObject _enemy;
    UIManager _uIManager;
    FadeManager _fadeManager;
    SpawnManager _spawnManager;

    [SerializeField,Header("キャラクター落下判定範囲")]　float _deathRange;
    
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _uIManager = GameObject.Find("MainUI").GetComponent<UIManager>();
        _fadeManager = GetComponent<FadeManager>();
        _spawnManager = GetComponent<SpawnManager>();
    }

    void Update()
    {
        //それぞれの数値をUIに更新していく
        _uIManager.OutputNowWave();//現在のWave出力
        _uIManager.OutputRemainingWave();//残りのWave出力
        _uIManager.OutputEnemyCount();//敵の数出力

        GameOver();//playerが死んだときの処理
        WaveChange();//
    }

    void WaveChange()
    {
        if (_uIManager.EnemyCount() == 0)
        {

        }
    }

    void GameOver()
    {
        if (_player.transform.position.y < _deathRange)
        {
            Destroy(_player);   //プレイヤーのkill

            //ゲームオーバーの処理を描く


        }
    }
}
