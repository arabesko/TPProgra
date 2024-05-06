using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class chaseEnemy : MonoBehaviour
{
    public Transform player;
    public float rangoVision;
    public float speed;
    public Vector3[] positions;
    public int index;
    public Transform rotationPoint;
    public float rotationSpeed;
    public float rotationRange;

    void Start()
    {
        
    }

    
    void Update()
    {


        if (Vector3.Distance(transform.position, player.position) < rangoVision)
        {

            Vector3 playerDirection = (player.position - transform.position).normalized;
            playerDirection.y = 0;

            transform.position += playerDirection * speed * Time.deltaTime;
            Vector3 targetDirection = player.position - transform.position;
            transform.rotation = Quaternion.LookRotation(targetDirection);
        }
        else
        {
                           
            transform.rotation = Quaternion.identity;

            Vector3 positionDirection = (positions[index] - transform.position).normalized;
            positionDirection.y = 0;

            transform.position = Vector3.MoveTowards(transform.position, positions[index],speed * Time.deltaTime);

            transform.position += positionDirection * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position,positions[index]) < 0.1f)
            {
                index++;
                if(index >= positions.Length)
                {
                    index = 0;
                }
            }
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
