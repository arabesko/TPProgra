using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public GameplayCanvasManager gamePlayCanvas;
    public Transform pointDeathZone;
    public AudioSource audioSource;
    public AudioClip revivirSound;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            //gamePlayCanvas.onLose();
            player.TakeDamage(1);
            player.transform.position = pointDeathZone.position;
            player.transform.rotation = pointDeathZone.rotation;
            audioSource.PlayOneShot(revivirSound);
        }
    }
}
