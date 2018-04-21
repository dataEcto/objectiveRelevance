using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour {


    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    public string levelOne;
    public string levelTwo;
    public string credits;
    public string tutorial;

    private Queue<string> sentences;
    //Think: John's Sylladex from Homestuck

    Scene scene;

    private void Awake()
    {
        sentences = new Queue<string>();
        scene = SceneManager.GetActiveScene();
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

        if (scene.name == levelOne)
        {
            SceneManager.LoadScene("debug 1");
        }

        else if (scene.name == levelTwo)
        {
            SceneManager.LoadScene("debug 2");
        }

        else if (scene.name == credits)
        {
            SceneManager.LoadScene("win 2");
        }

        else
        {
            SceneManager.LoadScene("instructions");
        }
    }

  

}
