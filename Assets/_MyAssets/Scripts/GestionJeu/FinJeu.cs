using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FinJeu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtScore = default;
    private int _score;
    private GestionScene _quitter;

    // Start is called before the first frame update
    void Start()
    {
        _score = PlayerPrefs.GetInt("Score");
        _txtScore.text = "Votre Pointage : " + _score.ToString();
    }

    private void ChargerDepart()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            _quitter.Quitter();
        }
    }

}
