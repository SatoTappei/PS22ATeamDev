using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    [SerializeField] float _rotationalSpeed = 1f;
    [SerializeField] string _playeTag = "Player";
    private void Update()
    {
        transform.Rotate(new Vector3(0,_rotationalSpeed,0));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag==_playeTag)
        {
            PlayerMove _pm = collision.gameObject.GetComponent<PlayerMove>();
            _pm._pushPower += _pm._pushPowerUp;
            SoundManager._instance.Play("SE_アイテム獲得");
            Destroy(this.gameObject);
        }
    }
}
