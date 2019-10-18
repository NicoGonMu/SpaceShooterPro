using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    
    private float _speed = 8f;

    private bool _isEnemy = false;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 9.5)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        } else
        {
            Vector3 direction = _isEnemy ? Vector3.down : Vector3.up;
            transform.Translate(direction * _speed * Time.deltaTime);
        }
    }

    public void IsEnemy(bool isEnemy)
    {
        _isEnemy = isEnemy;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemy)
        {
            Player player = other.GetComponent<Player>();
            player.Damage();
        }
    }
}
