using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehaviour : MonoBehaviour
{
    public int _maxSpeed;
    public int _speed;
    // Start is called before the first frame update
    void Start()
    {
        _maxSpeed = Random.Range(60, 120);
        StartCoroutine(IncrementSpeed());
    }

    // Update is called once per frame
    void Update()
    {
        
    }



        IEnumerator IncrementSpeed()
        {
            while (_maxSpeed > _speed)
            {
                _speed += 5;
                yield return new WaitForSeconds(0.5f);
            }
        }
}
