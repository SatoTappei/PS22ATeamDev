using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField, Header("Spawner�̃I�u�W�F�N�g")] GameObject _spwaner;
    [SerializeField, Header("Wave�����̓G�̃v���n�u")] List<GameObject> _enemyPrefabs;
    Transform _spawnPos;

    private void Start()
    {
        _spawnPos = _spwaner.transform;
    }

    public void EnemySpawn1() 
    {
        Instantiate(_enemyPrefabs[0], _spawnPos);
    }

    public void EnemySpawn2()
    {

        Instantiate(_enemyPrefabs[1], _spawnPos);
    }
    public void EnemySpawn3()
    {

        Instantiate(_enemyPrefabs[2], _spawnPos);
    }
}
