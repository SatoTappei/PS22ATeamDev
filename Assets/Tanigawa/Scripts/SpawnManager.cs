using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField, Header("Wave”•ª‚Ì“G‚ÌƒvƒŒƒnƒu")] List<GameObject> _spawners;
    //[SerializeField] Transform _spawnPos;


    void Start()
    {
        foreach (GameObject spawner in _spawners) 
        {
            spawner.SetActive(false);
        }
    }

    public void EnemySpawn1(GameObject spawn) 
    {
        _spawners[0].SetActive(true);
        //Instantiate(spawn, _spawnPos);
    }
    public void EnemySpawn2(GameObject spawn)
    {

        _spawners[1].SetActive(true);
        //Instantiate(spawn, _spawnPos);
    }
    public void EnemySpawn3(GameObject spawn)
    {

        _spawners[2].SetActive(true);
        //Instantiate(spawn, _spawnPos);
    }
}
