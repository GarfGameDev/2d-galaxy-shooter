using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 3.0f;
    private Player _player;

    [SerializeField]
    private GameObject _enemyLaser;

    private Animator _enemyAnim;
    private Collider2D _collider;

    [SerializeField]
    private AudioClip _enemyExplodeAudio;

    AudioSource _audio;
    private Laser _laser;

    private bool _enemyCanFire = false;

    void Start()
    {
        _enemyAnim = GetComponent<Animator>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _collider = GetComponent<Collider2D>();
        _audio = GetComponent<AudioSource>();

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
        transform.Translate(new Vector3(0, -1, 0) * _enemySpeed * Time.deltaTime );

        if (transform.position.y < -5.3f)
        {
            transform.position = new Vector3(Random.Range(-9.5f, 9.5f), 6.2f, 0);
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
            _enemyAnim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
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
            _enemySpeed = 0;
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
        while (_enemyCanFire == true)
        {
            yield return new WaitForSeconds(Random.Range(1.0f, 4.0f));
            GameObject enemyLaser = Instantiate(_enemyLaser, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].EnemyLaser();
            }
        }
    }

}
