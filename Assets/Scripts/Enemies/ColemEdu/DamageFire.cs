using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFire : MonoBehaviour
{
    public int damage = 1;
    public bool _inFireZone;
    public Player player;
    Coroutine _myCoroutine;
    public int lifeTime = 6;
    Rigidbody _rb;
    bool _oneTime = true;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            player = other.gameObject.GetComponent<Player>();
            _inFireZone = true;

            if (_oneTime)
            {
                _myCoroutine = StartCoroutine(InFireZone());
                _oneTime = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            _rb.isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            _inFireZone = false;
            StopCoroutine(_myCoroutine);
            _oneTime = true;
        }
    }

    public IEnumerator InFireZone()
    {
        while (_inFireZone) 
        { 
            yield return new WaitForSeconds(1f);
            player.TakeDamage(damage);
        }
        yield break;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1.67f);
    }

}
