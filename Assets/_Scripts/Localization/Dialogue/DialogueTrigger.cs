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

    private Coroutine _dialogueCoroutine;
    private bool _isPlayerInZone = false;
    private DialogueUI _dialogueUI;

    private int currentDialogueIndex = 0;
    private bool waitingForNextPhrase = false;

    [Inject] private LocalizationManager _localizationManager;
    [Inject] private DiContainer _container;
    [Inject] private IEventBus _eventBus;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_isPlayerInZone)
        {
            _isPlayerInZone = true;
            StartDialogue();
        }
    }

    [ContextMenu("Play dialog")]
    private void StartDialogue()
    {
        _eventBus.Publish("OnStartDialogue");
        _dialogueUI = _container.Resolve<DialogueUI>();
        if (_dialogueCoroutine == null)
        {
            _dialogueCoroutine = StartCoroutine(PlayDialogueSequence());
            _eventBus.Subscribe("OnClickDuringDialogue", OnScreenClick);
        }
    }

    private IEnumerator PlayDialogueSequence()
    {
        while (currentDialogueIndex < dialogueSequences.Length)
        {
            var sequence = dialogueSequences[currentDialogueIndex];
            string dialogueText = _localizationManager.GetDialoguePhrase(sequence.characterNameKey, sequence.dialogueKey);

            if (!string.IsNullOrEmpty(dialogueText))
            {
                waitingForNextPhrase = true;
                DisplayDialogue(sequence.characterNameKey, dialogueText);
                yield return new WaitUntil(() => !waitingForNextPhrase);
            }

            yield return null;
        }

        OnDialogueComplete();
    }

    public void OnScreenClick()
    {
        if (_dialogueUI.IsTyping)
        {
            _dialogueUI.SkipTyping();
        }
        else
        {
            _dialogueUI.ContinueTextOff();
            waitingForNextPhrase = false;
            currentDialogueIndex++;
        }
    }

    private void DisplayDialogue(string characterNameKey, string dialogueText)
    {
        Debug.Log($"[{characterNameKey}]: {dialogueText}");
        _dialogueUI.UpdateDialogue(characterNameKey, dialogueText);
    }
    private void OnDialogueComplete()
    {
        _dialogueCoroutine = null;
        _eventBus.Publish("OnEndDialogue");
        _eventBus.Unsubscribe("OnClickDuringDialogue", OnScreenClick);
    }

}
