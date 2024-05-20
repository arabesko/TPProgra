using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbe : MonoBehaviour
{
    public Rigidbody rB;
    public int movForce = 5;

    private void Start()
    {
        rB = GetComponent<Rigidbody>();
        rB.AddForce(transform.up * movForce, ForceMode.Impulse);
    }
}
