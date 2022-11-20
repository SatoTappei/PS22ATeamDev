using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField, Header("Wave”•ª‚Ì“G‚ÌƒvƒŒƒnƒu")] List<GameObject> _spawners;

    private void Start()
    {
        foreach (GameObject spawner in _spawners) 
        {
            spawner.SetActive(false);
        }
    }

    public void EnemySpawn1() 
    {
        _spawners[0].SetActive(true);
    }

    public void EnemySpawn2()
    {

        _spawners[1].SetActive(true);
    }
    public void EnemySpawn3()
    {

        _spawners[2].SetActive(true);
    }
}
