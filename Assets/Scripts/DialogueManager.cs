using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    Queue<string> CurrentSentences;
    string CurrentSentence;
    string CurrentName;
    bool isActive = false;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CurrentSentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        Debug.Log("Starting Conversation with " + dialogue.Name);
        isActive = true;
        CurrentName = dialogue.Name;
        
        CurrentSentences.Clear();

        foreach(string Sentence in dialogue.Sentences)
        {
            CurrentSentences.Enqueue(Sentence);
        }
        
        DisplayNextSentnce();

    }

    public void DisplayNextSentnce()
    {
        if(CurrentSentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        CurrentSentence = CurrentSentences.Dequeue();
    }

    public void EndDialogue()
    {
        isActive = false;
        Debug.Log("Chup ho gya");
    }

    public bool HasActiveDialogue()
    {
        return isActive;
    }

    public string GetName()
    {
        return CurrentName;
    }
    public string GetPara()
    {
        return CurrentSentence;
    }

}
