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
    public Vector3 vectorTest;

    [Header("Propiedades player")]
    private float _xAxis;
    private float _zAxis;
    private string _xAxisName = "xAxis";
    private string _zAxisName = "zAxis";
    private string _attack1 = "attack1";
    private string _jump = "jump";
    public float _speedRotation = 1.5f;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _energy;
    [SerializeField] private float _mana;
    [SerializeField] private bool _isEnabledToCollect;
    [SerializeField] private int _InventoryLimit;
    private bool _isInventoryFull;

    public float anguleRock;
    private AudioSource _audioSource;
    public AudioClip _goatJump;
    public AudioClip _goatDeath;
    public AudioClip _goatDamage;
    public GameplayCanvasManager gamePlayCanvas;

    [Header("Propiedades player")]
    private Vector3 _direction;


    private void Awake()
    {
        _rB = this.GetComponent<Rigidbody>();
        _animator = this.GetComponent<Animator>();
        _myInventory = this.GetComponent <Inventory>();
        _audioSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger(_jump);
            _audioSource.PlayOneShot(_goatJump);
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            //Recuperar energía
            if (_energy < 100)
            {
                EatApple();
            }
            else 
            {
                print("Energía a Full");
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
        Quaternion exitAn = Quaternion.Euler(transform.rotation.eulerAngles.x + anguleRock, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
       Instantiate(_bulletPrefab1, _instancePoin1.position, exitAn);
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
        else if ((_energy + ammount) <= 0)
        {
            _animator.SetTrigger("isDeath");
            _audioSource.PlayOneShot(_goatDeath);
            Destroy(this.GetComponent<Player>(), 1);
            gamePlayCanvas.onLose();
        }
        else
        {
            _energy += ammount;
            if (ammount < 0)
            {
                _audioSource.PlayOneShot(_goatDamage);
            }
        }
    }

    private void EatApple()
    {
        ModifyEnergy(_myInventory.EatApple());
    }
}
