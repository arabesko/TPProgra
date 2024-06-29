using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtaculoSubeBaja : MonoBehaviour
{
    [SerializeField] float speedRot;
    public float velSubBaja;
    public Transform puntoA;
    public Transform puntoB;
    private Vector3 targetMove;
    private Vector3 _direction;
    public float tiempoEspera;
    private bool _canSubeBaja = true;
    private bool sube = true;


    private void Start()
    {
        targetMove = puntoB.position;
    }
    private void Update()
    {
        Vector3 rote = new Vector3(0, 1, 0);
        this.transform.Rotate(rote * speedRot * Time.deltaTime);

        if (!_canSubeBaja) return;
        _direction = (targetMove - this.transform.position).normalized;
        this.transform.position = this.transform.position + _direction * velSubBaja * Time.deltaTime;

        if (Vector3.Distance(this.transform.position, targetMove) <= 0.5f)
        {
            if (sube)
            {
                targetMove = puntoA.position;
                sube = false;
            }
            else if (!sube)
            {
                targetMove = puntoB.position;
                sube = true;
            }
            _canSubeBaja = false;
            StartCoroutine("TiempoEspera");
        }
    }

    private IEnumerator TiempoEspera()
    {
        yield return new WaitForSeconds(tiempoEspera);
        _canSubeBaja = true;
        yield break;
    }
}
