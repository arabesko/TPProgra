using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGolem : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public Quaternion angulo;
    public float grado;

    public GameObject rock;

    public Transform puntoRoca;



    void Start()
    {
        ani = GetComponent<Animator>(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ani.SetTrigger("Attack1");
        }
    }

    public void tirarRocas()
    {
        Instantiate(rock, puntoRoca.position, transform.rotation);
    }
}
