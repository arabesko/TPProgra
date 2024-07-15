using UnityEngine;

public class UI : MonoBehaviour {

    public GameObject[] vidas;

    public void deactivateHP(int indice)
    {
        if (indice < 0) return;
        vidas[indice].SetActive(false);
    }

    public void activateHP(int indice)
    {
        vidas[(indice - 1)].SetActive(true);
    }
}
