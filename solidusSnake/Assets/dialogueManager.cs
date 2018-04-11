using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour {


    public Text nameText;
    public Text dialogueText;

    private Queue<string> sentences;

    //Think: John's Sylladex from Homestuck

    // Use this for initialization
    void Start() {

        sentences = new Queue<string>();

    }

    public void startDialogue(dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        displayNextSentence();
    }

    public void displayNextSentence()
    {
        if (sentences.Count == 0)
        {
            endDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

    }

    void endDialogue()
    {
        Debug.Log("end");
    }
  
}
