using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusiqueFond : MonoBehaviour
{
     private void Awake()
    {
        int nbMusiquedeFond = FindObjectsOfType<MusiqueFond>().Length;
        if (nbMusiquedeFond > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}
