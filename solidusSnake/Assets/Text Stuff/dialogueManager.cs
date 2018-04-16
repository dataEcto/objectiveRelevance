using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour {


    public Text nameText;
    public Text dialogueText;

    //public Animator animator;

    private Queue<string> sentences;

    //Think: John's Sylladex from Homestuck

    // Use this for initialization
    void Start() {

        sentences = new Queue<string>();

    }

    public void startDialogue(dialogue dialogue)
    {
       
        nameText.text = dialogue.name;
        Debug.Log("convo");
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
        SceneManager.LoadScene("debug 1");

       // animator.SetBool("isOpen", false);
    }
  
}
