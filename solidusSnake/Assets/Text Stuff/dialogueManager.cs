using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour {


    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    //Think: John's Sylladex from Homestuck

    // Use this for initialization
    void Start() {

        sentences = new Queue<string>();

    }

    public void startDialogue(dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
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
        StopAllCoroutines();
        StartCoroutine(typeSentence(sentence));

    }

    IEnumerator typeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }
    }

    void endDialogue()
    {
        animator.SetBool("isOpen", false);
    }
  
}
