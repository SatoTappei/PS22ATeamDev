using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject _player;
    GameObject _enemy;
    UIManager _uIManager;
    FadeManager _fadeManager;
    SpawnManager _spawnManager;

    [SerializeField,Header("�L�����N�^�[��������͈�")]�@float _deathRange;
    
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _uIManager = GameObject.Find("MainUI").GetComponent<UIManager>();
        _fadeManager = GetComponent<FadeManager>();
        _spawnManager = GetComponent<SpawnManager>();
    }

    void Update()
    {
        //���ꂼ��̐��l��UI�ɍX�V���Ă���
        _uIManager.OutputNowWave();//���݂�Wave�o��
        _uIManager.OutputRemainingWave();//�c���Wave�o��
        _uIManager.OutputEnemyCount();//�G�̐��o��

        GameOver();//player�����񂾂Ƃ��̏���
        WaveChange();//
    }

    void WaveChange()
    {
        if (_uIManager.EnemyCount() == 0)
        {

        }
    }

    void GameOver()
    {
        if (_player.transform.position.y < _deathRange)
        {
            Destroy(_player);   //�v���C���[��kill

            //�Q�[���I�[�o�[�̏�����`��


        }
    }
}
