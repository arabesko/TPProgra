using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleStone : MonoBehaviour
{
    private Rigidbody _PrefabRB;
    private Vector3 _direction;
    private int _speed = 400;

    private void Awake()
    {
        _PrefabRB = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _direction = this.transform.forward * _speed;
        _PrefabRB.AddForce(_direction, ForceMode.Force);
        Destroy(this.gameObject, 50);
    }

}
