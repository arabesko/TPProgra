using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CambioEscena : MonoBehaviour
{
    public Slider sliderBar;
    public GameObject panelCarga;
    public void LoadScene(string nameScene)
    {
        StartCoroutine(Charge(nameScene));
    }

    IEnumerator Charge(string scene)
    {
        panelCarga.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        while (!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            sliderBar.value = progress;

            yield return new WaitForSeconds(0.01f);
            //yield return null;
        }

        
    }
}
