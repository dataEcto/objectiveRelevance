using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour {


    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    float delay = 1;
    private Queue<string> sentences;
    //Think: John's Sylladex from Homestuck


    private void Awake()
    {
        sentences = new Queue<string>();
    }

    public void startDialogue(dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
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
      
        animator.SetBool("isOpen", false);
        SceneManager.LoadScene("debug 1");
    }

  

}
