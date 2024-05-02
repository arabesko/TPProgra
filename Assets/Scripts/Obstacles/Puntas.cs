using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puntas : MonoBehaviour
{
    private int damage = 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.ModifyEnergy(-damage);
        }
    }
}
