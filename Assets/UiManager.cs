using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    GameManager gameManager;
    public RectTransform HealthFill;
    public GameObject DeathScreen;
    public GameObject PauseMenu;
    PlayerHealth health;
    
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        health = gameManager.player.GetComponent<PlayerHealth>();
        if(DeathScreen)
            DeathScreen.SetActive(false);
        if(PauseMenu)
            PauseMenu.SetActive(false);
    }

    void Update()
    {
        if(HealthFill && gameManager.player)
        {
            HealthFill.localScale = Vector3.Slerp(HealthFill.localScale,new Vector3(health.GetHealthRatio(),1,1),.1f);
        }
        if(gameManager.GetGameState() == 1)
        {
            if(DeathScreen)
            DeathScreen.SetActive(true);
            if(PauseMenu)
            PauseMenu.SetActive(false);
        }
        else if (gameManager.GetGameState() == 0)
        {
            if(DeathScreen)
            DeathScreen.SetActive(false);
            if(PauseMenu)
            PauseMenu.SetActive(false);
        }
        else if (gameManager.GetGameState() == 2)
        {
            if(PauseMenu)
            PauseMenu.SetActive(true);
        }
    }

}
