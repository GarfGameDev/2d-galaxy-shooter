using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _laserSpeed = 8.0f;
    private bool _isEnemyLaser = false;
    private bool _isBackwardLaser = false;

    // Update is called once per frame
    void Update()
    {
        if (_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }

        if (_isBackwardLaser)
        {
            MoveUp();
        }
        

    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        if (transform.position.y > 10)
        {
            Destroy(this.gameObject);
        }
        else if (transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
        else if (transform.position.x > 10)
        {
            Destroy(this.gameObject);
        }
        else if (transform.position.x < -10)
        {
            Destroy(this.gameObject);
        }
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * _laserSpeed * Time.deltaTime);

        if (transform.position.y < -6.0f)
        {
            Destroy(this.gameObject);
        }
    }


    public void EnemyLaser()
    {
        _isEnemyLaser = true;
    }

    public void BackwardEnemyLaser()
    {
        _isBackwardLaser = true;
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "Enemy")
    //     {
    //         Destroy(this.gameObject);
    //     }
    // }
}
