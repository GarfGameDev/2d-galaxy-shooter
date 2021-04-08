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
    [SerializeField]
    private Text _missileText;

    [SerializeField]
    private Text _waveOneText;
    [SerializeField]
    private Text _waveTwoText;
    [SerializeField]
    private Text _waveThreeText;

    [SerializeField]
    private Text _enemiesRemaining;
    

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

    public void UpdateMissileText(int missileCount)
    {
        _missileText.text = "Missile: " + missileCount + "/1";
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

    public void UpdateWave(int currentwave)
    {
        StartCoroutine(WaveRoutine(currentwave));
    }

    public void UpdateEnemiesLeft(int enemies)
    {
        _enemiesRemaining.text = "Enemies Left: " + enemies;
    }

    IEnumerator WaveRoutine(int wave)
    {
        switch(wave)
        {
            case 1:
                _waveOneText.gameObject.SetActive(true);
                yield return new WaitForSeconds(2.0f);
                _waveOneText.gameObject.SetActive(false);
                break;
            case 2:
                _waveTwoText.gameObject.SetActive(true);
                yield return new WaitForSeconds(2.0f);
                _waveTwoText.gameObject.SetActive(false);
                break;
            case 3:
                _waveThreeText.gameObject.SetActive(true);
                yield return new WaitForSeconds(2.0f);
                _waveThreeText.gameObject.SetActive(false);
                break;
            default:
                Debug.Log("Something has gone horribly wrong");
                break;
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
