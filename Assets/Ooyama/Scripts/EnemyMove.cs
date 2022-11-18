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
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(_targetPlayer)
        {
            //�v���C���[�ƃG�l�~�[�̃|�W�V��������velocity�����Z
            _playerpos = _targetPlayer.transform.position;
            _rb.velocity = new Vector3(_playerpos.x - transform.position.x,0, _playerpos.z - transform.position.z);
        }
    }
    //�v���[���[�ɓ����������̏���
    void HitPlayer()
    {
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        _playersRb = _player.GetComponent<Rigidbody>();
        _playersRb.AddForce(_enemysPushPower,0,_enemysPushPower,ForceMode.Impulse);
    }
    //�v���[���[�Ƃ̓����蔻��
    private void OnCollisionEnter(Collision collision)
    {
        if(gameObject.tag==_playerTag)
        {
            HitPlayer();
        }
    }
}
