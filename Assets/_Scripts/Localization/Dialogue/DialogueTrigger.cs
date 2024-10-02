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
        EventBus.OnStartDialogue?.Invoke();
        dialogueUI = container.Resolve<DialogueUI>();
        Debug.Log($"DialogueUI {dialogueUI.gameObject.name}");
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
                // Отображение текста на экране
                DisplayDialogue(sequence.characterNameKey, dialogueText);

                // Ожидание перед воспроизведением следующей фразы
                yield return new WaitForSeconds(delayBetweenPhrases);
            }
        }

        // После завершения диалога
        OnDialogueComplete();
    }

    private void DisplayDialogue(string characterNameKey, string dialogueText)
    {
        Debug.Log($"[{characterNameKey}]: {dialogueText}");
        dialogueUI.UpdateDialogue(characterNameKey, dialogueText);
    }

    // Метод, который срабатывает после завершения всех диалогов
    private void OnDialogueComplete()
    {
        dialogueCoroutine = null;
        Debug.Log("Dialogue sequence completed.");
        EventBus.OnEndDialogue?.Invoke();
    }

}
