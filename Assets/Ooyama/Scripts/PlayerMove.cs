using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    bool _gameover;
    Rigidbody _PlayerRb;
    [SerializeField] float _pushpower = 5f;
    [SerializeField] string _enemytag = "Enemy";
    GameObject Player;
    void Start()
    {
        _PlayerRb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(_gameover==true)
        {
            GameOver();
        }
    }
    void GameOver()
    {

    }
    void HitEnemy()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag==_enemytag)
        {
            HitEnemy();
        }
    }
}
