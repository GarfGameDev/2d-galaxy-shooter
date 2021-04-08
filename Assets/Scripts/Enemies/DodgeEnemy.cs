using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeEnemy : MonoBehaviour
{
    [SerializeField]
    private float _verticalSpeed = 1.0f;
    private float _horizontalSpeed = 4.0f;

    private Player _player;

    [SerializeField]
    private GameObject _enemyLaser;

    private Animator _enemyAnim;
    private Collider2D _collider;



    [SerializeField]
    private AudioClip _enemyExplodeAudio;

    private SpawnManager _spawnManager;

    AudioSource _audio;
    private Laser _laser;

    private bool _enemyCanFire = false;
    [SerializeField]
    private bool _dodgeRight = false;
    [SerializeField]
    private bool _dodgeLeft = false;
    private bool _dodging = false;


    void Start()
    {
        _enemyAnim = GetComponent<Animator>();
        if (GameObject.Find("Player") != null)
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
        }
        
        _collider = GetComponent<Collider2D>();
        _audio = GetComponent<AudioSource>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();


        _enemyCanFire = true;
        StartCoroutine(SpawnLaser());

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

            // Vector3 rotationDirection = _player.transform.position - transform.position;
            // Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 180) * rotationDirection;
            // float distance = Vector3.Distance(_player.transform.position, transform.position);

            // if (distance < 5.0f && transform.position.y > _player.transform.position.y)
            // {
            //     Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
            //     transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _ramSpeed * Time.deltaTime);
            //     transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            // }
            // else 
            // {
            //     transform.rotation = Quaternion.Euler(0, 0, 0);
            //     Movement();
            // }

            Movement();
            
        }

        
    }

    private void Movement()
    {
        if (_dodgeRight == true)
        {
            transform.Translate(new Vector3(1, 0, 0) * _horizontalSpeed * Time.deltaTime );

            if (transform.position.x >= 9.7f || transform.position.x <= -9.7f)
            {
                _dodgeRight = false;
            }
        }

        else if (_dodgeLeft == true)
        {
            transform.Translate(new Vector3(-1, 0, 0) * _horizontalSpeed * Time.deltaTime );

            if (transform.position.x >= 9.7f || transform.position.x <= -9.7f)
            {
                _dodgeLeft = false;
            }
        }           

        else if (_dodgeLeft == false && _dodgeRight == false)
        {
            transform.Translate(new Vector3(0, -1, 0) * _verticalSpeed * Time.deltaTime);

        }
        

        if (transform.position.y < -5.3f)
        {
            _dodgeRight = false;
            _dodgeLeft = false;
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

    public void DodgeLaserRight()
    {
        if (_dodging == false) 
        {
            StartCoroutine(DodgeLaserRightRoutine());
        }
        else 
        {
            return;
        }

    }
    public void DodgeLaserLeft()
    {
        if (_dodging == false) 
        {
            StartCoroutine(DodgeLaserLeftRoutine());
        }
        else 
        {
            return;
        }
    }

    IEnumerator DodgeLaserRightRoutine()
    {
        _dodging = true;
        _dodgeRight = true;
        yield return new WaitForSeconds(0.5f);
        _dodgeRight = false;
        _dodging = false;
    }

    IEnumerator DodgeLaserLeftRoutine()
    {
        _dodging = true;
        _dodgeLeft = true;
        yield return new WaitForSeconds(0.5f);
        _dodgeLeft = false;
        _dodging = false; 
    }

   

}
