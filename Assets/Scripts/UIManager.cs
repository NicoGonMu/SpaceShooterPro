using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Text _restartText;

    [SerializeField]
    private Image _livesImage;

    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateScore(int i)
    {
        _scoreText.text = "Score: " + i;
    }

    public void UpdateLives(int i)
    {
        _livesImage.sprite = _liveSprites[i];
    }

    public void GameOver()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
        _gameManager.GameOver();
    }

    IEnumerator GameOverFlicker()
    {
        while(true)
        {
            if (_gameOverText.text == "")
            {
                _gameOverText.text = "GAME OVER";
            } else
            {
                _gameOverText.text = "";
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
