using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : MonoBehaviour
{
    public Transform player;
    public float vision;
    public float speed;

    public Vector3[] positions;
    public int index;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Vector3.Distance(transform.position, player.position)<vision)
        {
            Vector3 playerDirection = (player.position - transform.position).normalized;
            playerDirection.y = 0;

            transform.position += playerDirection * speed * Time.deltaTime;
        }
       else
        {
            Vector3 positionDirection = (positions[index] - transform.position).normalized;
            positionDirection.y = 0;

            transform.position += positionDirection * speed * Time.deltaTime;
            
        }
       
  
            
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,vision);

        foreach (var positions in positions)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(positions, 0.2f);
        }
    }
}
