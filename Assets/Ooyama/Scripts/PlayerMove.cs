using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class PlayerMove : MonoBehaviour
{
    bool _gameover;
    GameObject _enemy;
    Rigidbody _playerRb;
    Rigidbody _enemyRb;
    float _halfScale;
    [SerializeField] public float _pushPower = 5f;
    [SerializeField] float _upperPower = 0f;
    [SerializeField] public float _pushPowerUp = 2f;
    [SerializeField] string _enemyTag = "Enemy";
    [SerializeField] GameObject _particle;
    [SerializeField] float _plusPower = 3f;
    Vector3 _enemyPos;
    Vector3 _forceDir;
    float _pushXPower = 0;
    float _pushZPower = 0;
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _halfScale = transform.localScale.x / 2;
    }
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float _pushXPower = Input.GetAxis("Horizontal") * _plusPower;
            float _pushZPower = Input.GetAxis("Vertical") * _plusPower;
        }      
    }
    private void FixedUpdate()
    {
        _playerRb.velocity = new Vector3(_playerRb.velocity.x + _pushXPower, _playerRb.velocity.y, _playerRb.velocity.z + _pushZPower);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag==_enemyTag)
        {
            //ìGÇ∆è’ìÀÇµÇΩéûÇÃèàóù
            SoundManager._instance.Play("SE_è’ìÀ");
            _enemy = collision.gameObject;
            _enemyPos = collision.gameObject.transform.position;
            _forceDir = (_enemyPos - transform.position).normalized;
           GameObject _spawnParticle= Instantiate(_particle, new Vector3
                ((transform.position.x + _forceDir.x * _halfScale)
                , (transform.position.y + _forceDir.y * _halfScale)
                , (transform.position.z + _forceDir.z * _halfScale))
                , Quaternion.identity);
            _spawnParticle.transform.forward = _forceDir;
            _enemyRb = _enemy.GetComponent<Rigidbody>();
            _enemyRb.AddForce(_forceDir.x*_pushPower, _upperPower,_forceDir.z *_pushPower, ForceMode.Impulse);
        }
    }
}
