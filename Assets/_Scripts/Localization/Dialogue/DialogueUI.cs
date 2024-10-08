using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI continueText;
    [SerializeField] private const float speedTextTyping = 0.05f;
    [SerializeField] private const float _flickingTime = 0.5f;

    private Coroutine _typingCoroutine;
    private Coroutine _flickerContinueTextCoroutine;
    private string _currentPhrase;
    private bool _isTyping = false;

    [Inject] private IEventBus _eventBus;

    public bool IsTyping
    {
        get { return _isTyping; }
    }
    public void UpdateDialogue(string characterName, string phrase)
    {
        ClearDialogue();

        characterNameText.text = characterName;  
        _currentPhrase = phrase;
        if(_typingCoroutine != null )
        {
            StopCoroutine(_typingCoroutine );
        }
        _typingCoroutine = StartCoroutine(TypeSentence(_currentPhrase));
    }

    public void SkipTyping()
    {
        if (_isTyping && _typingCoroutine != null)
        {
            StopCoroutine( _typingCoroutine );
            ContinueTextOn();
            dialogueText.text = _currentPhrase;
            _isTyping = false;
        }
    }

    public void ClearDialogue()
    {
        characterNameText.text = string.Empty;   
        dialogueText.text = string.Empty;
        ContinueTextOff();
    }

    public void ContinueTextOn()
    {
        continueText.gameObject.SetActive(true);
        _flickerContinueTextCoroutine = StartCoroutine(FlickingText());
    }
    public void ContinueTextOff()
    {
        continueText.gameObject.SetActive(false);
        if(_flickerContinueTextCoroutine != null )
        {
            StopCoroutine(_flickerContinueTextCoroutine);
            _flickerContinueTextCoroutine = null;
        }
    }

    public void OnClickDuringDialogue()
    {
        Debug.Log("OnClick");
        _eventBus.Publish("OnClickDuringDialogue");
    }

    private IEnumerator TypeSentence(string sentence)
    {
        _isTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(speedTextTyping);
        }
        ContinueTextOn();
        _isTyping = false;
    }

    private IEnumerator FlickingText()
    {
        while (true)
        {
            continueText.gameObject.SetActive(true);
            yield return new WaitForSeconds(_flickingTime);
            continueText.gameObject.SetActive(false);
            yield return new WaitForSeconds(_flickingTime);
        }
    }
}
