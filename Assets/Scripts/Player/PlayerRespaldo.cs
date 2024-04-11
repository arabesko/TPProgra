using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3Person : MonoBehaviour
{
    [Header("Conexiones")]
    private Rigidbody _rB;

    [Header("Propiedades player")]
    [SerializeField] private float _xAxis;
    [SerializeField] private float _zAxis;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;

    [Header("Propiedades player")]
    [SerializeField] private Vector3 _direction;


    private void Awake()
    {
        _rB = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rB.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        _xAxis = Input.GetAxis("Horizontal");
        _zAxis = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (_xAxis != 0 || _zAxis != 0)
        {
            Movement();
        }
    }

    private void Movement()
    {
        _direction = (this.transform.right * _xAxis + this.transform.forward * _zAxis) * _speed;
        _rB.velocity = new Vector3(_direction.x, _rB.velocity.y, _direction.z);
        print(_rB.velocity);

    }
}
