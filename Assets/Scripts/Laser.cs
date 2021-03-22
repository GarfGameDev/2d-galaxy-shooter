using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _laserSpeed = 8.0f;

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        if (transform.position.y > 10)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
            
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "Enemy")
    //     {
    //         Destroy(this.gameObject);
    //     }
    // }
}
