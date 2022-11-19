using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]


public class EnemyMove : MonoBehaviour
{
    bool _canmove=true;
    Rigidbody _rb;
    Rigidbody _playersRb;
    [SerializeField]GameObject _targetPlayer;
    [SerializeField] string _playerTag = "Player";
    [SerializeField] float _enemysPushPower = 3f;
    [SerializeField] float _waitTimer = 3f;
    [SerializeField] float _movePower=3f;
    [SerializeField] float _upperPower=0f;
    Vector3 _playerpos;
    Vector3 _forceDir;
    Vector3 _veloDir;
    void Start()
    {
        if(GameObject.FindGameObjectWithTag(_playerTag))
        {
            _targetPlayer = GameObject.FindGameObjectWithTag(_playerTag);
        }      
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(_targetPlayer && _canmove)
        {
            //プレイヤーとエネミーのポジションからvelocityを演算
            _playerpos = _targetPlayer.transform.position;
            _veloDir= new Vector3(_playerpos.x - transform.position.x,
            _playerpos.y - transform.position.y, _playerpos.z - transform.position.z).normalized;
            _rb.velocity = new Vector3(_veloDir.x*_movePower,_veloDir.y*_movePower, _veloDir.z*_movePower);
        }
    }
    //プレーヤーとの当たり判定
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag==_playerTag)
        {
            //プレーヤーに当たった時の処理
            _canmove = false;
            _playerpos = collision.gameObject.transform.position;
            _forceDir = (_playerpos - transform.position).normalized;
            _playersRb = collision.gameObject.GetComponent<Rigidbody>();
            _playersRb.AddForce(_forceDir.x*_enemysPushPower, _upperPower, _forceDir.z*_enemysPushPower, ForceMode.Impulse);
            StartCoroutine(RestartMove());
        }
    }
    IEnumerator RestartMove()
    {
        yield return new WaitForSeconds(_waitTimer);
        _canmove = true;
    }
}
