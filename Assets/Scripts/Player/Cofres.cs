using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofres : MonoBehaviour
{
    [SerializeField] private bool _isInTrunk;
    private Inventory _myInventory;
    public GameObject orbe;
    public Transform instancePoint;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.O))
        {
            if (_isInTrunk)
            {
                if (_myInventory.HasItemsy("llavecabra"))
                {
                    print("El cofre se ha abierto");
                    Instantiate(orbe, instancePoint.position, this.transform.rotation);
                } else
                {
                    print("El cofre está cerrado, necesitas una llave");
                }
            } 
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
}
