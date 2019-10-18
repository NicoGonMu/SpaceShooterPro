using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private AudioSource _explosion;

    private Player _player;
    private Animator _animator;

    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate;
    private float _canShoot = -1f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-9.0f, 9.0f), 10, transform.position.z);
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = gameObject.GetComponent<Animator>();
        _explosion = GameObject.Find("Audio_Manager").GetComponent<AudioManager>().GetExplosion();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();

    }

    private void Move()
    {
        if (transform.position.y <= -3.02)
        {
            Destroy(gameObject);
        } else
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
    }

    private void Shoot()
    {
        if (Time.time > _canShoot)
        {
            _fireRate = Random.Range(3.0f, 7.0f);
            _canShoot = Time.time + _fireRate;
            GameObject enemyLasers = Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion());
            Laser[] lasers = enemyLasers.GetComponentsInChildren<Laser>();
            lasers[0].IsEnemy(true);
            lasers[1].IsEnemy(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string otherTag = other.gameObject.tag;
        if (otherTag == "Player" && _player != null) {
            _player.Damage();
            _animator.SetTrigger("Destroyed");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            _explosion.Play();
            Destroy(gameObject, 2.5f);

        } else if (otherTag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            _animator.SetTrigger("Destroyed");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            _explosion.Play();
            Destroy(gameObject, 3f);
        }
    }
}
