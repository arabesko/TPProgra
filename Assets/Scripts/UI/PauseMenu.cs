using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;

    [SerializeField] private GameObject menuPausa;

    [SerializeField] private GameObject panelSonidos;
    public void Pause()
    {
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        panelSonidos.SetActive(false);
    }

    public void Reanude()
    {
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        panelSonidos.SetActive(false);
    }

    public void GoToOptions()
    {
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(false);
        panelSonidos.SetActive(true);
        
    }

    public void GoTopause()
    {
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        panelSonidos.SetActive(false); 
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Cerrar()
    {
        Application.Quit();
    }
}
