using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;

    [SerializeField]
    private int _powerupID;
    //private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        //_player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player _player = other.transform.GetComponent<Player>();

            switch(_powerupID)
            {
                case 0:
                    _player.EngageTripleShot();
                    break;
                case 1:
                    _player.EngageSpeedPowerup();
                    break;
                case 2:
                    if (_player.collectedShield == false)
                    {
                        _player.EngageShieldPowerup();
                    }
                    
                    break;
                default:
                    Debug.Log("Something has gone wrong with defining the powerupID");
                    break;
            }
            
            Destroy(this.gameObject);
        }
    } 


}
