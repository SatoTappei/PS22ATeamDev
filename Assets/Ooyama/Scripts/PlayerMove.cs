using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    bool _gameover;
    GameObject _enemy;
    Rigidbody _playerRb;
    Rigidbody _enemyRb;
    [SerializeField] float _pushpower = 5f;
    [SerializeField] string _enemytag = "Enemy";
    Vector3 _enemypos;
    Vector3 _forcedir;
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
        if(collision.gameObject.tag==_enemytag)
        {
            //ìGÇ∆è’ìÀÇµÇΩéûÇÃèàóù
            _enemy = collision.gameObject;
            _enemypos = collision.gameObject.transform.position;
            _forcedir = (_enemypos - transform.position).normalized;
            _enemyRb = _enemy.GetComponent<Rigidbody>();
            _enemyRb.AddForce(_forcedir.x*_pushpower, 0,_forcedir.z *_pushpower, ForceMode.Impulse);
        }
    }
    void GameOver()
    {

    }
}
