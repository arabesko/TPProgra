using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _movSpeed = 3f;
    [SerializeField] private float _RotationSpeed = 100f;
    [SerializeField] private float _jumpForce = 300;
    private float _energy = 100;

    private float _xAxis;
    private float _yAxis;
    private Vector3 _dir;
    private Rigidbody _myRigid;

    [SerializeField] private LittleStone _littleStone;
    [SerializeField] private GameObject _firePoint1;
    [SerializeField] private Inventory _myInventory;

    private void Start()
    {
        _myRigid = this.GetComponent<Rigidbody>();
        _myInventory = this.GetComponent<Inventory>();
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
        Attack();
    }

    public void EnergyChange(float amount)
    {
        _energy += amount;
        if (_energy <= 0)
        {
            //metodo de muerte
        }
    }

    void Dead()
    {
        //Animacion de muerte
        //Sonido de muerte
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
            _myRigid.AddForce(Vector3.up * _jumpForce, ForceMode.Force);
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LittleStone _stonePF =  Instantiate(_littleStone, _firePoint1.transform.position, this.transform.rotation);
            //_stonePF.transform.rotation = this.transform.rotation;
        }
    }
}
