using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;

    [SerializeField]
    private int _powerupID;

    private AudioManager _audioManager;
    //private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _audioManager = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();
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
                    _audioManager.PlayAudio();
                    _player.EngageTripleShot();
                    break;
                case 1:
                    _audioManager.PlayAudio();
                    _player.EngageSpeedPowerup();
                    break;
                case 2:
                    if (_player.collectedShield == false)
                    {
                        _audioManager.PlayAudio();
                        _player.EngageShieldPowerup();
                    }
                    break;
                case 3:
                    _audioManager.PlayAudio();
                    _player.RefillAmmo();
                    break;
                case 4:
                    _audioManager.PlayAudio();
                    _player.HealPlayer();
                    break;
                case 5:
                    _audioManager.PlayAudio();
                    _player.EngageMultiShot();
                    break;
                default:
                    Debug.Log("Something has gone wrong with defining the powerupID");
                    break;
            }
            
            Destroy(this.gameObject);
        }
    } 


}
