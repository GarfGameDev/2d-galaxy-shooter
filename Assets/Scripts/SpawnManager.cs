using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _playerAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemy()
    {
        while(_playerAlive == true)
        {
            Vector3 position = new Vector3(Random.Range(-10.7f, 10.7f), 6.2f, 0);
            // Used to store the object we Instantiate into a GameObject type variable called enemy
            GameObject enemy = Instantiate(_enemy, position, Quaternion.identity);
            enemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    public void OnPlayerDeath()
    {
        _playerAlive = false;
    }
}
