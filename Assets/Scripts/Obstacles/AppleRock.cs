using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleRock : MonoBehaviour
{
    public GameObject SkyRockPrefab;
    public AudioSource audioSource;
    public AudioClip rockSound;
    public Transform rockPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            StartCoroutine("RocaSkyActivate");
        }
    }

    public IEnumerator RocaSkyActivate()
    {
        audioSource.PlayOneShot(rockSound);
        Instantiate(SkyRockPrefab, rockPoint.position, rockPoint.rotation);

        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
