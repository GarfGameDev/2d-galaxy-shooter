using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamEnemy : MonoBehaviour
{
    private float _speed = 16.0f;

    [SerializeField]
    private GameObject _beamLaser;
    [SerializeField]
    private GameObject _shield;

    [SerializeField]
    private bool _isRotatingRight;
    private bool _canFire = true;
    private bool _isAlive = true;
    private bool _shieldActive = true;

    [SerializeField]
    private AudioClip _enemyExplodeAudio;
    private SpawnManager _spawnManager;
    private Animator _enemyAnim;
    

    private AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _enemyAnim = GetComponent<Animator>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        StartCoroutine(BeamFiringRoutine());

        if (_audio != null)
        {
            _audio.clip = _enemyExplodeAudio;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAlive == true)
        {
            if (_isRotatingRight == true)
            {
                transform.Rotate(new Vector3(0, 0, 1) * _speed * Time.deltaTime);

                if (transform.localEulerAngles.z >= 90.0f && transform.localEulerAngles.z < 100.0f)
                {
                    _isRotatingRight = false;
                }
            }

            if (_isRotatingRight == false) 
            {
                transform.Rotate(new Vector3(0, 0, -1) *_speed * Time.deltaTime);

                if (transform.localEulerAngles.z < 270.0f && transform.localEulerAngles.z > 260.0f)
                {
                    _isRotatingRight = true;
                }
            }
        }
        
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Laser")
        {
            if (_shieldActive == true) 
            {
                _shieldActive = false;
                _shield.SetActive(false);
                Destroy(other.gameObject);
            }

            else 
            {
                _audio.Play();
                _enemyAnim.SetTrigger("OnEnemyDeath");
                _spawnManager.EnemyDestroyed();
                Destroy(other.gameObject);
                Destroy(GetComponent<Collider2D>());
                StartCoroutine(DestroyEnemyRoutine(this.gameObject));
            }

        }
    }

    IEnumerator BeamFiringRoutine()
    {
        while (_canFire == true)
        {
            yield return new WaitForSeconds(3.0f);
            if (_canFire == true)
            {
                _beamLaser.SetActive(true);
            }
            
            yield return new WaitForSeconds(11.0f);
            _beamLaser.SetActive(false);
            


        }
    }

    IEnumerator DestroyEnemyRoutine(GameObject enemy)
    {
        _isAlive = false;
        _canFire = false;
        _beamLaser.SetActive(false);
        yield return new WaitForSeconds(2.8f);
        Destroy(enemy);
    }
}
