using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    private GameObject[] _enemies;
    private Transform nearestTarget;

    private float _speed = 3.0f;
    private float _rotationSpeed = 255.0f;

    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy") != null)
        {
            _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        Transform[] enemyTransform = new Transform[_enemies.Length];
        

        float minDistance = Mathf.Infinity;
        float distance;

        // foreach (Transform enemy in enemyTransform)
        // {
        //     float Distance = Vector3.Distance(enemy.position, transform.position);

        //     if (Distance < dist)
        //     {
        //         dist = Distance;
        //         Debug.Log(Distance);
        //     }
        // }

        for (int i = 0; i < _enemies.Length; i++)
        {
            if (_enemies[i] == null)
            {
                Destroy(this.gameObject);
            }
            else 
            {
                enemyTransform[i] = _enemies[i].transform;

                Vector3 rotationDirection = enemyTransform[i].position - transform.position;
                Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 180) * rotationDirection;

                distance = Vector3.Distance(enemyTransform[i].position, transform.position);

                if (distance < minDistance && enemyTransform[i] != null)
                {
                    nearestTarget = enemyTransform[i];
                    minDistance = distance;
                }

                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
                transform.position = Vector3.MoveTowards(transform.position, nearestTarget.position, _speed * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            }
        }

    }
    
}
