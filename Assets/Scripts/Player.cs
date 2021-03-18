using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 8.0f;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = -1f;

    [SerializeField]
    private GameObject _laser;

    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            FireLaser();
        }
        
    }

    private void FireLaser()
    {

            _nextFire = Time.time + _fireRate;
            Instantiate(_laser, transform.position + new Vector3(0.5f, 1f, 0), Quaternion.identity);
            
        
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _playerSpeed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 10.7f)
        {
            transform.position = new Vector3(-10.7f, transform.position.y, 0);
        }
        else if (transform.position.x <= -10.7f)
        {
            transform.position = new Vector3(10.7f, transform.position.y, 0);
        }
    }
}


        // new Vector3(1, 0, 0) * 0 * 3.5f * real time
        // Vector3.right is the same as Vector3(1, 0, 0), so this by itself * by the realtime makes it move to the right infinitely at 1 metre per second
        // The Horizontal Input is a value between 1 and -1, so if the input is -1 that would cause the Vector3 to show as Vector3(-1, 0, 0) since we're multiplying 1 by -1