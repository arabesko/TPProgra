using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject menuPausa;

    [SerializeField] private GameObject panelSonidos;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        
        menuPausa.SetActive(true);
        panelSonidos.SetActive(false);
    }

    public void Reanude()
    {
        Time.timeScale = 1f;
        
        menuPausa.SetActive(false);
        panelSonidos.SetActive(false);
    }

    public void GoToOptions()
    {
        Time.timeScale = 0f;
        
        menuPausa.SetActive(false);
        panelSonidos.SetActive(true);
        
    }

    public void GoTopause()
    {
        Time.timeScale = 0f;
        
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
