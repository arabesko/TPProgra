using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocasBehaviour : MonoBehaviour
{
    [SerializeField] private bool _canDamage = true;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private int _damage;

    void Start()
    {
       // Destroy(gameObject, Random.Range(4, 20));
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if (_canDamage)
            {
                player.ModifyEnergy(-_damage);
            }
        }
    }

    private void Update()
    {
        if (_rb.velocity.magnitude <= 0.01f)
        {
            _canDamage = false;
        }
    }

}
