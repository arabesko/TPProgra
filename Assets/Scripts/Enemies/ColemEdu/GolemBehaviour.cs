using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemBehaviour : MonoBehaviour
{
    public RawImage barraVidaGolem;
    public GameObject uiBigGolem;
    public GameObject bigExplosion;
    [SerializeField] private float _distancePlayer;

    public GameObject rock;
    public GameObject prefKey;
    public Transform keyPoint;

    public GameObject miniGolem;
    private GameObject _miniG;
    private MiniMiniGolem _miniGScript;
    public Transform puntoMiniGolem1;
    public Transform puntoMiniGolem2;
    public Transform puntoMiniGolem3;
    private bool _canEsbirro = true;

    public GameObject humo;
    public GameObject fuego;

    public Transform puntoRoca;
    public Transform player;
    [SerializeField] private float maxLife = 200;
    [SerializeField] private float life;

    public float attackPunch;
    public float throwRock;
    public float velocidadDeGiro;
    [SerializeField] private float _timer;

    public GameObject triggerAudio;

    public LayerMask capasDetectables;

    public float speed;

    public GameplayCanvasManager canvas;
    public bool isDeath;
    public float deathCount;

    public SkinnedMeshRenderer[] renderers;
    private Material[] _originalMaterial;
    public Material flashMaterial;

    private bool _canMove = true;
    private AudioSource _audioSource;
    public AudioClip explosion;
    public AudioClip nearAttack;


    [SerializeField] private bool _isFar;
    [SerializeField] private int _timeOutsideMax = 5;
    [SerializeField] private float _timeOutsideMaxCount;

    public AudioClip _jumpUngry;

    private Animator _animator;

    public GameObject[] rocks;
    [SerializeField] private List<Vector3> posRocks;

    public Transform LimSupIz;
    public Transform LimInfDer;

    [SerializeField] private int _numRocks = 10;
    [SerializeField] private Transform _player;

    private Vector3 _directionPlayer;
    private Quaternion _rotationGolem;



    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();

        _originalMaterial = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            _originalMaterial[i] = renderers[i].material;
        }

        life = maxLife;
    }

    void Update()
    {
        if (!_canMove) return;

        _distancePlayer = Vector3.Distance(transform.position, player.position);
        _directionPlayer = (player.position - transform.position).normalized;
        _directionPlayer.y = 0;

        MirarPlayer();
        _timer += Time.deltaTime;

        if (_distancePlayer > throwRock)
        {
            if (_timer > 8)
            {
                //Jump Attack
                _animator.SetTrigger("isJumpAttacking");
            }
            else 
            {
                //caminar
                CaminarAlPlayer();
            }
        } 
        else if (_distancePlayer <= throwRock && _distancePlayer > attackPunch)
        {
            if (_timer > 5)
            {
                //Throw Attack
                _animator.SetBool("ThrowRock", true);
            } 
            else
            {
                //Caminar
                CaminarAlPlayer();
            }
        } else if (_distancePlayer <= attackPunch)
        {
            if (_timer > 4 && _canEsbirro)
            {
                //Ataque cercano
                _audioSource.PlayOneShot(nearAttack);
                _animator.SetTrigger("nearAttack");
                StartCoroutine(Esbirros());
                _canEsbirro = false;
            }
            else
            {
                //Caminar
                CaminarAlPlayer();
            }
        }

        if (isDeath == true)
        {
            deathCount += Time.deltaTime;
            if (deathCount >= 3)
            {
                Instantiate(prefKey, keyPoint.position, prefKey.transform.rotation);
                _canMove = false;
            }
        }
    }

    public IEnumerator Esbirros()
    {
        int veces = 0;
        if (life <= maxLife && life > maxLife * 0.5f)
        {
            veces = 1;
        }
        else if (life <= maxLife * 0.5f && life > maxLife * 0.25f)
        {
            veces = 2;
        }
        else if (life <= maxLife * 0.25f)
        {
            veces = 3;
        }

        for (int i = 0; i < veces; i++) 
        {
            _miniG = Instantiate(miniGolem, puntoMiniGolem1.position, puntoMiniGolem1.rotation);
            _miniGScript = _miniG.GetComponent<MiniMiniGolem>();
            _miniGScript.player = player;
            _miniGScript.speed = Random.Range(7, 11);

            _miniG = Instantiate(miniGolem, puntoMiniGolem2.position, puntoMiniGolem2.rotation);
            _miniGScript = _miniG.GetComponent<MiniMiniGolem>();
            _miniGScript.player = player;
            _miniGScript.speed = Random.Range(7, 11);

            _miniG = Instantiate(miniGolem, puntoMiniGolem3.position, puntoMiniGolem3.rotation);
            _miniGScript = _miniG.GetComponent<MiniMiniGolem>();
            _miniGScript.player = player;
            _miniGScript.speed = Random.Range(7, 11);

            yield return new WaitForSeconds(1f);
        }
        yield break;
    }

    public void RenovarTimer()
    {
        _timer = 0;
        _canEsbirro = true;
    }
    public void ThrowRockSound()
    {
        _audioSource.PlayOneShot(_jumpUngry);
    }
    private void CaminarAlPlayer() 
    {
        if (Vector3.Distance(transform.position, player.position) > 2)
        {
            transform.position += _directionPlayer * speed * Time.deltaTime;
        }
    }

    public void MirarPlayer()
    {
        _rotationGolem = Quaternion.LookRotation(_directionPlayer);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _rotationGolem, velocidadDeGiro * Time.deltaTime);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackPunch);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, throwRock);
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

    public void TirarRocas()
    {
        _audioSource.PlayOneShot(_jumpUngry);
        GameObject rockAttack = Instantiate(rock, puntoRoca.position, puntoRoca.rotation);
        RockAttack rocky = rockAttack.GetComponent<RockAttack>();
        rocky.HaciaElPlayer((player.transform.position - puntoRoca.position).normalized);

        if (life <= maxLife && life > maxLife * 0.5f)
        {
            rocky.EstadoABC('A');
        }
        else if (life <= maxLife * 0.5f && life > maxLife * 0.25f)
        {
            rocky.EstadoABC('B');
        }
        else if (life <= maxLife * 0.25f)
        {
            rocky.EstadoABC('C');
        }
        _animator.SetBool("ThrowRock", false);
    }

    public void Damage(int damage)
    {
        life -= damage;

        StartCoroutine("FlashColor");

        //1 --> maxLife
        //x    --> life
        // x = (1 * life) / maxLife

        barraVidaGolem.rectTransform.localScale = new Vector3(life / maxLife, 0.3f, 1);

        if (life <= maxLife && life > maxLife * 0.5f)
        {
        }
        else if (life <= maxLife * 0.5f && life > maxLife * 0.25f)
        {
            speed = 4;
            humo.SetActive(true);
        }
        else if (life <= maxLife * 0.25f && life > 0)
        {
            speed = 5;
            fuego.SetActive(true);
        }
        else if (life <= 0)
        {
            //Muerte
            _animator.SetTrigger("isDeath");
            ActivateBigGolem activateNormalMusic = triggerAudio.GetComponent<ActivateBigGolem>();
            if (activateNormalMusic != null)
            {
                activateNormalMusic.NormalMusic();
            }

            uiBigGolem.SetActive(false);
            Destroy(this.GetComponent<Rigidbody>());
            Destroy(this.GetComponent<CapsuleCollider>());
            isDeath = true;
        }
    }

    public void BigExplosion()
    {
        StartCoroutine(Morir());
    }

    public IEnumerator Morir()
    {
        yield return new WaitForSeconds(3);
        bigExplosion.SetActive(true);
        _audioSource.PlayOneShot(explosion);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public IEnumerator FlashColor()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = flashMaterial;
        }
        yield return new WaitForSeconds(0.07f);
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = _originalMaterial[i];
        }
        yield break;
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
