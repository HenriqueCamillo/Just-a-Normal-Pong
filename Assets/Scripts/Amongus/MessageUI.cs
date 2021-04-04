using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class MessageUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI name, text;
    [SerializeField] Image icon;

    public void Fill(AmongUs.Message message, AmongUs.CharacterInfo character)
    {
        name.text = character.name;
        icon.sprite = character.icon;
        text.text = message.text;
    }

    public void Fill(string message)
    {
        text.text = message;
    }
}