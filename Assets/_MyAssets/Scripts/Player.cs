using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speed = 10;
    [SerializeField] private float _fireRate = 0.1f;
    [SerializeField] private int _viesJoueur = 3;
    [SerializeField] private GameObject _BulletPrefab = default;
    [SerializeField] private GameObject _BallLighteningPrefab = default;
    [SerializeField] private GameObject _explosionPrefab = default;
    
    private float _canFire = -1f;
    private float _cadenceInitiale;
    private Animator _anim;
    private GestionScene _gestionscene;
    private UIManager _uimanager;

    void Start()
    {
        _cadenceInitiale = _fireRate;
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        MouvementsJoueur();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            Instantiate(_BulletPrefab, transform.position + new Vector3(1.1f, 0f, 0f), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            Instantiate(_BallLighteningPrefab, transform.position + new Vector3(1.1f, 0f, 0f), Quaternion.identity);
        }
    }

    private void MouvementsJoueur()
    {
        float posHorizontal = Input.GetAxis("Horizontal");
        float posVertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(posHorizontal, posVertical, 0);
        transform.Translate(direction * Time.deltaTime * _speed);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -6f, -0.40f),transform.position.y, 0f);
        
        if (posHorizontal < 0f)
        {
            _anim.SetBool("TurnLeft", true);
            _anim.SetBool("TurnRight", false);
        }
        else if (posHorizontal > 0f)
        {
            _anim.SetBool("TurnRight", true);
            _anim.SetBool("TurnLeft", false);
        }
        else
        {
            _anim.SetBool("TurnRight", false);
            _anim.SetBool("TurnLeft", false);
        }

        if (transform.position.y >= 4.45f)
        {
            transform.position = new Vector3(transform.position.x, -4.1f, 0f);
        }
        else if (transform.position.y <= -4.1f)
        {
            transform.position = new Vector3(transform.position.x, 4.45f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fireball")
        {
            Damage();
        }
    }

    public void Damage()
    {
        _viesJoueur--;
        UIManager uiManager = FindObjectOfType<UIManager>();

        if (_viesJoueur < 1)
        {
            SpawnManager spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            spawnManager.FinJeu();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            _gestionscene.ChangerSceneSuivante();
        }
    }
}
