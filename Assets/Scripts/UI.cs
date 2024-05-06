using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject[] vidas;


    public void deactivateHP(int indice)
    {
        vidas[indice].SetActive(false);
    }

    public void activateHP(int indice)
    {
        vidas[indice].SetActive(true);
    }
  
}
