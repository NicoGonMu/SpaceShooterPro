using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed;
    private SpawnManager _spawnManager;
    private AudioSource _explosion;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _explosion = GameObject.Find("Audio_Manager").GetComponent<AudioManager>().GetExplosion();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, _rotateSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Laser")
        {
            Destroy(other.gameObject);
            _animator.SetTrigger("Hit");
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            _explosion.Play();
            _spawnManager.StartSpawn();
            Destroy(gameObject, 2.5f);

        }
    }
}
