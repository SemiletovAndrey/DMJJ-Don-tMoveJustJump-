using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI continueText;

    public void UpdateDialogue(string characterName, string phrase)
    {
        Debug.Log($"UpdateDialogue called with Character: {characterName}, Phrase: {phrase}");
        characterNameText.text = characterName;  
        dialogueText.text = phrase;
        Canvas.ForceUpdateCanvases();
    }

    public void ClearDialogue()
    {
        characterNameText.text = string.Empty;   
        dialogueText.text = string.Empty;
    }

    public void ContinueTextOn()
    {
        continueText.gameObject.SetActive(true);
    }
    public void ContinueTextOff()
    {
        continueText.gameObject.SetActive(false);
    }


}
