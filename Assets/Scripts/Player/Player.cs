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
    [SerializeField] Collectibles _collect;
    [SerializeField] Inventory _myInventory;

    [Header("Propiedades player")]
    private float _xAxis;
    private float _zAxis;
    private string _xAxisName = "xAxis";
    private string _zAxisName = "zAxis";
    private string _attack1 = "attack1";
    private string _jump = "jump";
    private float _speedRotation = 3;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _energy;
    [SerializeField] private float _mana;
    [SerializeField] private bool _isEnabledToCollect;
    [SerializeField] private int _InventoryLimit;
    private bool _isInventoryFull;

    [Header("Propiedades player")]
    private Vector3 _direction;


    private void Awake()
    {
        _rB = this.GetComponent<Rigidbody>();
        _animator = this.GetComponent<Animator>();
        _myInventory = this.GetComponent <Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
                if(_myInventory.items.Count < _InventoryLimit)
                {
                    _myInventory.AddItems(_collect.element, _collect.life);
                } 
                else
                {
                    print("La alforja está llena");
                }
                
                _animator.SetTrigger("collect");
            }
        }

        _xAxis = Input.GetAxis("Horizontal");
        _zAxis = Input.GetAxis("Vertical");

        _animator.SetFloat(_xAxisName, _xAxis);
        _animator.SetFloat(_zAxisName, _zAxis);

        this.transform.Rotate(0, Input.GetAxis("Mouse X") * _speedRotation, 0);
    }

    public void DeleleteCollectibles()
    {
        if (_myInventory.items.Count <= _InventoryLimit && _isInventoryFull == false)
        {
            Destroy(_collect.gameObject);
            _isEnabledToCollect = false;
            if (_myInventory.items.Count == _InventoryLimit) _isInventoryFull = true;
        }
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

    private void OnTriggerEnter(Collider other)
    {
        _collect = other.gameObject.GetComponent<Collectibles>();
        if (_collect != null)
        {
            _isEnabledToCollect = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _collect = other.gameObject.GetComponent<Collectibles>();
        if (_collect != null)
        {
            _isEnabledToCollect = false;
            _collect = null;
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
