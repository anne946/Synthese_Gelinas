using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _fireRate = 0.1f;
    [SerializeField] private int _viesJoueur = 3;
    [SerializeField] private AudioClip _laserSound = default;
    [SerializeField] private GameObject _laserPrefab = default;
    [SerializeField] private GameObject _tripleLaserPrefab = default;
    [SerializeField] private GameObject _explosionPrefab = default;
    
    private float _canFire = -1f;
    private bool _isTripleActive = false;
    private float _cadenceInitiale;
    private GameObject _shield;
    private Animator _anim;

    void Start()
    {
        _cadenceInitiale = _fireRate;
        _shield = transform.GetChild(0).gameObject;
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
            AudioSource.PlayClipAtPoint(_laserSound, Camera.main.transform.position, 0.3f);
            if(_isTripleActive == false)
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0f, 1.1f, 0f), Quaternion.identity);
            }
            else
            {
                Instantiate(_tripleLaserPrefab, transform.position + new Vector3(0f, 3.5f, 0f), Quaternion.identity);
            }
        }
    }

    private void MouvementsJoueur()
    {
        float posHorizontal = Input.GetAxis("Horizontal");
        float posVertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(posHorizontal, posVertical, 0);
        transform.Translate(direction * Time.deltaTime * _speed);
        
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

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.1f, 0.5f), 0f);
        
        if (transform.position.x >= 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0f);
        }
        else if (transform.position.x <= -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0f);
        }
    }

    public void Damage()
    {
        if (!_shield.activeSelf)
        {
            _viesJoueur--;
            UIManager uiManager = FindObjectOfType<UIManager>();
            uiManager.ChangeLivesDisplayImage(_viesJoueur);
        }
        else
        {
            _shield.SetActive(false);
        }

        if (_viesJoueur < 1)
        {
            SpawnManager spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            spawnManager.FinJeu();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void PowerTripleShot()
    {
       _isTripleActive = true;
       StartCoroutine(Triple()); 
    }

    IEnumerator Triple()
    {
        yield return new WaitForSeconds(5f);
        _isTripleActive = false;
    }

    public void SpeedPowerUp()
    {
        float newCadence = _cadenceInitiale - 0.4f;
        if(newCadence <= 0)
        {
            newCadence = 0.01f;
        }
         _fireRate = newCadence;
         StartCoroutine(Cadence());   
    }

    IEnumerator Cadence()
    {
        yield return new WaitForSeconds(5f);
        _fireRate = _cadenceInitiale;   
    }

    public void ShieldPowerUp()
    {
        _shield.SetActive(true);  
    }
}
