using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Conexiones")]
    private Rigidbody _rB;
    [SerializeField] private Transform _instancePoin1;
    [SerializeField] private GameObject _bulletPrefab1;
    private Animator _animator;
    [SerializeField] Fruit _fruit;

    [Header("Propiedades player")]
    private float _xAxis;
    private float _zAxis;
    private string _xAxisName = "xAxis";
    private string _zAxisName = "zAxis";
    private string _attack1 = "attack1";
    private string _jump = "jump";
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _energy;
    [SerializeField] private float _mana;
    [SerializeField] private bool _isEnabledToCollect;

    [Header("Propiedades player")]
    private Vector3 _direction;


    private void Awake()
    {
        _rB = this.GetComponent<Rigidbody>();
        _animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("funciona");
            _animator.SetTrigger(_jump);
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Attack1();
            _animator.SetTrigger(_attack1);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_isEnabledToCollect)
            {
                //Animación
                print("si entra");
                Destroy(_fruit.gameObject);
            }
        }

        _xAxis = Input.GetAxis("Horizontal");
        _zAxis = Input.GetAxis("Vertical");

        _animator.SetFloat(_xAxisName, _xAxis);
        _animator.SetFloat(_zAxisName, _zAxis);
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
    }

    private void Attack1()
    {
       Instantiate(_bulletPrefab1, _instancePoin1.position, this.transform.rotation);
    }

    public void Jump()
    {
        _rB.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _fruit = collision.gameObject.GetComponent<Fruit>();
        if (collision.gameObject.layer == 7)
        {
            _isEnabledToCollect = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Fruit _fruit = collision.gameObject.GetComponent<Fruit>();
        if (collision.gameObject.layer == 7)
        {
            _isEnabledToCollect = false;
        }
    }
    public void ModifyEnergy(float ammount)
    {
        if ((_energy + ammount) >= 100)
        {
            _energy = 100;
        }
        else if ((_energy - ammount) <= 0)
        {
            //Muerte
        }
        else
        {
            _energy += ammount;
        }
    }
}
