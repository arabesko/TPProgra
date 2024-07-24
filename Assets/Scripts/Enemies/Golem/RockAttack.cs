using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAttack : MonoBehaviour
{
    private Rigidbody _prefabRB;
    private Vector3 _direction;
    public int _speed = 2000;
    public int damage = 1;
    public char etapaAcutual='A';
    public GameObject fuego;
    private int _timeLife;
    public GameObject fuegoGravedad;
    private bool _isLevelC;
    private bool _canDamage = true;

    private void Awake()
    {
        _prefabRB = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Destroy(this.gameObject, _timeLife);
    }

    private void Update()
    {
        //if (!_isLevelC) return;
        if (_prefabRB.velocity.magnitude <= 0.01f)
        {
            _canDamage = false;
        }
    }

    public void EstadoABC(char etapa)
    {
        etapaAcutual = etapa;

        if (etapaAcutual == 'A')
        {
            _timeLife = 5;
        }
        else if (etapaAcutual == 'B')
        {
            fuego.SetActive(true);
            
            damage = 1;
            _timeLife = 10;
        }
        else if (etapaAcutual == 'C')
        {
            fuego.SetActive(true);
            _timeLife = 15;
            damage = 1;
            _isLevelC = true;
            StartCoroutine(LineaFuego());
        }
    }

    private IEnumerator LineaFuego()
    {
        while (_isLevelC)
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(fuegoGravedad, transform.position, Quaternion.identity);
        }
        yield break;
    }

    public void HaciaElPlayer(Vector3 direction)
    {
        _prefabRB.AddForce(direction * _speed, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_canDamage)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }

}
