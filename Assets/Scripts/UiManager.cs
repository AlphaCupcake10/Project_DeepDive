using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    GameManager gameManager;
    DialogueManager dialogueManager;
    public RectTransform HealthFill;
    public GameObject DeathScreen;
    public GameObject PauseMenu;
    public GameObject DialogueScreen;
    public TextMeshProUGUI DialogueName;
    public TextMeshProUGUI DialoguePara;
    // PlayerHealth health;
    
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        dialogueManager = GetComponent<DialogueManager>();
        // health = gameManager.player.GetComponent<PlayerHealth>();
        if(DeathScreen)
            DeathScreen.SetActive(false);
        if(PauseMenu)
            PauseMenu.SetActive(false);
    }

    void Update()
    {
        
    }

}
