using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] int life;
    [SerializeField] float damage;
    [SerializeField] GameObject VenomBallPrefab;
    [SerializeField] Transform Player;
    [SerializeField] float turningSpeed;
    [SerializeField] float ActionRange;
    [SerializeField] float VisualRange;
    [SerializeField] Transform Rotador;
    [SerializeField] LayerMask DetectableLayers;
    [SerializeField] Transform puntoDeDisparo;

    public float ShootTimer;
    private float _counter;

    void Start()
    {
        Player = FindObjectOfType<Player>().transform;
    }

    
    void Update()
    {
        bool playerInRange = Vector3.Distance(transform.position, Player.position) < VisualRange;
        _counter += Time.deltaTime;

        if(playerInRange == true)
        {
            Vector3 directionToPlayer = (Player.position - Rotador.position).normalized;
            directionToPlayer.y = 0;

            Debug.DrawRay(Rotador.position, directionToPlayer * VisualRange, Color.red);

            if(Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, VisualRange, DetectableLayers))
            {

                if (hit.transform.CompareTag("Player"))
                {
                    TargetToPlayer();
                }

            }
        }
       
    }

    public void Shoot()
    {
        if(_counter >= ShootTimer)
        {
            _counter = 0;
            Instantiate(VenomBallPrefab, puntoDeDisparo.position, puntoDeDisparo.rotation);
        }
    }

    public void TargetToPlayer()
    {
        Vector3 directionToPlayer = (Player.position - Rotador.position).normalized;
        directionToPlayer.y = 0;

        Quaternion desiredRotation = Quaternion.LookRotation(directionToPlayer);
        Rotador.rotation = Quaternion.RotateTowards(Rotador.rotation, desiredRotation, turningSpeed * Time.deltaTime);

        Debug.DrawRay(puntoDeDisparo.position, puntoDeDisparo.forward * ActionRange, Color.blue);

        if (Physics.Raycast(puntoDeDisparo.position, puntoDeDisparo.forward, out RaycastHit hit, ActionRange, DetectableLayers))
        {

            if (hit.transform.CompareTag("Player"))
            {
                Shoot();
            }

        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Rotador.position, ActionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Rotador.position, VisualRange);
    }
}
