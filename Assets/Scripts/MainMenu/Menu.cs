using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject panelPrincipal;
    public GameObject panelControles;
    public GameObject panelSonidos;
    public GameObject panelCreditos;
    public GameObject panelCarga;

    private void Start()
    {
        GoToprincipal();
    }
    public void Comenzar(string Startlevel)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Startlevel);
    }

    public void GoToprincipal()
    {
        panelPrincipal.SetActive(true);
        panelControles.SetActive(false);
        panelSonidos.SetActive(false);
        panelCreditos.SetActive(false);
        panelCarga.SetActive(false);
    }
    public void GoToControls()
    {
        panelPrincipal.SetActive(false);
        panelControles.SetActive(true);
        panelSonidos.SetActive(false);
        panelCreditos.SetActive(false);
    }

    public void GoToSounds()
    {
        panelPrincipal.SetActive(false);
        panelControles.SetActive(false);
        panelSonidos.SetActive(true);
        panelCreditos.SetActive(false);
    }

    public void GoToCredits()
    {
        panelPrincipal.SetActive(false);
        panelControles.SetActive(false);
        panelSonidos.SetActive(false);
        panelCreditos.SetActive(true);
    }

    public void Salir()
    {
        Debug.Log("Se cerrará el juego");
        Application.Quit();
        
    }
}
