using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UI ui;

    private int vidas = 10;
    
    public void loseHP()
    {
        vidas -= 1;
        ui.deactivateHP(vidas);
    }
    
}
