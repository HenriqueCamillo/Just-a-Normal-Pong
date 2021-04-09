using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class fillText : MonoBehaviour {
    public string text;
    public TextMeshProUGUI dialogueText;
    public int framesPerCharacter;

    public void OnEnable() {
        dialogueText.SetText("");
        StartCoroutine(FillText());
    }

    private IEnumerator FillText() {
        foreach (char character in text) {
            dialogueText.text = dialogueText.text + character;

            for (int i = 0; i < framesPerCharacter; i++)
                yield return null;
        }
    }
}
