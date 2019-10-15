using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    public enum PowerupTypes { TripleShot, Speed, Shield };
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private PowerupTypes powerupType;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-9.0f, 9.0f), 10, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -3.02)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            switch(powerupType)
            {
                case PowerupTypes.TripleShot:
                    other.transform.GetComponent<Player>().AddTripleShot();
                    break;
                case PowerupTypes.Speed:
                    other.transform.GetComponent<Player>().SpeedPowerup();
                    break;
                case PowerupTypes.Shield:
                    other.transform.GetComponent<Player>().ShieldPowerup();
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }
}
