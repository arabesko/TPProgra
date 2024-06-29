using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubeBaja : MonoBehaviour
{

    [SerializeField] float speedRot;
    public float velSubBaja;
    public Transform puntoA;
    public Transform puntoB;
    private Transform targetMove;

    private void Start()
    {
        targetMove.position = puntoB.position;
    }

    private void Update()
    {
        Vector3 rote = new Vector3(0, 1, 0);
        this.transform.Rotate(rote * speedRot * Time.deltaTime);



        if (Vector3.Distance(this.transform.position, targetMove.position) < 0.5f)
        {
            if (targetMove == puntoB)
            {
                targetMove.position = puntoA.position;
            }
            else if (targetMove == puntoA)
            {
                targetMove.position = puntoB.position;
            }
        }
        this.transform.position = this.transform.position + new Vector3(0,1,0) * velSubBaja * Time.deltaTime;
    }
}
