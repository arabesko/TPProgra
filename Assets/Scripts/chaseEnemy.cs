using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using static UnityEngine.GraphicsBuffer;

public class chaseEnemy : MonoBehaviour
{
    public Transform player;
    public float rangoVision;
    public float speed;
    public Vector3[] positions;
    public int index;
    public float rotationSpeed;
    public GameObject explosionEffect;  
    public float explosionRadius = 3f;  
    public int explosionDamage = 1;    

    private bool isChasingPlayer = false;

    void Start()
    {
        //if (positions.Length > 0)
        //{
            //transform.position = positions[0];
        //}
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < rangoVision)
        {
            if (!isChasingPlayer)
            {
                isChasingPlayer = true;
                StartCoroutine(ExplodeAfterDelay(3f));  
            }

            
            Vector3 playerDirection = (player.position - transform.position).normalized;
            playerDirection.y = 0;

            transform.position += playerDirection * speed * Time.deltaTime;

            Vector3 targetDirection = player.position - transform.position;
            transform.rotation = Quaternion.LookRotation(targetDirection);
        }
        else
        {
            if (isChasingPlayer)
            {
                isChasingPlayer = false;
                StopAllCoroutines();  
            }

            
            Vector3 positionDirection = (positions[index] - transform.position).normalized;
            positionDirection.y = 0;

            transform.position = Vector3.MoveTowards(transform.position, positions[index], speed * Time.deltaTime);

            
            Vector3 targetDirection = positions[index] - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, positions[index]) < 0.1f)
            {
                index++;
                if (index >= positions.Length)
                {
                    index = 0;
                }
            }
        }
    }

    private IEnumerator ExplodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (isChasingPlayer)
        {
            Explode();
        }
    }

    private void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);  
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoVision);

        foreach (var position in positions)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(position, 0.2f);
        }

        // Dibujar el radio de la explosión
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}