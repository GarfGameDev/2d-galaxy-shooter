using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int score;
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    private Text _thrusterText;
    [SerializeField]
    private int _maxAmmo = 15;

    private Player _player;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int playerLives)
    {
        _livesImage.sprite = _liveSprites[playerLives];
        if (playerLives < 1)
        {
            StartCoroutine(GameOverFlicker());
            _restartText.gameObject.SetActive(true);
            _gameManager.GameOver();
        }
    }

    public void UpdateAmmoText(int ammoCount)
    {
        _ammoText.text = "Ammo: " + ammoCount + "/" + _maxAmmo;
    }

    public void UpdateThrusterText(int thrusterTotal)
    {
        if (thrusterTotal <= 20)
        {
            _thrusterText.color = Color.green;
            _thrusterText.text = " " + thrusterTotal;
        } 
        else if (thrusterTotal > 20 && thrusterTotal < 40)
        {
            _thrusterText.color = Color.yellow;
            _thrusterText.text = " " + thrusterTotal;
        }
        else if (thrusterTotal >= 40)
        {
            _thrusterText.color = Color.red;
            _thrusterText.text = " " + thrusterTotal;
        }

    }

    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.7f);
        }
    }
}
