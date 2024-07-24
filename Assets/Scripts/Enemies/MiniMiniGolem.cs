using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using static UnityEngine.GraphicsBuffer;

public class MiniMiniGolem : MonoBehaviour
{
    public Transform player;
    public float speed;
    private float life = 20;
    public float rotationSpeed;
    public GameObject explosionEffect;  
    public float explosionRadius = 3f;  
    public int explosionDamage = 1;
    public SkinnedMeshRenderer[] renderers;
    private Material[] _originalMaterial;
    public Material flashMaterial;

    private bool isChasingPlayer = false;

    public Transform punto1;
    public Transform punto2;
    public Transform punto3;
    public Transform punto4;
    public GameObject ball;


    void Start()
    {
        _originalMaterial = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++) 
        {
            _originalMaterial[i] = renderers[i].material;
        }
        StartCoroutine(ExplodeAfterDelay(5));
    }

    void Update()
    {
        Vector3 playerDirection = (player.position - transform.position).normalized;
        playerDirection.y = 0;

        if(Vector3.Distance(transform.position, player.position) > 0.5f)
        {
            transform.position += playerDirection * speed * Time.deltaTime;
        }

        Vector3 targetDirection = player.position - transform.position;
        transform.rotation = Quaternion.LookRotation(targetDirection);
    }

    private IEnumerator ExplodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Explode();
        yield break;
    }

    private void Explode()
    {
        //Instantiate(explosionEffect, transform.position, Quaternion.identity);
        GameObject bullet1 = Instantiate(ball);
        bullet1.GetComponent<MoveSphere>().speed = 5;
        bullet1.transform.position = punto1.position;
        bullet1.GetComponent<MoveSphere>().direction = punto1.position - transform.position;
        
        GameObject bullet2 = Instantiate(ball);
        bullet2.GetComponent<MoveSphere>().speed = 5;
        bullet2.transform.position = punto2.position;
        bullet2.GetComponent<MoveSphere>().direction = punto2.position - transform.position;

        GameObject bullet3 = Instantiate(ball);
        bullet3.GetComponent<MoveSphere>().speed = 5;
        bullet3.transform.position = punto3.position;
        bullet3.GetComponent<MoveSphere>().direction = punto3.position - transform.position;

        GameObject bullet4 = Instantiate(ball);
        bullet4.GetComponent<MoveSphere>().speed = 5;
        bullet4.transform.position = punto4.position;
        bullet4.GetComponent<MoveSphere>().direction = punto4.position - transform.position;

        Destroy(gameObject);  
    }


    public void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.TakeDamage(1);
            Explode();
        }

    }

    public void Damage(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            Death();
        } else
        {
            StartCoroutine("FlashColor");
        }
    }

    public IEnumerator FlashColor()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = flashMaterial;
        }
        yield return new WaitForSeconds(0.07f);
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = _originalMaterial[i];
        }
        yield break;
    }

    void Death()
    {
        Explode();
    }
}