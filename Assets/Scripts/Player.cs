using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private AudioSource _shootAudio;
    private AudioSource _explosion;
    private AudioSource _powerUpSound;

    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private GameObject _rightFire;
    [SerializeField]
    private GameObject _leftFire;
    [SerializeField]
    private bool _tripleShotEnabled = false;
    private bool _hasField = false;
    private float _powerupCD = 5f;
    private float _canShoot = -1f;
    private float _fireRate = 0.15f;
    [SerializeField]
    private int _lives = 3;

    private int _score = 0;

    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _explosion = GameObject.Find("Audio_Manager").GetComponent<AudioManager>().GetExplosion();
        _powerUpSound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        Shoot();
    }

    void CalculateMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Boundary limits
        Vector3 directions = new Vector3(horizontal, vertical, 0) * _speed * Time.deltaTime;

        float futureX = transform.position.x + directions.x;
        float futureY = transform.position.y + directions.y;
        if (futureY < 9 && futureY > -1.4)
        {
            transform.Translate(0, directions.y, 0);
        }

        if (futureX >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, transform.position.z);
        }
        else if (futureX <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, transform.position.z);
        }
        else
        {
            transform.Translate(directions.x, 0, 0);
        }
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canShoot < Time.time)
        {
            _canShoot = Time.time + _fireRate;
            if (_tripleShotEnabled)
            {
                Instantiate(_tripleShotPrefab, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), new Quaternion());
            } else
            {
                Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), new Quaternion());
            }

            _shootAudio.Play();
        }
    }

    public void Damage()
    {
        // If player has a Field, remove it ando do not damage him
        if (_hasField)
        {
            _hasField = false;
            _shield.SetActive(false);
            return;
        }


        _lives--;
        _uiManager.UpdateLives(_lives);

        // Set damage animations
        if (_lives == 2)
        {
            // Randomly select which engine will be damaged
            int i = Random.Range(0, 2);
            if (i == 0)
            {
                _rightFire.SetActive(true);
            } else
            {
                _leftFire.SetActive(true);
            }
        } else
        {
            _rightFire.SetActive(true);
            _leftFire.SetActive(true);
        }

        if (_lives == 0)
        {
            _uiManager.GameOver();
            _explosion.Play();
            Destroy(gameObject);
        }
    }

    public void AddTripleShot()
    {
        _powerUpSound.Play();
        _tripleShotEnabled = true;
        StartCoroutine(DisableTripleShot());
    }

    IEnumerator DisableTripleShot()
    {
        yield return new WaitForSeconds(_powerupCD);
        _tripleShotEnabled = false;
    }

    public void SpeedPowerup()
    {
        _powerUpSound.Play();
        _speed = 8.5f;
        StartCoroutine(DisableSpeed());
    }

    IEnumerator DisableSpeed()
    {
        yield return new WaitForSeconds(_powerupCD);
        _speed = 3.5f;
    }

    public void ShieldPowerup()
    {
        _powerUpSound.Play();
        _hasField = true;
        _shield.SetActive(true);
    }

    public void AddScore(int i)
    {
        _score += i;
        _uiManager.UpdateScore(_score);
    }
}
