using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    private Player _player;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-9.0f, 9.0f), 10, transform.position.z);
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -3.02)
        {
            Destroy(gameObject);
        } else
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string otherTag = other.gameObject.tag;
        if (otherTag == "Player" && _player != null) {
            _player.Damage();
            _animator.SetTrigger("Destroyed");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
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
            Destroy(gameObject, 3f);
        }
    }
}
