using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBigGolem : MonoBehaviour
{
    public GameObject bigGolem;
    public AudioSource audiNormal;
    public AudioSource audiBigBoss;
    private bool playOnce;
    private bool playOnce2;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            if (playOnce == false) 
            {
                bigGolem.SetActive(true);
                audiNormal.Stop();
                audiBigBoss.Play();
                playOnce = true;
            }
        }
    }

    public void NormalMusic()
    {
        audiBigBoss.Stop();
        audiNormal.Play();
    }

}
