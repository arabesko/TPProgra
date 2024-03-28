using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _movSpeed = 3f;
    [SerializeField] private float _RotationSpeed = 100f;
    [SerializeField] private float _jumpForce = 300;
    private float _xAxis;
    private float _yAxis;
    private Vector3 _dir;
    public Rigidbody myRigid;

    private void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _xAxis = Input.GetAxis("Horizontal");
        _yAxis = Input.GetAxis("Vertical");

        if (_xAxis != 0 || _yAxis != 0)
        {
            _dir = transform.forward * _yAxis;
            Movement();
        }

        JumpMoving();
    }

    void Movement()
    {
        if (_xAxis != 0)
        {
            transform.Rotate(0, _xAxis * _RotationSpeed * Time.deltaTime, 0);
        }
        transform.position += _dir * _movSpeed * Time.deltaTime;
    }

    void JumpMoving()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigid.AddForce(Vector3.up * _jumpForce, ForceMode.Force);
        }
    }
}
