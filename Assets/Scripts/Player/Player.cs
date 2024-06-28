using JetBrains.Annotations;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [Header("Conexiones")]
    private Rigidbody _rB;
    [SerializeField] private Transform _instancePoin1;
    [SerializeField] private GameObject _bulletPrefab1;
    private Animator _animator;
    [SerializeField] Collectibles _collect;
    [SerializeField] Inventory _myInventory;
    public Vector3 vectorTest;
    public bool canMove = true;
    public Transform playerMiniMap;
    public TextMeshProUGUI apples;
    public GameObject keyCabra;

    [Header("Propiedades player")]
    private float _xAxis;
    private float _zAxis;
    private string _zAxisName = "zAxis";
    private string _attack1 = "attack1";
    private string _jump = "jump";
    public float _speedRotation = 60f;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _energy;
    [SerializeField] private float _mana;
    [SerializeField] private bool _isEnabledToCollect;
    [SerializeField] private int _InventoryLimit;
    private bool _isInventoryFull;
    public bool _onTheFloor;
    public LayerMask capasDetectables;

    public float anguleRock;
    private AudioSource _audioSource;
    public AudioClip _goatJump;
    public AudioClip _goatDeath;
    public AudioClip _goatDamage;
    public GameplayCanvasManager gamePlayCanvas;
    private GameManager _gameManager;

    [Header("Propiedades player")]
    private Vector3 _direction;

    private void Start()
    {
        playerMiniMap.localScale = new Vector3(8, 8, 8);
    }
    private void Awake()
    {
        _rB = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _myInventory = GetComponent<Inventory>();
        _gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        if(!canMove) return;

        _xAxis = Input.GetAxis("Horizontal");
        _zAxis = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_onTheFloor)
            {
                _animator.SetTrigger(_jump);
                _audioSource.PlayOneShot(_goatJump);
                _onTheFloor = false;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Attack1();
            _animator.SetTrigger(_attack1);
        }

        if (Input.GetKeyDown(KeyCode.E) && _xAxis == 0 && _zAxis == 0)
        {
            if (_isEnabledToCollect)
            {
                canMove = false;
                if (_myInventory.items.Count < _InventoryLimit)
                {
                    _myInventory.AddItems(_collect.element, _collect.life);
                }
                else print("La alforja está llena");

                _animator.SetTrigger("collect");
                if (_myInventory.HasItemsy("llavecabra") == true)
                {
                    keyCabra.SetActive(true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //Recuperar energía
            if (_energy < 10)
            {
                EatApple();
                apples.text = _myInventory.CountApples().ToString();
            }
        }

        //_animator.SetFloat(_xAxisName, _xAxis);
        _animator.SetFloat(_zAxisName, _zAxis);

        if (Input.GetKey(KeyCode.A))
        { 
            transform.Rotate(0, _speedRotation * _xAxis * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, _speedRotation * _xAxis * Time.deltaTime, 0);
        }
    }

    public void AppleColectNumber()
    {
        apples.text = _myInventory.CountApples().ToString();
    }
    public void DeleleteCollectibles()
    {
        if (_myInventory.items.Count <= _InventoryLimit && _isInventoryFull == false)
        {
            Destroy(_collect.gameObject);
            _isEnabledToCollect = false;
            if (_myInventory.items.Count == _InventoryLimit) _isInventoryFull = true;
            canMove = true;
        }
    }

    private void FixedUpdate()
    {
        if (_xAxis != 0 || _zAxis != 0) Movement();
    }

    private void Movement()
    {
        //_direction = (this.transform.right * _xAxis + this.transform.forward * _zAxis) * _speed;
        _direction = (this.transform.forward * _zAxis) * _speed;
        _rB.velocity = new Vector3(_direction.x, _rB.velocity.y, _direction.z);
    }
    // SIN USO MOMENTANEAMENTE
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            _onTheFloor = true;
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
    public void Health(int value)
    {
        _energy += value;

        if (_energy > 10) _energy = 10;
        else _gameManager.GainHP();
    }
    public void TakeDamage(int value)
    {
        _energy -= value;

        _audioSource.PlayOneShot(_goatDamage);

        if (_energy <= 0)
        {
            _energy = 0;
            DeadCondition();
        }
    }
    private void DeadCondition()
    {
        _animator.SetTrigger("isDeath");
        _audioSource.PlayOneShot(_goatDeath);
        gamePlayCanvas.onLose();

        Destroy(GetComponent<Player>(), 1);
    }
    private void EatApple()
    {
        int _countApple = _myInventory.EatApple();

        if(_countApple > 0) Health(_countApple);
    }
}
