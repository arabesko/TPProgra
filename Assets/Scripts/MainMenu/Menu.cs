using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Comenzar(string Startlevel)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Startlevel);
    }
   public void Salir()
    {
        Debug.Log("Se cerrará el juego");
        Application.Quit();
        
    }
}
