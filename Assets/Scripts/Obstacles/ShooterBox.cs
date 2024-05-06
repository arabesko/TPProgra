using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBox : MonoBehaviour
{
    public int rotationSpeed = 200;
    public Transform punto1;
    public Transform punto2;
    public Transform punto3;
    public Transform punto4;

    public Transform puntoA;
    public Transform puntoB;
    public int speedMoveUpDown = 10;

    public int damage = 5;

    public bool canShoot;
    public GameObject ball;

    public float timeToShoot = 0.5f;
    public float timeToShootCount;

    public bool moveUP;
    public bool moveDOWN;


    void Update()
    {
        if (moveUP && transform.position.y < puntoB.position.y)
        {
            MoveToUp();
            if (transform.position.y >= puntoB.position.y)
            {
                moveUP = false;
                canShoot = true;
            }
        }

        if (moveDOWN && transform.position.y > puntoA.position.y)
        {
            MoveToDown();
            if (transform.position.y <= puntoA.position.y)
            {
                moveDOWN = false;
            }
        }

        if (canShoot )
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            timeToShootCount += Time.deltaTime;

            if (timeToShootCount > timeToShoot)
            {
                
                GameObject bullet1 = Instantiate(ball);
                bullet1.transform.position = punto1.position;
                bullet1.GetComponent<MoveSphere>().direction = punto1.position - transform.position;

                GameObject bullet2 = Instantiate(ball);
                bullet2.transform.position = punto2.position;
                bullet2.GetComponent<MoveSphere>().direction = punto2.position - transform.position;

                GameObject bullet3 = Instantiate(ball);
                bullet3.transform.position = punto3.position;
                bullet3.GetComponent<MoveSphere>().direction = punto3.position - transform.position;

                GameObject bullet4 = Instantiate(ball);
                bullet4.transform.position = punto4.position;
                bullet4.GetComponent<MoveSphere>().direction = punto4.position - transform.position;

                timeToShootCount = 0;
            }
        }
    }

    private void MoveToUp()
    {
        transform.position += Vector3.up * Time.deltaTime * speedMoveUpDown;
    }

    private void MoveToDown()
    {
        transform.position += -Vector3.up * Time.deltaTime * speedMoveUpDown;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            moveUP = true;
            moveDOWN = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            moveUP = false;
            moveDOWN = true;
            canShoot = false;
            timeToShootCount = 0;
        }
    }
}
