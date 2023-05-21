using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private int _powerUpID = default;
    //[SerializeField] private AudioClip _powerUpSound = default;
    Player _playerPrefab;
        
    // Start is called before the first frame update
    void Start()    {
        _playerPrefab = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y <= -5.0f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            Player player = other.GetComponent<Player>();
            Destroy(this.gameObject);
            //AudioSource.PlayClipAtPoint(_powerUpSound, Camera.main.transform.position, 0.6f);
            if (player != null) {
                switch (_powerUpID) {
                    case 0:
                        player.PowerTripleShot();
                        break;
                    case 1:
                        player.SpeedPowerUp();
                        break;
                    case 2:
                        player.ShieldPowerUp();
                        break;
                }
            }
            
        }
    }
}
