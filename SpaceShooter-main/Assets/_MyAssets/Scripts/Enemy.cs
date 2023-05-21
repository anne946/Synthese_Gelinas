using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private int _points = 100;
    [SerializeField] private GameObject _explosionPrefab = default;
    private UIManager _uiManager;
    private Player _player;

    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    void Update()
    {
       MouvementsEnemy(); 
    }

    private void MouvementsEnemy()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if(transform.position.y < -6f)
        {
            float randomX = Random.Range(-8.5f, 8.5f);
            transform.position = new Vector3(randomX, 6f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            UIManager uiManager = FindObjectOfType<UIManager>();
            uiManager.AjouterScore(_points);
            Destroy(collision.gameObject);
            DestructionEnemy();
        }
        else if (collision.tag == "Player")
        {
            _player.Damage();
            DestructionEnemy();
        }
    }

    private void DestructionEnemy()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
