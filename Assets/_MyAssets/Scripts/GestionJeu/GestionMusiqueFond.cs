using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionMusiqueFond : MonoBehaviour
{
    private AudioSource _audioSource;
    private bool _pauseOn = false;

    private void Start()
    {
        _audioSource = FindObjectOfType<MusiqueFond>().GetComponent<AudioSource>();
    }

    public void FixedUpdate()
    {
        MusiqueOnOff();
    }

    public void MusiqueOnOff()
    {
        if(Input.GetKeyDown(KeyCode.F4) && !_pauseOn)  
        {
            _audioSource.Pause();
            _pauseOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.F4) && _pauseOn) 
        {
            _audioSource.Play();
            _pauseOn = false;
        }
    }
}
