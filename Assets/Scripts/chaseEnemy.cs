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

    public Transform punto1;
    public Transform punto2;
    public Transform punto3;
    public Transform punto4;
    public GameObject ball;

    public bool shaseNow;

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
            shaseNow = true;
        }
            
        if (shaseNow == true)
        {
            StartCoroutine(ExplodeAfterDelay(5f));  

            Vector3 playerDirection = (player.position - transform.position).normalized;
            playerDirection.y = 0;

            transform.position += playerDirection * speed * Time.deltaTime;

            Vector3 targetDirection = player.position - transform.position;
            transform.rotation = Quaternion.LookRotation(targetDirection);
        }
        else
        {
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
        Explode();
        yield break;
    }

    private void Explode()
    {
        //Instantiate(explosionEffect, transform.position, Quaternion.identity);
        GameObject bullet1 = Instantiate(ball);
        bullet1.GetComponent<MoveSphere>().speed = 3;
        bullet1.transform.position = punto1.position;
        bullet1.GetComponent<MoveSphere>().direction = punto1.position - transform.position;
        
        GameObject bullet2 = Instantiate(ball);
        bullet2.GetComponent<MoveSphere>().speed = 3;
        bullet2.transform.position = punto2.position;
        bullet2.GetComponent<MoveSphere>().direction = punto2.position - transform.position;

        GameObject bullet3 = Instantiate(ball);
        bullet3.GetComponent<MoveSphere>().speed = 3;
        bullet3.transform.position = punto3.position;
        bullet3.GetComponent<MoveSphere>().direction = punto3.position - transform.position;

        GameObject bullet4 = Instantiate(ball);
        bullet4.GetComponent<MoveSphere>().speed = 3;
        bullet4.transform.position = punto4.position;
        bullet4.GetComponent<MoveSphere>().direction = punto4.position - transform.position;

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