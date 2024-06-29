using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryJump : MonoBehaviour
{
    [SerializeField] private bool _isFar;
    [SerializeField] private int _timeOutsideMax = 5;
    [SerializeField] private float _timeOutsideMaxCount;

    private AudioSource _audioSource;

    public AudioClip _jumpUngry;

    private Animator _animator;

    public GameObject[] rocks;
    [SerializeField] private List<Vector3> posRocks;

    public Transform LimSupIz;
    public Transform LimInfDer;

    [SerializeField] private int _numRocks = 10;
    [SerializeField] private Transform _player;
    [SerializeField] private float _distance;


    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        _distance = Vector3.Distance(_player.position, transform.position);
        if (_isFar)
        {
            _timeOutsideMaxCount += Time.deltaTime;

            if (_timeOutsideMaxCount >= _timeOutsideMax)
            {
                _animator.SetTrigger("isJumpAttacking");
                _timeOutsideMaxCount = 0;
            }
        }

        if (_distance > 20)
        {
            _isFar = true;
        } 
        else
        {
            _isFar = false;
            _timeOutsideMaxCount = 0;
        }
    }

    public void ResetJump()
    {
        posRocks.Clear();

        _audioSource.PlayOneShot(_jumpUngry);
        _timeOutsideMaxCount = 0;

        
        for (int i = 0; i < (_numRocks - 1); i++)
        {
            float posX = Random.Range(LimSupIz.position.x, LimInfDer.position.x);
            float posY = LimSupIz.position.y;
            float posZ = Random.Range(LimSupIz.position.z, LimInfDer.position.z);

            Vector3 position = new Vector3(posX, posY, posZ);
            posRocks.Add(position);
        }

        foreach (var item in posRocks)
        {
            int indexRock = Random.Range(0, rocks.Length);

            Instantiate(rocks[indexRock], item, rocks[indexRock].transform.rotation);
        }



    }

    public void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.TakeDamage(1);
        }

    }
}
