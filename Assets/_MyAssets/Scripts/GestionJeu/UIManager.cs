using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour  {
    
    [SerializeField] private int _score = default;
    [SerializeField] private float _temps = default;
    [SerializeField] private TextMeshProUGUI _txtScore = default;
    [SerializeField] private TextMeshProUGUI _txtTemps = default;
    [SerializeField] private GameObject _pausePanel = default;
    [SerializeField] private Image _livesDisplayImage = default;
    [SerializeField] private Sprite[] _liveSprites = default;
    private bool _pauseOn = false;
    
    // Start is called before the first frame update

    public void Start() 
    {
        _score = 0;
        _temps = Time.time;
        ChangeLivesDisplayImage(3);
        Time.timeScale = 1;
        UpdateScore();
        UpdateTime();
    }

    public void Update()
    {
        if((Input.GetKeyDown(KeyCode.Escape)) && !_pauseOn)  {
            _pausePanel.SetActive(true);
            Time.timeScale = 0;
            _pauseOn = true;
        }
        else if ((Input.GetKeyDown(KeyCode.Escape)) && _pauseOn) {
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
            _pauseOn = false;
        }
    }
    public void AjouterScore(int points) 
    {
        _score += points;
        UpdateScore();
    }

    private void UpdateScore() 
    {
        _txtScore.text = "Score : " + _score.ToString();
    }

    private void UpdateTime()
    {
        _txtTemps.text = "Temps: " + _temps.ToString();
    }

    public void ChangeLivesDisplayImage(int noImage) {
        if (noImage < 0) {
            noImage = 0;
        }
        _livesDisplayImage.sprite = _liveSprites[noImage];
        
        // Si le joueur n'a plus de vie on lance la séquence de fin de partie
        if (noImage == 0) {
            PlayerPrefs.SetInt("Score", _score);
            PlayerPrefs.SetFloat("Temps", _temps);
            PlayerPrefs.Save();
            StartCoroutine("FinPartie");
        }
    }

    IEnumerator FinPartie()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);
    }

    public void ResumeGame() 
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
        _pauseOn = false;
    }
}
