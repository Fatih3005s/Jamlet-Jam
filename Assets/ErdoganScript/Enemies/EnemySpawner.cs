using System.Collections;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] private GameObject[] Enemies;

    [Header("Spawner")]
    [SerializeField] private float spawnSpeedFloat1;
    [SerializeField] private float spawnSpeedFloat2;
    private float spawnSpeed;

    [Header("Timer")]
    private float _timer;
  
    private int randomEnemy;

    private bool canSpawn = true;

    void Start()
    {
        
    }

    void Update()
    {
        if(canSpawn)
            StartCoroutine(SpawnEnemy());

    }

    IEnumerator SpawnEnemy()
    {
        canSpawn = false;

        spawnSpeed = Random.Range(spawnSpeedFloat1, spawnSpeedFloat2);

        getNewRandomFloat();

        randomEnemy = Random.Range(0,3);

        Instantiate(Enemies[randomEnemy], transform.position, Quaternion.identity);

        yield return new WaitForSeconds(spawnSpeed);

        canSpawn = true;
    }

    void getNewRandomFloat()
    {
        float minusFloat = spawnSpeed * 0.003f;

        spawnSpeedFloat1 -= minusFloat;
        spawnSpeedFloat2 -= minusFloat;
    }
}
