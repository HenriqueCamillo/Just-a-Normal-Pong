using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using RotaryHeart.Lib.SerializableDictionary;

public class AmongUs : MonoBehaviour
{
    [SerializeField] GameObject messagePrefab, playerMessagePrefab;
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
            Instantiate(messagePrefab, messagesParent).GetComponent<MessageUI>().Fill(message, charDict[message.character]);
            audio.PlayOneShot(messageSFX);
            yield return new WaitForSeconds(message.delay);
        }
    }

    public void SendPlayerMessage()
    {
        Instantiate(playerMessagePrefab, messagesParent).GetComponent<MessageUI>().Fill(playerMessage);
        audio.PlayOneShot(messageSFX);
    }
}