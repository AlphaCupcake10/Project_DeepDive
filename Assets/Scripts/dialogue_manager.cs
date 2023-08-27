using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class dialogue_manager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text nametext;
    public Text dailtext;
    public Queue<string> sentences;

    public Animator animator;

    public GameObject canvas;
    void Start()
    {

        sentences=new Queue<string>();
        canvas.SetActive(false);
        
    }
    public void startdialogue(obj_dailogue dialogue){
        nametext.text=dialogue.name; // name aap rkhlnea mrko smjh ni aara kya prob aari iss line pr
        canvas.SetActive(true);
        print(dialogue.name);
        sentences.Clear();
        foreach (string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }
        DisplayNext();
    }

    public void DisplayNext(){
        if(sentences.Count==0){
            endDia();
            return;
        }
        string sentence= sentences.Dequeue();
        dailtext.text=sentence;
    }
    void endDia(){
        animator.SetBool("in",true);
        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));
        canvas.SetActive(false);
    }
}
