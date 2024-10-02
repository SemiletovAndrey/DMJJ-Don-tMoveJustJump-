using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private GameObject Controller;
    [SerializeField] private GameObject DialoguePanel;

    private void Start()
    {
        Controller.SetActive(true);
        DialoguePanel.SetActive(false);

        EventBus.OnStartDialogue += OnDialoguePanelOn;
        EventBus.OnEndDialogue += OnControllerOn;
    }

    private void OnDestroy()
    {
        EventBus.OnStartDialogue -= OnDialoguePanelOn;
        EventBus.OnEndDialogue -= OnControllerOn;
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
