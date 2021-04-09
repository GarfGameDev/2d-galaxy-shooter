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
    private GameObject _backwardEnemy;
    [SerializeField]
    private GameObject _dodgingEnemy;
    [SerializeField]
    private GameObject _bossEnemy;
    [SerializeField]
    private GameObject _enemyContainer;

    private int _wave = 1;
    [SerializeField]
    private int _enemyCount;
    private int  _beamEnemyCount;
    private int _backwardEnemyCount;
    private int _dodgeEnemyCount;
    [SerializeField]
    private int _enemiesToDestroy;

    private UIManager _uiManager;
    //private Enemy _backwardEnemyScript;


    [SerializeField]
    private GameObject[] _powerups;

    private bool _playerAlive = true;


    public void StartGame()
    {
        
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerup());
        _uiManager.UpdateWave(_wave);
        _enemiesToDestroy = 7;
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
                    yield return new WaitForSeconds(2.0f);
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

                        if (_backwardEnemyCount < 2)
                        {
                            Vector3 backwardPosition = new Vector3(Random.Range(-10.7f, 10.7f), 6.2f, 0);
                            GameObject backwardEnemy = Instantiate(_backwardEnemy, backwardPosition, Quaternion.identity);
                            Enemy _backwardEnemyScript = backwardEnemy.GetComponent<Enemy>();
                            backwardEnemy.transform.parent = _enemyContainer.transform;
                            _backwardEnemyScript.BackwardEnemy();
                            _backwardEnemyCount += 1;
                        }

                        if (_enemiesToDestroy < 3 && _dodgeEnemyCount < 1 || _enemyCount >= 3 && _dodgeEnemyCount < 1)
                        {
                            Instantiate(_dodgingEnemy, position, Quaternion.identity);
                            _dodgeEnemyCount += 1;
                        }
                    
                   }
                   else 
                   {
                       yield return new WaitUntil(() => _enemiesToDestroy <= 0);
                       _enemyCount = 0;
                       _enemiesToDestroy = 9;
                       _beamEnemyCount = 0;
                       _dodgeEnemyCount = 0;
                       _backwardEnemyCount = 0;
                       _uiManager.UpdateEnemiesLeft(_enemiesToDestroy);
                       _wave += 1;
                       _uiManager.UpdateWave(_wave);
                   }
                   break;
                case 2:
                    yield return new WaitForSeconds(5.0f);
                    if (_enemyCount < 4)
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

                        if (_backwardEnemyCount < 3)
                        {
                            Vector3 backwardPosition = new Vector3(Random.Range(-10.7f, 10.7f), 6.2f, 0);
                            GameObject backwardEnemy = Instantiate(_backwardEnemy, backwardPosition, Quaternion.identity);
                            Enemy _backwardEnemyScript = backwardEnemy.GetComponent<Enemy>();
                            backwardEnemy.transform.parent = _enemyContainer.transform;
                            _backwardEnemyScript.BackwardEnemy();
                            _backwardEnemyCount += 1;
                        }

                        if (_enemiesToDestroy < 3 && _dodgeEnemyCount < 1 || _enemyCount >= 4 && _dodgeEnemyCount < 1)
                        {
                            Instantiate(_dodgingEnemy, position, Quaternion.identity);
                            _dodgeEnemyCount += 1;
                        }
                   }
                    else 
                    {
                        yield return new WaitUntil(() => _enemiesToDestroy <= 0);
                        _enemyCount = 0;
                        _enemiesToDestroy = 10;
                        _beamEnemyCount = 0;
                        _dodgeEnemyCount = 0;
                        _backwardEnemyCount = 0;
                        _uiManager.UpdateEnemiesLeft(_enemiesToDestroy);
                        _wave += 1;
                        _uiManager.UpdateWave(_wave);
                    }
                    break;
                case 3:
                    yield return new WaitForSeconds(2.0f);
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

                        if (_backwardEnemyCount < 3)
                        {
                            Vector3 backwardPosition = new Vector3(Random.Range(-10.7f, 10.7f), 6.2f, 0);
                            GameObject backwardEnemy = Instantiate(_backwardEnemy, backwardPosition, Quaternion.identity);
                            Enemy _backwardEnemyScript = backwardEnemy.GetComponent<Enemy>();
                            backwardEnemy.transform.parent = _enemyContainer.transform;
                            _backwardEnemyScript.BackwardEnemy();
                            _backwardEnemyCount += 1;
                        }

                        if (_enemiesToDestroy < 3 && _dodgeEnemyCount < 1 || _enemyCount >= 5 && _dodgeEnemyCount < 1)
                        {
                            Instantiate(_dodgingEnemy, position, Quaternion.identity);
                            _dodgeEnemyCount += 1;
                        }
                   }
                    else 
                    {
                        yield return new WaitUntil(() => _enemiesToDestroy <= 0);
                        _enemyCount = 0;
                        _enemiesToDestroy = 1;
                        _uiManager.UpdateEnemiesLeft(_enemiesToDestroy);
                        _wave += 1;
                        _uiManager.UpdateWave(_wave);
                    }
                    break;
                case 4:
                    yield return new WaitForSeconds(2.0f);
                    if (_enemyCount < 1)
                    {
                        Instantiate(_bossEnemy, new Vector3(0, 6.2f, 0), Quaternion.identity);
                        _enemyCount += 1;
                    }
                    else 
                    {
                        yield return new WaitUntil(() => _enemiesToDestroy <= 0);
                        _playerAlive = false;
                        _uiManager.GameOver();
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
                int multishotChance = Random.Range(0, 8);
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
