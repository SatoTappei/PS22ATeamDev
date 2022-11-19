using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject _player;
    UIManager _uIManager;
    [SerializeField,Header("キャラクター落下判定範囲")]　float _deathRange;
    
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _uIManager = GameObject.Find("MainUI").GetComponent<UIManager>();
    }

    void Update()
    {
        if (_player.transform.position.y < _deathRange) 
        {
            Destroy(_player);
        }
    }
}
