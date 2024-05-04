using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaseEnemy : MonoBehaviour
{
    public Transform player;
    public float rangoVision;
    public float speed;

    public Vector3[] positions;
    public int index;
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) < rangoVision)
        {
            Vector3 playerDirection = (player.position - transform.position).normalized;
            playerDirection.y = 0;

            transform.position += playerDirection*speed*Time.deltaTime;
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
        Gizmos.DrawWireSphere(transform.position, rangoVision);
        
        foreach(var position in positions)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(position, 0.2f);
        }
    }
}
