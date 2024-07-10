using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleStone : MonoBehaviour
{
    private Rigidbody _prefabRB;
    private Vector3 _direction;
    public int _speed = 400;
    public int damage = 5;
    public AudioSource audioSource;
    public AudioClip rockSound;

    private void Awake()
    {
        _prefabRB = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _direction = this.transform.forward * _speed;
        _prefabRB.AddForce(_direction, ForceMode.Force);
        Destroy(this.gameObject, 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Snake snake = collision.gameObject.GetComponent<Snake>();
        if (snake != null)
        {
            snake.Damage(damage);
        }

        MovementGolem movementGolem = collision.gameObject.GetComponent<MovementGolem>();
        if (movementGolem !=null )
        {
            movementGolem.Damage(damage);
            Destroy(gameObject);
        }

        audioSource.PlayOneShot(rockSound);

        chaseEnemy miniGolem = collision.gameObject.GetComponent<chaseEnemy>();
        if (miniGolem != null)
        {
            miniGolem.Damage(damage);
            Destroy(gameObject);
        }
    }

}
