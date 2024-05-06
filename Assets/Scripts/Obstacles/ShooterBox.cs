using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBox : MonoBehaviour
{
    public int rotationSpeed = 100;
    public Transform punto1;

    public int damage = 5;

    public bool canShoot;
    public GameObject ball;

    public float timeToShoot = 0.5f;
    public float timeToShootCount;


    void Start()
    {
    }

    void Update()
    {
        //transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (canShoot )
        {
            timeToShootCount += Time.deltaTime;

            if (timeToShootCount > timeToShoot)
            {
                GameObject bullet1 = Instantiate(ball);
                bullet1.GetComponent<MoveSphere>().direction = punto1.position - transform.position;

                bullet1.transform.position = punto1.position;
                timeToShootCount = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            canShoot = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            canShoot = false;
            timeToShootCount = 0;
        }
    }
}
