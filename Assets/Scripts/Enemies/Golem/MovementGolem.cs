using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGolem : MonoBehaviour
{
    public Animator ani;

    public GameObject rock;
    public GameObject prefKey;
    public Transform keyPoint;

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

    void Start()
    {
        ani = GetComponent<Animator>();
        golem = FindObjectOfType<Player>().transform;

        _originalMaterial = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            _originalMaterial[i] = renderers[i].material;
        }
    }

    void Update()
    {
        float golemInRange = Vector3.Distance(transform.position, player.position);
        ApuntarAlJugador();
        contador += Time.deltaTime;

        if (golemInRange <= throwRock && golemInRange > 20)
        {
            //Rayo Rojo
            Vector3 direccionAlJugador = (golem.position - transform.position).normalized;
            direccionAlJugador.y = 0.01f;

            Debug.DrawRay(transform.position, direccionAlJugador * throwRock, Color.red);

            if (Physics.Raycast(transform.position, direccionAlJugador, out RaycastHit hit, throwRock, capasDetectables))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    ani.SetBool("ThrowRock", true);
                }
            }
        }
        else if (golemInRange <= attackPunch)
        {
            //print("golpe");
        }

        //Para que lo siga
        if (Vector3.Distance(transform.position, player.position) < vision)
        {
            Vector3 direccionAlJugador = (player.position - transform.position).normalized;
            direccionAlJugador.y = 0;

            transform.position += direccionAlJugador * speed * Time.deltaTime;
            ApuntarAlJugador();
        }

        if(isDeath == true)
        {
            deathCount += Time.deltaTime;
            if(deathCount >= 3)
            {
                //canvas.onWin();
                //Time.timeScale = 0f;
                Instantiate(prefKey, keyPoint.position, keyPoint.rotation);
                Destroy(this.GetComponent<MovementGolem>());
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
        ani.SetBool("ThrowRock", false);

        if (contador >= timer)
        {
            Quaternion rotacionDeseada = Quaternion.LookRotation(-player.position);

            contador = 0;
            Instantiate(rock, puntoRoca.position, puntoRoca.rotation);
        }
    }

    public void Damage(int damage)
    {
        life -= damage;

        StartCoroutine("FlashColor");
        if (life <= 0)
        {
            //Muerte
            ani.SetTrigger("isDeath");
            ActivateBigGolem activateNormalMusic = triggerAudio.GetComponent<ActivateBigGolem>();
            if (activateNormalMusic != null) 
            {
                activateNormalMusic.NormalMusic();
            }
            
            Destroy(this.GetComponent<AngryJump>());
            Destroy(this.GetComponent<Rigidbody>());
            Destroy(this.GetComponent<CapsuleCollider>());
            isDeath = true;
        }
        
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
