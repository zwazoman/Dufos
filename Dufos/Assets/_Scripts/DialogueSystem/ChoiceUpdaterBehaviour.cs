using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoiceUpdaterBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _choicesDisplay;
    private DialogueOpenerBehaviour _dialogueOpener;
    private List<DialogueManager.Choice> _choices = new();

    private void Awake()
    {
        _dialogueOpener = GetComponent<DialogueOpenerBehaviour>();
        _dialogueOpener.OnChoiceDisplay += UpdateChoice;
    }

    public void UpdateChoice(List<DialogueManager.Choice> choices)
    {
        _choices.Clear();
        _choices = new(choices);
    }

    public void ChooseStory(TextMeshProUGUI choiceDisplay)
    {
        if (_choices.Count > 0)
        {
            _choicesDisplay[0].transform.parent.gameObject.SetActive(false);

            foreach(var choice in _choices)
            {
                if( choice.PlayerChoice == choiceDisplay.text)
                {
                    _dialogueOpener.DisplayDialogues(choice.NextStory);
                }
            }
        }
    }
}
