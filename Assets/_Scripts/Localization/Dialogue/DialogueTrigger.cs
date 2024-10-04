using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[System.Serializable]
public class DialogueSequence
{
    public string characterNameKey;
    public string dialogueKey;
}

public class DialogueTrigger : MonoBehaviour
{
    public DialogueSequence[] dialogueSequences;
    public float delayBetweenPhrases = 2f;
    private Coroutine dialogueCoroutine;
    private bool isPlayerInZone = false;
    private DialogueUI dialogueUI;

    [Inject] private LocalizationManager localizationManager;
    [Inject] private DiContainer container;
    [Inject] private IEventBus eventBus;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayerInZone)
        {
            isPlayerInZone = true;
            StartDialogue();
        }
    }

    [ContextMenu("Play dialog")]
    private void StartDialogue()
    {
        eventBus.Publish("OnStartDialogue");
        dialogueUI = container.Resolve<DialogueUI>();
        dialogueUI.ClearDialogue();
        if (dialogueCoroutine == null)
        {
            dialogueCoroutine = StartCoroutine(PlayDialogueSequence());
        }
    }

    private IEnumerator PlayDialogueSequence()
    {
        foreach (var sequence in dialogueSequences)
        {
            string dialogueText = localizationManager.GetDialoguePhrase(sequence.characterNameKey, sequence.dialogueKey);

            if (!string.IsNullOrEmpty(dialogueText))
            {
                DisplayDialogue(sequence.characterNameKey, dialogueText);
                yield return new WaitForSeconds(delayBetweenPhrases);
            }
        }
        OnDialogueComplete();
    }

    private void DisplayDialogue(string characterNameKey, string dialogueText)
    {
        Debug.Log($"[{characterNameKey}]: {dialogueText}");
        dialogueUI.UpdateDialogue(characterNameKey, dialogueText);
    }
    private void OnDialogueComplete()
    {
        dialogueCoroutine = null;
        eventBus.Publish("OnEndDialogue");
    }

}
