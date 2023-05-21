using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour  {
    
    [SerializeField] private int _score =  default;
    [SerializeField] private TextMeshProUGUI _txtScore = default;
    [SerializeField] private TextMeshProUGUI _txtGameOver = default;
    [SerializeField] private TextMeshProUGUI _txtRestart = default;
    [SerializeField] private TextMeshProUGUI _txtQuit = default;
    [SerializeField] private Image _livesDisplayImage = default;
    [SerializeField] private Sprite[] _liveSprites = default;
    [SerializeField] private GameObject _pausePanel = default;
    private bool _pauseOn = false;
    
    // Start is called before the first frame update

    private void Start() {
        _score = 0;
        _txtGameOver.gameObject.SetActive(false);
        ChangeLivesDisplayImage(3);
        UpdateScore();
    }

    private void Update() {
        if (_txtRestart.gameObject.activeSelf && Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (_txtRestart.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(0);
        }

        if((Input.GetKeyDown(KeyCode.Escape) && !_txtRestart.gameObject.activeSelf) && !_pauseOn)  {
            _pausePanel.SetActive(true);
            Time.timeScale = 0;
            _pauseOn = true;
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) && !_txtRestart.gameObject.activeSelf) && _pauseOn) {
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
            _pauseOn = false;
        }
    }
    public void AjouterScore(int points) {
        _score += points;
        UpdateScore();
    }

    private void UpdateScore() {
        _txtScore.text = "Score : " + _score.ToString();
    }

    public void ChangeLivesDisplayImage(int noImage) {
        if (noImage < 0) {
            noImage = 0;
        }
        _livesDisplayImage.sprite = _liveSprites[noImage];
        if (noImage == 0) {
            GameOverSequence();
        }
    }

    private void GameOverSequence() {
        _txtGameOver.gameObject.SetActive(true);
        _txtRestart.gameObject.SetActive(true);
        _txtQuit.gameObject.SetActive(true);
        StartCoroutine(GameOverBlinkRoutine());
    }

    IEnumerator GameOverBlinkRoutine() {
        while (true) {
            _txtGameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            _txtGameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.7f);
        }
    }

    public void ResumeGame() {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
        _pauseOn = false;
    }
}
