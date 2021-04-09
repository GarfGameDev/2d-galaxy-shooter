using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private float _verticalSpeed = 1.0f;
    private float _rotateSpeed = 16.0f;

    private float _health = 12;

    private Player _player;

    [SerializeField]
    private GameObject _enemyLaser;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _bossLaser;


    private Animator _enemyAnim;
    private Collider2D _collider;



    [SerializeField]
    private AudioClip _enemyExplodeAudio;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private MainCamera _mainCamera;

    AudioSource _audio;
    private Laser _laser;

    [SerializeField]
    private bool _enemyCanFire = false;
    private bool _isRotatingRight = true;



    void Start()
    {
        _enemyAnim = GetComponent<Animator>();
        if (GameObject.Find("Player") != null)
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
        }
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _collider = GetComponent<Collider2D>();
        _audio = GetComponent<AudioSource>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _mainCamera = GameObject.Find("Main Camera").GetComponent<MainCamera>();


        _enemyCanFire = true;
        StartCoroutine(SpawnLaser());
        StartCoroutine(SpawnFrontLaser());
        StartCoroutine(BossLaser());

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


            Movement();
            
        }
        else if (_player == null)
        {
            _enemyCanFire = false;
        }

        
    }

    private void Movement()
    {

        transform.Translate(new Vector3(0, -1, 0) * _verticalSpeed * Time.deltaTime);

        if (transform.position.y <= 2)
        {
            _verticalSpeed = 0;

            if (_isRotatingRight == true)
            {
                transform.Rotate(new Vector3(0, 0, 1) * _rotateSpeed * Time.deltaTime);

                if (transform.localEulerAngles.z >= 30.0f && transform.localEulerAngles.z < 35.0f)
                {
                    _isRotatingRight = false;
                }
            }

            if (_isRotatingRight == false) 
            {
                transform.Rotate(new Vector3(0, 0, -1) *_rotateSpeed * Time.deltaTime);

                if (transform.localEulerAngles.z < 330.0f && transform.localEulerAngles.z > 325.0f)
                {
                    _isRotatingRight = true;
                }
            }
        }

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
            Damage();
            _audio.Play();
            if (_health <= 0)
            {
                _leftEngine.SetActive(false);
                _rightEngine.SetActive(false);
                _enemyCanFire = false;
                _collider.enabled = false;
                _enemyAnim.SetTrigger("OnEnemyDeath");
                _spawnManager.EnemyDestroyed();
                StartCoroutine(DestroyEnemyRoutine(this.gameObject));
                
            }
            
        }

        else if (other.tag == "Laser")
        {
            //Player player = GameObject.Find("Player").GetComponent<Player>();
            if (_player != null)
            {
                _player.AddScore(Random.Range(5, 20));
            }
            Destroy(other.gameObject);
            Damage();
            _audio.Play();
            if (_health <= 0)
            {
                _leftEngine.SetActive(false);
                _rightEngine.SetActive(false);
                _enemyCanFire = false;
                _collider.enabled = false;
                _enemyAnim.SetTrigger("OnEnemyDeath");
                _spawnManager.EnemyDestroyed();
                StartCoroutine(DestroyEnemyRoutine(this.gameObject));
                
            }
            
        }
    }

    IEnumerator DestroyEnemyRoutine(GameObject enemy)
    {
        yield return new WaitForSeconds(2.8f);
        Destroy(enemy);
    }

    IEnumerator SpawnLaser()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(8.0f);
            if (_enemyCanFire == true)
            {

                for (int i = 0; i < 9; i++)
                {
                    GameObject enemyLaser = Instantiate(_enemyLaser, transform.position, Quaternion.Euler(0, 0, (i *40)));
                }


            }


        }
    }

    IEnumerator SpawnFrontLaser()
    {
        while (true) 
        {
            yield return new WaitForSeconds(3.0f);
            if (_enemyCanFire == true)
            {
                GameObject enemyLaser1 = Instantiate(_enemyLaser, transform.position, Quaternion.Euler(0, 0, 210) * transform.rotation);
                GameObject enemyLaser2 = Instantiate(_enemyLaser, transform.position, Quaternion.Euler(0, 0, 180) * transform.rotation);
                GameObject enemyLaser3 = Instantiate(_enemyLaser, transform.position, Quaternion.Euler(0, 0, 150) * transform.rotation);
            }


        }

    }

    IEnumerator BossLaser()
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            if (_enemyCanFire == true)
            {
                _uiManager.ShowWarningRight();
                _mainCamera.BossShake();
                yield return new WaitForSeconds(2.0f);
                _uiManager.ShowWarningRight();
                GameObject BossLaserRight = Instantiate(_bossLaser, new Vector3(6f, 0.78f, 0f), Quaternion.identity);
                yield return new WaitForSeconds(4.0f);
                Destroy(BossLaserRight);
                yield return new WaitForSeconds(5.0f);
                if (_enemyCanFire == true) 
                {
                    _uiManager.ShowWarningLeft();
                    _mainCamera.BossShake();
                    yield return new WaitForSeconds(2.0f);
                    _uiManager.ShowWarningLeft();
                    GameObject BossLaserLeft = Instantiate(_bossLaser, new Vector3(-6f, 0.78f, 0f), Quaternion.identity);
                    yield return new WaitForSeconds(4.0f);
                    Destroy(BossLaserLeft);
                }
            }
        }
    }

    private void Damage()
    {
        _health -= 1;

        if (_health <= 8)
        {
            _leftEngine.SetActive(true);
        }

        if (_health <= 4)
        {
            _rightEngine.SetActive(true);
        }
    }

}
