using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigJaula : MonoBehaviour
{
    [SerializeField] private bool _isInTrunk;
    private Inventory _myInventory;
    public GameObject _azul;
    private bool _azulOpen;
    public GameObject _rojo;
    private bool _rojoOpen;
    public GameObject _verde;
    private bool _verdeOpen;
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip magicSound;
    public AudioClip noAccessSound;
    private bool onlyOneTime;
    public GameplayCanvasManager gamePlayCanvas;
    public GameObject todo;
    private bool onFinalSound = true;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (_isInTrunk)
            {
                if (_myInventory.HasItemsy("orbe_azul") || _myInventory.HasItemsy("orbe_rojo") || _myInventory.HasItemsy("orbe_verde"))
                {
                    if (_myInventory.HasItemsy("orbe_azul"))
                    {
                        _azul.SetActive(true);
                        _myInventory.DeleteOrbe("orbe_azul");
                        _azulOpen = true;
                    }

                    if (_myInventory.HasItemsy("orbe_rojo"))
                    {
                        _rojo.SetActive(true);
                        _myInventory.DeleteOrbe("orbe_rojo");
                        _rojoOpen = true;
                    }

                    if (_myInventory.HasItemsy("orbe_verde"))
                    {
                        _verde.SetActive(true);
                        _myInventory.DeleteOrbe("orbe_verde");
                        _verdeOpen = true;
                    }
                    audioSource.PlayOneShot(magicSound);
                }
                else
                {
                    audioSource.PlayOneShot(noAccessSound);
                }
            }
        }

        if(_rojoOpen && _azulOpen && _verdeOpen && onFinalSound)
        {
            StartCoroutine("AbrirJaula");
            onFinalSound = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _myInventory = other.GetComponent<Inventory>();
        if (_myInventory != null)
        {
            _isInTrunk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isInTrunk = false;
    }

    private IEnumerator AbrirJaula()
    {
        
        yield return new WaitForSeconds(2);
        audioSource.PlayOneShot(openSound);
        yield return new WaitForSeconds(2);

        Destroy(todo.gameObject);
        Destroy(this.gameObject.GetComponent<BoxCollider>());

        yield return new WaitForSeconds(3);
        if (gamePlayCanvas != null)
        {
            Time.timeScale = 0;
            gamePlayCanvas.onWin();
        }
        
    }
}
