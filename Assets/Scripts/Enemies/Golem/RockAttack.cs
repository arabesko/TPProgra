using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAttack : MonoBehaviour
{
    private Rigidbody _prefabRB;
    private Vector3 _direction;
    private int _speed = 2000;
    public int damage = 10;

    private void Awake()
    {
        _prefabRB = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //_direction = this.transform.forward * _speed;
        Destroy(this.gameObject, 10);
    }

    public void HaciaElPlayer(Vector3 direction)
    {
        _prefabRB.AddForce(direction * _speed, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if(player != null)
        {
            player.TakeDamage(damage);
        }
    }

}
