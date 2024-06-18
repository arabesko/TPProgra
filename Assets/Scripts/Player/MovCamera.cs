using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 position;
    [SerializeField] int speedRotation;
    [SerializeField] float distance;

    void Start()
    {
        //position = new Vector3(-2, 6, -66);
        //transform.position = new Vector3(position.x, position.y, position.z);
    }

    void Update()
    {
        
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
        /*
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -speedRotation * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            this.transform.Rotate(0, speedRotation * Time.deltaTime, 0);
        }
        */
    }
}
