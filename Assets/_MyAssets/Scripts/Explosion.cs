using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1f);
    }


    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 2f);
    }
}
