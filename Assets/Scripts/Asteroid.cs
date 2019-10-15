using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, _rotateSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string otherTag = other.gameObject.tag;
        if (otherTag == "Player")
        {
            _player.Damage();
            _animator.SetTrigger("Destroyed");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject, 2.5f);

        }
        else if (otherTag == "Laser")
        {
            Destroy(other.gameObject);
            _animator.SetTrigger("Destroyed");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject, 2.5f);
        }
    }
}
