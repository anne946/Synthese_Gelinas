using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLightening : MonoBehaviour
{
    [SerializeField] private float _speed = 13f;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * _speed);
        if (transform.position.x > 6f)
        {
            Destroy(gameObject);
        }
    }
}
