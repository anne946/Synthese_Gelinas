using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _prefabEnemy = default;
    [SerializeField] private GameObject _enemyContainer = default;
    private bool _stopSpawn = false;

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine()); 
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
    
    public void FinJeu()
    {
        _stopSpawn = true;
    }
}
