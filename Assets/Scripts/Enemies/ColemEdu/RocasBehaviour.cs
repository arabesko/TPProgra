using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocasBehaviour : MonoBehaviour
{
    [SerializeField] private bool _canDamage = true;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private int _damage;
    [SerializeField] private bool onlyOneDamage = true;
    void Start()
    {
       //Destroy(gameObject, Random.Range(2, 20));
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
                if(onlyOneDamage == true)
                {
                    player.TakeDamage(_damage);
                    onlyOneDamage = false;
                }
                
            }
        }
        Destroy(gameObject, Random.Range(4, 7));
    }

    private void Update()
    {
        if (_rb.velocity.magnitude <= 0.01f)
        {
            _canDamage = false;
        }
    }

}
