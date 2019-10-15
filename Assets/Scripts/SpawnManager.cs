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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnPowerup());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemies()
    {
        while (_player != null)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(enemyRespawnTime);
        }
    }

    IEnumerator SpawnPowerup()
    {
        while (_player != null)
        {
            Instantiate(powerups[Random.Range(0, powerups.Length)]);
            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
    }
}
