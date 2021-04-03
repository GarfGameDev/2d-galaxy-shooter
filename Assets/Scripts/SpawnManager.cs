using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _beamEnemy;
    [SerializeField]
    private GameObject _enemyContainer;

    private int _wave = 1;
    [SerializeField]
    private int _enemyCount;
    private int  _beamEnemyCount;
    [SerializeField]
    private int _enemiesToDestroy;

    private UIManager _uiManager;


    [SerializeField]
    private GameObject[] _powerups;

    private bool _playerAlive = true;
    // Start is called before the first frame update


    public void StartGame()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerup());
        _uiManager.UpdateWave(_wave);
        _enemiesToDestroy = 4;
        _uiManager.UpdateEnemiesLeft(_enemiesToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        while(_playerAlive == true)
        {

            switch (_wave)
            {
                case 1:
                   if (_enemyCount < 3)
                   {
                        Vector3 position = new Vector3(Random.Range(-10.7f, 10.7f), 6.2f, 0);
                        GameObject enemy = Instantiate(_enemy, position, Quaternion.identity);
                        enemy.transform.parent = _enemyContainer.transform;
                        _enemyCount += 1;
                        yield return new WaitForSeconds(5.0f);

                        if (_beamEnemyCount < 1)
                        {
                            Instantiate(_beamEnemy, new Vector3(0, 6, 0), Quaternion.identity);
                            _beamEnemyCount += 1;
                        }
                    
                   }
                   else 
                   {
                       yield return new WaitUntil(() => _enemiesToDestroy == 0);
                       _enemyCount = 0;
                       _enemiesToDestroy = 6;
                       _beamEnemyCount = 0;
                       _uiManager.UpdateEnemiesLeft(_enemiesToDestroy);
                       _wave += 1;
                       _uiManager.UpdateWave(_wave);
                   }
                   break;
                case 2:
                    if (_enemyCount < 5)
                   {
                        Vector3 position = new Vector3(Random.Range(-10.7f, 10.7f), 6.2f, 0);
                        GameObject enemy = Instantiate(_enemy, position, Quaternion.identity);
                        enemy.transform.parent = _enemyContainer.transform;
                        _enemyCount += 1;
                        yield return new WaitForSeconds(5.0f);

                        if (_beamEnemyCount < 1)
                        {
                            Instantiate(_beamEnemy, new Vector3(0, 6, 0), Quaternion.identity);
                            _beamEnemyCount += 1;
                        }
                   }
                    else 
                    {
                        yield return new WaitUntil(() => _enemiesToDestroy == 0);
                        _enemyCount = 0;
                        _enemiesToDestroy = 8;
                        _beamEnemyCount = 0;
                        _uiManager.UpdateEnemiesLeft(_enemiesToDestroy);
                        _wave += 1;
                        _uiManager.UpdateWave(_wave);
                    }
                    break;
                case 3:
                    if (_enemyCount < 8)
                   {
                        Vector3 position = new Vector3(Random.Range(-10.7f, 10.7f), 6.2f, 0);
                        GameObject enemy = Instantiate(_enemy, position, Quaternion.identity);
                        enemy.transform.parent = _enemyContainer.transform;
                        _enemyCount += 1;
                        yield return new WaitForSeconds(5.0f);

                        if (_beamEnemyCount < 1)
                        {
                            Instantiate(_beamEnemy, new Vector3(0, 6, 0), Quaternion.identity);
                            _beamEnemyCount += 1;
                        }
                   }
                    else 
                    {
                        yield return new WaitUntil(() => _enemiesToDestroy == 0);
                        _playerAlive = false;
                    }
                    break;
                default:
                    Debug.Log("Something has gone wrong with spawning");
                    break;
            }



            // Vector3 position = new Vector3(Random.Range(-10.7f, 10.7f), 6.2f, 0);
            // // Used to store the object we Instantiate into a GameObject type variable called enemy
            // GameObject enemy = Instantiate(_enemy, position, Quaternion.identity);
            // enemy.transform.parent = _enemyContainer.transform;
            // yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerup()
    {
        while (_playerAlive == true) 
        {
            float randomValue = Random.value;
            if (randomValue <= 0.1f)
            {
                int multishotChance = Random.Range(0, 7);
                Instantiate(_powerups[multishotChance], new Vector3(Random.Range(-10.7f, 10.7f), 6.2f, 0), Quaternion.identity); 
                yield return new WaitForSeconds(3.0f);
            }
            else if (randomValue > 0.1f && randomValue < 0.5f)
            {
                float secondValue = Random.value;
                if (secondValue < 0.4f)
                {
                    int randomPowerUp = Random.Range(0, 6);
                    Instantiate(_powerups[randomPowerUp], new Vector3(Random.Range(-10.7f, 10.7f), 6.2f, 0), Quaternion.identity);           
                    yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
                }

                else 
                {
                    Debug.Log("Chance for health spawn");
                    int randomPowerUp = Random.Range(0, 5);
                    Instantiate(_powerups[randomPowerUp], new Vector3(Random.Range(-10.7f, 10.7f), 6.2f, 0), Quaternion.identity);           
                    yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
                }
            }

            else if (randomValue > 0.5f)
            {
                Debug.Log("Ammo Spawn");
                Instantiate(_powerups[3], new Vector3(Random.Range(-10.7f, 10.7f), 6.2f, 0), Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
            }

        }
    }

    public void OnPlayerDeath()
    {
        _playerAlive = false;
    }

    public void EnemyDestroyed()
    {
        _enemiesToDestroy -= 1;
        _uiManager.UpdateEnemiesLeft(_enemiesToDestroy);
    }
}
