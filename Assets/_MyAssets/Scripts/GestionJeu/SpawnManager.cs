using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _prefabEnemy = default;
    [SerializeField] private GameObject _enemyContainer = default;
    private bool _stopSpawn = false;
    private UIManager _uiManager;

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine()); 
        _uiManager = FindObjectOfType<UIManager>();
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        while(!_stopSpawn)
        {
            Vector3 poSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 7f, 0f);
            if( _uiManager._score < 1000)
            {
                GameObject newEnemy = Instantiate(_prefabEnemy, poSpawn, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(5f);
            }
            else
            {
                GameObject newEnemy = Instantiate(_prefabEnemy, poSpawn, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(3f);
            }
            
        }  
    }

    
    public void FinJeu()
    {
        _stopSpawn = true;
    }
}
