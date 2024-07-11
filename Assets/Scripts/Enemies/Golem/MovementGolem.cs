using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementGolem : MonoBehaviour
{
    public Animator ani;
    public RawImage barraVidaGolem;
    public GameObject uiBigGolem;
    public GameObject bigExplosion;
    public float distancePlayer;

    public GameObject rock;
    public GameObject prefKey;
    public Transform keyPoint;

    public GameObject humo;
    public GameObject fuego;

    public Transform puntoRoca;
    public Transform player;
    public Transform golem;
    [SerializeField] private int life = 100;

    public float vision;
    public float attackPunch;
    public float throwRock;
    public float velocidadDeGiro;
    public float timer;

    public GameObject triggerAudio;

    private float contador;
    public LayerMask capasDetectables;

    public float speed;

    public Vector3 direccionAlJugador;
    public Vector3 offsetRotation;

    public GameplayCanvasManager canvas;
    public bool isDeath;
    public float deathCount;

    public SkinnedMeshRenderer[] renderers;
    private Material[] _originalMaterial;
    public Material flashMaterial;

    private bool _canMove = true;
    private AudioSource _audioSource;
    public AudioClip explosion;
    

    void Start()
    {
        ani = GetComponent<Animator>();
        golem = FindObjectOfType<Player>().transform;
        _audioSource = GetComponent<AudioSource>();

        _originalMaterial = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            _originalMaterial[i] = renderers[i].material;
        }
    }

    void Update()
    {
        if (!_canMove) return;
        distancePlayer = Vector3.Distance(transform.position, player.position);
        ApuntarAlJugador();
        contador += Time.deltaTime;

        if (distancePlayer <= throwRock && distancePlayer > 20)
        {
            //Rayo Rojo
            Vector3 direccionAlJugador = (golem.position - transform.position).normalized;
            direccionAlJugador.y = 0.01f;

            Debug.DrawRay(transform.position, direccionAlJugador * throwRock, Color.red);

            //if (Physics.Raycast(transform.position, direccionAlJugador, out RaycastHit hit, throwRock, capasDetectables))
            //{
            //    if (hit.transform.CompareTag("Player"))
            //    {
                    ani.SetBool("ThrowRock", true);
            //    }
            //}
        }else
        {
            //Para que lo siga
            if (Vector3.Distance(transform.position, player.position) > attackPunch)
            {
                Vector3 direccionAlJugador = (player.position - transform.position).normalized;
                direccionAlJugador.y = 0;

                transform.position += direccionAlJugador * speed * Time.deltaTime;
                ApuntarAlJugador();
            }
        }


        if(isDeath == true)
        {
            deathCount += Time.deltaTime;
            if(deathCount >= 3)
            {
                //canvas.onWin();
                //Time.timeScale = 0f;

                Instantiate(prefKey, keyPoint.position, prefKey.transform.rotation);
                _canMove = false;
            }
        }
    }


    public void ApuntarAlJugador() 
    {
        Vector3 direccionAlJugador = (player.position - transform.position).normalized;
        direccionAlJugador.y = 0;

        Quaternion rotacionDeseada = Quaternion.LookRotation(direccionAlJugador);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacionDeseada, velocidadDeGiro * Time.deltaTime);

        Debug.DrawRay(transform.position, direccionAlJugador * throwRock, Color.blue);

        if (Physics.Raycast(golem.position, transform.forward, out RaycastHit hit, vision, capasDetectables))
        {
            if (hit.transform.CompareTag("Player"))
            {
                //print("te sigo con la mirada");
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, vision);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackPunch);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, throwRock);
    }

    public void tirarRocas()
    {
        //if (contador >= timer)
        //{
            //Quaternion rotacionDeseada = Quaternion.LookRotation(-player.position);

            //contador = 0;
            GameObject rockAttack = Instantiate(rock, puntoRoca.position, puntoRoca.rotation);
            RockAttack rocky = rockAttack.GetComponent<RockAttack>();
            rocky.HaciaElPlayer((player.transform.position - puntoRoca.position).normalized);
        //}
        ani.SetBool("ThrowRock", false);
    }

    public void Damage(int damage)
    {
        life -= damage;

        StartCoroutine("FlashColor");

        barraVidaGolem.rectTransform.localScale = new Vector3(0.005f * life, 0.3f, 1);

        if (life >= 100)
        {
        } else if (life >= 50)
        {
            speed = 5;
            humo.SetActive(true);
        } else if (life >= 25)
        {
            speed = 7;
            fuego.SetActive(true);
        } else
        {
            //Muerte
            ani.SetTrigger("isDeath");
            ActivateBigGolem activateNormalMusic = triggerAudio.GetComponent<ActivateBigGolem>();
            if (activateNormalMusic != null)
            {
                activateNormalMusic.NormalMusic();
            }

            uiBigGolem.SetActive(false);
            Destroy(this.GetComponent<AngryJump>());
            Destroy(this.GetComponent<Rigidbody>());
            Destroy(this.GetComponent<CapsuleCollider>());
            isDeath = true;
        }

        /*if (life <= 0)
        {
            
        }*/
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

}
