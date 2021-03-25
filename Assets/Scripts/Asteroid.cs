using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    private Animator _asteroidAnim;
    private SpawnManager _spawnManager;

    [SerializeField]
    private AudioClip _asteroidExplodeAudio;
    private AudioSource _audio;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _audio.clip = _asteroidExplodeAudio;
    }

    void Update()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _asteroidAnim = GetComponent<Animator>();
        transform.Rotate(new Vector3(0, 0, 1) * _speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            _audio.Play();
            StartCoroutine(StartGameCoroutine(this.gameObject));
            Destroy(other.gameObject);
            Destroy(GetComponent<Collider2D>());
            
            _asteroidAnim.SetTrigger("isAsteroidHit");
        }
    }

    IEnumerator StartGameCoroutine(GameObject enemy)
    {
        yield return new WaitForSeconds(2.8f);
        _spawnManager.StartGame();
        Destroy(enemy);
    }
}
