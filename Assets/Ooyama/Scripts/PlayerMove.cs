using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class PlayerMove : MonoBehaviour
{
    bool _gameover;
    GameObject _enemy;
    Rigidbody _playerRb;
    Rigidbody _enemyRb;
    [SerializeField] float _pushPower = 5f;
    [SerializeField] float _upperPower = 0f;
    [SerializeField] string _enemyTag = "Enemy";
    Vector3 _enemyPos;
    Vector3 _forceDir;
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(_gameover==true)
        {
            GameOver();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag==_enemyTag)
        {
            //ìGÇ∆è’ìÀÇµÇΩéûÇÃèàóù
            _enemy = collision.gameObject;
            _enemyPos = collision.gameObject.transform.position;
            _forceDir = (_enemyPos - transform.position).normalized;
            _enemyRb = _enemy.GetComponent<Rigidbody>();
            _enemyRb.AddForce(_forceDir.x*_pushPower, _upperPower,_forceDir.z *_pushPower, ForceMode.Impulse);
        }
    }
    void GameOver()
    {

    }
}
