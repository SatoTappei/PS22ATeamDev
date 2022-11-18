using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody _rb;
    Rigidbody _playersRb;
    GameObject _player;
    [SerializeField]GameObject _targetPlayer;
    [SerializeField] string _playerTag = "Player";
    [SerializeField] float _enemysPushPower = 3f;
    Vector3 _playerpos;
    Vector3 _forcedir;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(_targetPlayer)
        {
            //プレイヤーとエネミーのポジションからvelocityを演算
            _playerpos = _targetPlayer.transform.position;
            _rb.velocity = new Vector3(_playerpos.x - transform.position.x,0, _playerpos.z - transform.position.z);
        }
    }
    //プレーヤーとの当たり判定
    private void OnCollisionEnter(Collision collision)
    {
        if(gameObject.tag==_playerTag)
        {
            //プレーヤーに当たった時の処理
            _playerpos = collision.gameObject.transform.position;
            _forcedir = (_playerpos - transform.position).normalized;
            _playersRb = _player.GetComponent<Rigidbody>();
            _playersRb.AddForce(_forcedir.x*_enemysPushPower, 0, _forcedir.z*_enemysPushPower, ForceMode.Impulse);
        }
    }
}
