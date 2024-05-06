using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Manager : MonoBehaviour
{
    public GameObject[] hp;
    

    public void deactivateHP(int indice)
    {
        hp[indice].SetActive(false);
    }

    public void activateHP(int indice)
    {
        hp[indice].SetActive(true);
    }

    
}
