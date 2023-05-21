using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private int _points = 100;
    [SerializeField] private float _fireRate = 32f;
    [SerializeField] private GameObject _explosionPrefab = default;
    [SerializeField] private GameObject _FireballPrefab = default;
    private UIManager _uiManager;
    private Player _player;
    private float _canFire = -1f;

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
        transform.Translate(Vector3.left * Time.deltaTime * _speed);
        if(transform.position.x < -4.1f)
        {
            float randomX = Random.Range(-4.1f, 4.35f);
            transform.position = new Vector3(6f, randomX, 0f);
            Vector3 position = transform.position;

            if (Time.time > _canFire)
            {
                _canFire = Time.time + _fireRate;
                Instantiate(_FireballPrefab, position + new Vector3(-1.1f, 0f, 0f), Quaternion.identity);
            }   
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            UIManager uiManager = FindObjectOfType<UIManager>();
            uiManager.AjouterScore(_points);
            Destroy(collision.gameObject);
            DestructionEnemy();
        }
        if (collision.tag == "BallLightening")
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
