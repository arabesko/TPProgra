using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSphere : MonoBehaviour
{
    public int speed = 1;
    public Vector3 direction = new Vector3(0, 0, 0);
    void Start()
    {
        Destroy(gameObject, 10);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
