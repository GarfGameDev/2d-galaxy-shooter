using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 8.0f;
    private int _doubleSpeed = 2;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = -1f;

    [SerializeField]
    private int _playerLives = 3;

    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldPrefab;
    private GameObject _shield;
    
    private SpawnManager _spawnManager;

    private bool _collectedTripleShot = false;
    private bool _collectedSpeed = false;
    public bool collectedShield = false;

    

    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();   

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is null");
        } 
    }

    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;

            FireLaser();
        }
        
    }

    private void FireLaser()
    {
        _nextFire = Time.time + _fireRate;
        if (_collectedTripleShot == true)
        {
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(-0.86f, 0.7f, 0), Quaternion.identity);
        }
        else if (_collectedTripleShot == false)
        {
            Instantiate(_laser, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        }
       
    }



    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (_collectedSpeed == false)
        {
            transform.Translate(direction * _playerSpeed * Time.deltaTime);
        }

        else if (_collectedSpeed == true)
        {
            transform.Translate(direction * (_playerSpeed * _doubleSpeed) * Time.deltaTime);
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

        if (collectedShield == true)
        {
            Destroy(_shield.gameObject);
            collectedShield = false;
        }
        else 
        {
            _playerLives -= 1;
        }
        

        if (_playerLives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
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
    }



}


        // new Vector3(1, 0, 0) * 0 * 3.5f * real time
        // Vector3.right is the same as Vector3(1, 0, 0), so this by itself * by the realtime makes it move to the right infinitely at 1 metre per second
        // The Horizontal Input is a value between 1 and -1, so if the input is -1 that would cause the Vector3 to show as Vector3(-1, 0, 0) since we're multiplying 1 by -1