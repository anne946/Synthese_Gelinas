using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour  {
    
    [SerializeField] private int _score = default;
    [SerializeField] private float _temps = default;
    [SerializeField] private int _vies = default;
    [SerializeField] private TextMeshProUGUI _txtScore = default;
    [SerializeField] private TextMeshProUGUI _txtTemps = default;
    [SerializeField] private TextMeshProUGUI _txtVies = default;
    [SerializeField] private GameObject _pausePanel = default;
    private bool _pauseOn = false;
    
    // Start is called before the first frame update

    private void Start() {
        _score = 0;
        _temps = Time.time;
        _vies = 3;
        Time.timeScale = 1;
        UpdateScore();
    }

    private void FixedUpdate() 
    {
        _txtTemps.text = "Temps: " + _temps.ToString();
        _txtVies.text = "Nombre de vies: " + _vies.ToString();
    }
    public void AjouterScore(int points) {
        _score += points;
        UpdateScore();
    }

    public void EnleverVie ()
    {
        _vies -= 1;
    }

    private void UpdateScore() {
        _txtScore.text = "Score : " + _score.ToString();
    }

    public void ResumeGame() {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
        _pauseOn = false;
    }
}
