using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private float _playerSpeed = 8.0f;
    private int _doubleSpeed = 2;
    private float _thrusterSpeed = 12.0f;
    private float _fireRate = 0.5f;
    private float _nextFire = -1f;
    private float _thrusterAccelerate = 0.1f;
    private float _thrusterDecelerate = 0.5f;
    private float _nextThrust = -1f;

    [SerializeField]
    private int _playerLives = 3;
    private int _score;
    private int _shieldHealth;
    private int _ammoCount = 15;
    private int _thrusterTotal = 0;

    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldPrefab;
    private GameObject _shield;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject[] _multiShot;
    [SerializeField]
    private GameObject _laserContainer;

    [SerializeField]
    private AudioClip _laserAudio;

    private AudioSource _audio;
    
    private SpawnManager _spawnManager;

    private bool _collectedTripleShot = false;
    private bool _collectedSpeed = false;
    public bool collectedShield = false;
    public bool _isMultiShotActive = false;

    private UIManager _uiManager;

    private SpriteRenderer _shieldVisualHealth;

    private MainCamera _camera;

    void Start()
    {
        _camera = GameObject.Find("Main Camera").GetComponent<MainCamera>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();   

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is null");
        } 

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is null");
        }

        _audio = GetComponent<AudioSource>();

        _audio.clip = _laserAudio;
    }

    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;

            if (_ammoCount > 0)
            {
                FireLaser();
            }
            else
            {
                Debug.Log("You are out of ammo!");
            }

        }
        
    }

    private void FireLaser()
    {
        _nextFire = Time.time + _fireRate;
        if (_collectedTripleShot == true && _isMultiShotActive == false)
        {
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(-0.86f, 0.7f, 0), Quaternion.identity);
        }
        else if (_collectedTripleShot == false && _isMultiShotActive == false)
        {
            Instantiate(_laser, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            _ammoCount -= 1;
            _uiManager.UpdateAmmoText(_ammoCount);
        }

        if (_collectedTripleShot == false && _isMultiShotActive == true)
        {
            for (int i = 0; i < _multiShot.Length; i++)
            {
                GameObject laser = Instantiate(_laser, transform.position, Quaternion.Euler(0, 0, (i * 20)));
                laser.transform.parent = _laserContainer.transform;
            }
            
        }

        _audio.Play();
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyLaser")
        {
            Destroy(other.gameObject);
            Damage();
        }
    }



    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (_collectedSpeed == false && Input.GetKey(KeyCode.LeftShift) == false)
        {
            transform.Translate(direction * _playerSpeed * Time.deltaTime);

            if (_thrusterTotal > 0 && Time.time > _nextThrust)
            {
                _nextThrust = Time.time + _thrusterDecelerate;
                _thrusterTotal -= 1;
                _uiManager.UpdateThrusterText(_thrusterTotal);
            }
        }

        else if (_collectedSpeed == true)
        {
            transform.Translate(direction * (_playerSpeed * _doubleSpeed) * Time.deltaTime);
        }

        else if (_collectedSpeed == false && Input.GetKey(KeyCode.LeftShift))
        {
            
            if (_thrusterTotal < 50)
            {
                transform.Translate(direction * _thrusterSpeed * Time.deltaTime);
                if(Time.time > _nextThrust)
                {
                    _nextThrust = Time.time + _thrusterAccelerate;
                    _thrusterTotal += 1;
                    _uiManager.UpdateThrusterText(_thrusterTotal);
                }
                

            }

            else
            {
                transform.Translate(direction * _playerSpeed * Time.deltaTime);
            }
            
        }

        

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 10.7f)
        {
            transform.position = new Vector3(-10.7f, transform.position.y, 0);
        }
        else if (transform.position.x <= -10.7f)
        {
            transform.position = new Vector3(10.7f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        _camera.ShakeCamera();

        if (collectedShield == true)
        {
            _shieldHealth -= 1;

            switch(_shieldHealth)
            {
                case 0:
                    Destroy(_shield.gameObject);
                    collectedShield = false;
                    break;
                case 1:
                    _shieldVisualHealth.color = Color.red;
                    break;
                case 2:
                    _shieldVisualHealth.color = Color.yellow;
                    break;
                case 3:
                    _shieldVisualHealth.color = Color.magenta;
                    break;
                default:
                    Debug.Log("Something has gone wrong with the Shield Health");
                    break;

            }
        }
        else 
        {
            _playerLives -= 1;
            _uiManager.UpdateLives(_playerLives);
        }

        switch(_playerLives)
        {
            case 0:
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
                break;
            case 1:
                _leftEngine.SetActive(true);
                break;
            case 2:
                _rightEngine.SetActive(true);
                break;
            default:
                Debug.Log("Shield took the hit");
                break;
        }



        
        

        // if (_playerLives < 1)
        // {
            
        //     _spawnManager.OnPlayerDeath();
        //     Destroy(this.gameObject);
        // }
    }

    public void EngageTripleShot()
    {
        _collectedTripleShot = true;
        StartCoroutine(TripleShotTimer());
    }

    IEnumerator TripleShotTimer()
    {
        yield return new WaitForSeconds(5.0f);
        _collectedTripleShot = false;
    }

    public void EngageSpeedPowerup()
    {
       _collectedSpeed = true;
        StartCoroutine(SpeedPowerupTimer());

    }

    IEnumerator SpeedPowerupTimer()
    {
        yield return new WaitForSeconds(5.0f);
        _collectedSpeed = false;
    }

    public void EngageShieldPowerup()
    {
        _shield = Instantiate(_shieldPrefab, transform.position, Quaternion.identity);
        _shield.transform.parent = this.transform;
        collectedShield = true; 
        _shieldVisualHealth = _shield.GetComponent<SpriteRenderer>(); 
        _shieldHealth = 4;     
    }

    public void EngageMultiShot()
    {
        _isMultiShotActive = true;
        StartCoroutine(MultiShotRoutine());
    }

    IEnumerator MultiShotRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isMultiShotActive = false;
    }
    
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    public void RefillAmmo()
    {
        _ammoCount = 15;
        _uiManager.UpdateAmmoText(_ammoCount);
    }

    public void HealPlayer()
    {

        if (_playerLives == 1)
        {
            _playerLives += 1;
            _leftEngine.SetActive(false);
            _uiManager.UpdateLives(_playerLives);
        }
        else if (_playerLives == 2)
        {
            _playerLives += 1;
            _rightEngine.SetActive(false);
            _uiManager.UpdateLives(_playerLives);
        }
    }



}


        // new Vector3(1, 0, 0) * 0 * 3.5f * real time
        // Vector3.right is the same as Vector3(1, 0, 0), so this by itself * by the realtime makes it move to the right infinitely at 1 metre per second
        // The Horizontal Input is a value between 1 and -1, so if the input is -1 that would cause the Vector3 to show as Vector3(-1, 0, 0) since we're multiplying 1 by -1