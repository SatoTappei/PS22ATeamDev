using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject _player;
    UIManager _uIManager;
    [SerializeField,Header("�L�����N�^�[��������͈�")]�@float _deathRange;
    
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
