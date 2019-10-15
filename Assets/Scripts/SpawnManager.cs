using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject _enemyPrefab;
    public GameObject _enemyContainer;
    public GameObject[] powerups;

    [SerializeField]
    private GameObject _player;

    private float enemyRespawnTime = 5;
    private bool _spawning = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartSpawn()
    {
        if (_spawning)
        {
            return;
        }

        _spawning = true;
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnPowerup());
    }
    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(3f);
        while (_player != null)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(enemyRespawnTime);
        }
    }

    IEnumerator SpawnPowerup()
    {
        yield return new WaitForSeconds(3f);
        while (_player != null)
        {
            Instantiate(powerups[Random.Range(0, powerups.Length)]);
            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
    }
}
