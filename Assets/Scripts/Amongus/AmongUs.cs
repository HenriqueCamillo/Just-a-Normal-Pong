using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using RotaryHeart.Lib.SerializableDictionary;

public class AmongUs : MonoBehaviour
{
    [SerializeField] GameObject messagePrefab, playerMessagePrefab, votePrefab;
    public enum Character { Urexwife, CoolGamer, Cthulhu, DrStonks, uwu, KatanaLover, Roberto }

    [System.Serializable]
    public class Message
    {
        public Character character;
        public bool isVote;
        public string text;
        public float delay;
    }

    [System.Serializable]
    public class CharacterInfo
    {
        public Sprite icon;
        public string name;
    }

    [System.Serializable] public class CharacterDict : SerializableDictionaryBase<Character, CharacterInfo> { }

    [SerializeField] CharacterDict charDict;
    [SerializeField] List<Message> messages;
    [SerializeField] Transform messagesParent;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip messageSFX;
    [SerializeField] TMP_InputField inputF;
    [SerializeField] Animator anim;

    private string playerMessage;
    public string PlayerMessage
    {
        get => playerMessage;
        set
        {
            playerMessage = value;
        }
    }

    private void Awake()
    {
        StartAmongUs();
    }

    public void StartAmongUs()
    {
        this.gameObject.SetActive(true);
    }

    public void StartMessages()
    {
        StartCoroutine(MessagesAnimation());
    }

    private IEnumerator MessagesAnimation()
    {
        foreach(var message in messages)
        {
            if (message.isVote == true) {
                Instantiate(votePrefab, messagesParent).GetComponent<MessageUI>().Fill(message, charDict[message.character]);
            }
            else {
                Instantiate(messagePrefab, messagesParent).GetComponent<MessageUI>().Fill(message, charDict[message.character]);
            }
            audio.PlayOneShot(messageSFX);
            yield return new WaitForSeconds(message.delay);
        }
        anim.SetTrigger("Close");
    }

    public void SendPlayerMessage()
    {
        if (PlayerMessage != "") {
            Instantiate(playerMessagePrefab, messagesParent).GetComponent<MessageUI>().Fill(playerMessage);
            audio.PlayOneShot(messageSFX);
            PlayerMessage = "";
            clear();
        }
    }

    public void clear()
    {
        inputF.Select();
        inputF.text = "";
    }
}