using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleStone : MonoBehaviour
{
    private Rigidbody _prefabRB;
    private Vector3 _direction;
    private int _speed = 400;

    private void Awake()
    {
        _prefabRB = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _direction = this.transform.forward * _speed;
        _prefabRB.AddForce(_direction, ForceMode.Force);
        Destroy(this.gameObject, 10);
    }

}
