using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 position;
    [SerializeField] Vector3 diference;
    void Start()
    {
        position = new Vector3(-5, 7, -18);
        transform.position = new Vector3(position.x, position.y, position.z);
        //diference = transform.position - position;
    }

    void Update()
    {
        transform.position = player.transform.position + diference;
    }
}
