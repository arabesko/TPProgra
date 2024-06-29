using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] int life = 20;
    [SerializeField] GameObject VenomBallPrefab;
    [SerializeField] Transform Player;
    [SerializeField] float turningSpeed;
    [SerializeField] float UpDownSpeed;
    [SerializeField] float ActionRange;
    [SerializeField] float VisualRange;
    [SerializeField] LayerMask DetectableLayers;
    [SerializeField] Transform puntoDeDisparo;

    public float ShootTimer;
    private float _counter;
    [SerializeField] Vector3[] positions;
    [SerializeField] int index;

    private AudioSource audioSource;
    public AudioClip snakeSound;
    private bool inZoneSound;

    private float currentAngle = 0f;
    private bool rotatingForward = true;
    private const float maxRotationAngle = 140f;

    void Start()
    {
        Player = FindObjectOfType<Player>().transform;
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        bool playerInRange = Vector3.Distance(transform.position, Player.position) < VisualRange;
        _counter += Time.deltaTime;

        if (playerInRange == true)
        {
            //print("Player in range");
            Vector3 directionToPlayer = (Player.position - transform.position).normalized;
            directionToPlayer.y = 0;

            Debug.DrawRay(transform.position, directionToPlayer * VisualRange, Color.red);

            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, VisualRange, DetectableLayers))
            {

                if (hit.transform.CompareTag("Player"))
                {
                    //print("Te veo");
                    TargetToPlayer();
                }
                else
                {
                    //print("No te veo");
                    Spin();
                }

            }
        }
        else
        {
            //print("Player out of range");
            Spin();
            Vector3 directionToPosition = (positions[index] - transform.position).normalized;


            transform.position += directionToPosition * UpDownSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, positions[index]) < 0.1f)
            {
                index++;
            }
            if (index >= positions.Length)
            {
                index = 0;
            }
        }

    }

    private void Spin()
    {
        //transform.Rotate(90, turningSpeed * Time.deltaTime,0);
        float rotationStep = turningSpeed * Time.deltaTime;

        if (rotatingForward)
        {
            currentAngle += rotationStep;
            if (currentAngle >= maxRotationAngle)
            {
                rotatingForward = false;
            }
        }
        else
        {
            currentAngle -= rotationStep;
            if (currentAngle <= 0)
            {
                rotatingForward = true;
            }
        }

        transform.localRotation = Quaternion.Euler(0, currentAngle, 0);

    }

    public void Shoot()
    {
        if (_counter >= ShootTimer)
        {
            _counter = 0;
            Instantiate(VenomBallPrefab, puntoDeDisparo.position, puntoDeDisparo.rotation);
            //print("Te disparo");
        }
    }

    public void TargetToPlayer()
    {
        Vector3 directionToPlayer = (Player.position - transform.position).normalized;
        directionToPlayer.y = 0;

        Quaternion desiredRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, turningSpeed * Time.deltaTime);

        Debug.DrawRay(puntoDeDisparo.position, puntoDeDisparo.forward * ActionRange, Color.blue);

        if (Physics.Raycast(puntoDeDisparo.position, puntoDeDisparo.forward, out RaycastHit hit, ActionRange, DetectableLayers))
        {

            if (hit.transform.CompareTag("Player"))
            {

                Shoot();
            }

        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ActionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, VisualRange);

        foreach (var position in positions)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(position, 0.2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            audioSource.PlayOneShot(snakeSound);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            audioSource.Stop();
        }
    }

    public void Damage(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        this.gameObject.SetActive(false);
        audioSource.Stop();
    }
}

