using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

using TMPro;

public class VisualNovel : MonoBehaviour
{
    public static Action OnAmongUs;


    public enum Entity { Player, Enemy }

    [System.Serializable]
    public class DialogueEntry
    {
        [TextArea(3, 5)] public string text;
        public AudioClip voiceline;
        public UnityEvent OnNext;
    }

    [SerializeField] List<DialogueEntry> mainDialogueLines;
    [SerializeField] List<DialogueEntry> firstOptionLines, secondOptionLines, thirdOptionLines, fourthOptionLines;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] int framesPerCharacter;
    [SerializeField] Button dialogueButton;
    [SerializeField] AudioSource source;
    [SerializeField] AudioSource typingSource;
    [SerializeField] GameObject amongUs;

    List<DialogueEntry> currentBranch;
    int currentLine = 0;
    bool filling = false;

    private void Awake()
    {
        StartDialogue();
    }

    public void StartDialogue()
    {
        source = GetComponent<AudioSource>();
        this.gameObject.SetActive(true);
        currentBranch = mainDialogueLines;
        dialogueButton.Select();
        StartCoroutine(FillText());
    }

    public void Next()
    {
        if(filling)
        {
            StopAllCoroutines();
            filling = false;
            dialogueText.text = currentBranch[currentLine].text;
        }
        else
        {
            currentBranch[currentLine].OnNext?.Invoke();
            dialogueText.text = "";
            if(currentLine + 1 < currentBranch.Count)
            {
                currentLine++;
                StopAllCoroutines();
                StartCoroutine(FillText());
            }
        }
    }

    private IEnumerator FillText()
    {
        filling = true;
        source.Stop();
        source.clip = currentBranch[currentLine].voiceline;
        source.Play();

        foreach(char character in currentBranch[currentLine].text)
        {
            dialogueText.text = dialogueText.text + character;
            typingSource.Stop();
            typingSource.Play();

            for(int i = 0; i < framesPerCharacter; i++)
                yield return null;
        }
        filling = false;
    }

    public void ChooseOption(int index)
    {
        currentLine = 0;
        switch (index)
        {
            case 0:
                currentBranch = firstOptionLines;
                break;
            case 1:
                currentBranch = secondOptionLines;
                break;
            case 2:
                currentBranch = thirdOptionLines;
                break;
            case 3:
                currentBranch = fourthOptionLines;
                break;
            default:
                currentBranch = firstOptionLines;
                break;
        }
        StartCoroutine(FillText());
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void AmongUs()
    {
        print("amogus");
        OnAmongUs?.Invoke();
        // this.gameObject.SetActive(false);
        // amongUs.gameObject.SetActive(true);
    }
}
