using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _prefabEnemy = default;
    [SerializeField] private GameObject _enemyContainer = default;
    [SerializeField] private GameObject[] _listePUPrefabs = default;
    private bool _stopSpawn = false;

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine()); 
        StartCoroutine(SpawnPURoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while(!_stopSpawn)
        {
            Vector3 poSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 7f, 0f);
            GameObject newEnemy = Instantiate(_prefabEnemy, poSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(2.0f);
        }
    }

    IEnumerator SpawnPURoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while(!_stopSpawn)
        {
            Vector3 poSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 7f, 0f);
            int randomPU = Random.Range(0, _listePUPrefabs.Length);
            GameObject newPU = Instantiate(_listePUPrefabs[randomPU], poSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(8.0f,12.0f));
        }
    }

    public void FinJeu()
    {
        _stopSpawn = true;
    }
}
