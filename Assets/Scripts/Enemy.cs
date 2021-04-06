using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _verticalSpeed = 1.0f;
    private float _horizontalSpeed = 4.0f;
    private float _ramSpeed = 8.0f;
    private float _rotationSpeed = 250.0f;
    private float _fireRate = 0.1f;
    private float _nextFire;

    private Player _player;

    [SerializeField]
    private GameObject _enemyLaser;
    [SerializeField]
    private GameObject _backwardLaser;

    private Animator _enemyAnim;
    private Collider2D _collider;



    [SerializeField]
    private AudioClip _enemyExplodeAudio;

    private SpawnManager _spawnManager;

    AudioSource _audio;
    private Laser _laser;

    private bool _enemyCanFire = false;
    private bool _movingRight = true;
    private bool _isBackwardEnemy = false;
    [SerializeField]
    private bool _canFire = false;

    void Start()
    {
        _enemyAnim = GetComponent<Animator>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _collider = GetComponent<Collider2D>();
        _audio = GetComponent<AudioSource>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();


        _enemyCanFire = true;

        if (_isBackwardEnemy == false && this.gameObject != null) 
        {
            StartCoroutine(SpawnLaser());
        }

        

        if (_audio != null)
        {
            _audio.clip = _enemyExplodeAudio;
        }
    }
    // Start is called before the first frame update
    void Update()
    {

        if (_player != null && this.gameObject != null)
        {
            if (_isBackwardEnemy == true)
            {
                FireBackwardLaser();
            }

            Vector3 rotationDirection = _player.transform.position - transform.position;
            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 180) * rotationDirection;
            float distance = Vector3.Distance(_player.transform.position, transform.position);

            if (distance < 5.0f && transform.position.y > _player.transform.position.y)
            {
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
                transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _ramSpeed * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
            else 
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                Movement();
            }
        }

        
    }

    private void Movement()
    {
        if (_movingRight == true)
        {
            transform.Translate(new Vector3(1, 0, 0) * _horizontalSpeed * Time.deltaTime );
            transform.Translate(new Vector3(0, -1, 0) * _verticalSpeed * Time.deltaTime );

            if (transform.position.x >= 9.7f)
            {
                _movingRight = false;
            }
            
        }

        if (_movingRight == false) 
        {
            transform.Translate(new Vector3(-1, 0, 0) * _horizontalSpeed * Time.deltaTime);
            transform.Translate(new Vector3(0, -1, 0) * _verticalSpeed * Time.deltaTime);

            if (transform.position.x <= -9.7f)
            {
                _movingRight = true;
            }
        }
        

        if (transform.position.y < -5.3f)
        {
            if (Random.value > 0.5f)
            {
                _movingRight = false;
            }

            else 
            {
                _movingRight = true;
            }
            transform.position = new Vector3(Random.Range(-9.5f, 9.5f), 6.2f, 0);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10.8f, 10.8f), transform.position.y, 0);


    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //Player player = other.transform.GetComponent<Player>();
            if (_player != null)
            {
               _player.Damage();  
            }
            _enemyAnim.SetTrigger("OnEnemyDeath");
            _spawnManager.EnemyDestroyed();
            _horizontalSpeed = 0;
            _verticalSpeed = 0;
            _ramSpeed = 0;
            _rotationSpeed = 0;
            _collider.enabled = false;
            _audio.Play();
            StartCoroutine(DestroyEnemyRoutine(this.gameObject));
        }

        else if (other.tag == "Laser")
        {
            //Player player = GameObject.Find("Player").GetComponent<Player>();
            if (_player != null)
            {
                _player.AddScore(Random.Range(5, 20));
            }
            _enemyAnim.SetTrigger("OnEnemyDeath");
            _spawnManager.EnemyDestroyed();
            _horizontalSpeed = 0;
            _ramSpeed = 0;
            _rotationSpeed = 0;
            _verticalSpeed = 0;
            Destroy(other.gameObject);
            _collider.enabled = false;
            _audio.Play();
            Destroy(GetComponent<Collider2D>());
            StartCoroutine(DestroyEnemyRoutine(this.gameObject));
            
        }
    }

    IEnumerator DestroyEnemyRoutine(GameObject enemy)
    {
        _enemyCanFire = false;
        _canFire = false;
        yield return new WaitForSeconds(2.8f);
        Destroy(enemy);
    }

    IEnumerator SpawnLaser()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1.0f, 4.0f));
            if (_enemyCanFire == true)
            {
                GameObject enemyLaser = Instantiate(_enemyLaser, transform.position, Quaternion.identity);
                Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

                for (int i = 0; i < lasers.Length; i++)
                {
                    lasers[i].EnemyLaser();
                }
            }
            else
            {
                Debug.Log("Can't fire as enemy is destroyed");
            }

        }
    }

    public void BackwardEnemy()
    {
        _isBackwardEnemy = true;
    }

    private void FireBackwardLaser()
    {
        if (this.gameObject != null)
        {
            if (transform.position.y < _player.transform.position.y && Time.time > _nextFire)
            {
                
                _nextFire = Time.time + _fireRate;
                GameObject backwardEnemy = Instantiate(_backwardLaser, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                Laser[] lasers = backwardEnemy.GetComponentsInChildren<Laser>();

                for (int i = 0; i < lasers.Length; i++)
                {
                    lasers[i].BackwardEnemyLaser();
                }
            }
        }
        else 
        {
            return;
        }

    }

    public void FireLaser()
    {
        StartCoroutine(FireLaserRoutine());
    }

    IEnumerator FireLaserRoutine()
    {
        _canFire = true;
        yield return new WaitForSeconds(0.1f);

        if (_canFire == true)
        {
            
                GameObject enemyLaser = Instantiate(_enemyLaser, transform.position, Quaternion.identity);
                Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

                for (int i = 0; i < lasers.Length; i++)
                {
                    lasers[i].EnemyLaser();
                }

                _canFire = false;
        }

        else 
        {
            Debug.Log("Can't fire");
        }
    }

   

}
