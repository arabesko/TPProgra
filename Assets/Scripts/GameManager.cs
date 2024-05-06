using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UI ui;

    private int vidas = 10;
    
    public void LoseHP()
    {
        vidas -= 1;
        ui.deactivateHP(vidas);
    }
    public void GainHP()
    {
        vidas += 1;

        ui.activateHP(vidas);
    }
}
