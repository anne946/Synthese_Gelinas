using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private AudioClip _explosionSound = default;

    void Start()
    {
        AudioSource.PlayClipAtPoint(_explosionSound, Camera.main.transform.position, 0.3f);
        Destroy(gameObject, 1.5f);
    }


    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 2f);
    }
}
