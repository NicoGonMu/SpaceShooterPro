using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    
    private float _speed = 8f;

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
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
    }
}
