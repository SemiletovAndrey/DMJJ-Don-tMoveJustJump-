using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HUDController : MonoBehaviour
{
    [SerializeField] private GameObject Controller;
    [SerializeField] private GameObject DialoguePanel;

    [Inject] private IEventBus _eventBus;

    private void Start()
    {
        Controller.SetActive(true);
        DialoguePanel.SetActive(false);
    }
    private void OnEnable()
    {
        _eventBus.Subscribe("OnStartDialogue", OnDialoguePanelOn);
        _eventBus.Subscribe("OnEndDialogue", OnControllerOn);
    }

    private void OnDisable()
    {
        _eventBus.Unsubscribe("OnStartDialogue", OnDialoguePanelOn);
        _eventBus.Unsubscribe("OnEndDialogue", OnControllerOn);
    }

    public void OnDialoguePanelOn()
    {
        Controller.SetActive(false);
        DialoguePanel.SetActive(true);
    }
    
    public void OnControllerOn()
    {
        Controller.SetActive(true);
        DialoguePanel.SetActive(false);
    }
}
