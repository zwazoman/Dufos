using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueOpenerBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _dialogueSystemDisplay;
    [SerializeField]
    private TextMeshProUGUI _speakerName;
    [SerializeField]
    private TextMeshProUGUI _speakerDialogue;
    [SerializeField]
    private List<TextMeshProUGUI> _choiceBoxes;
    [SerializeField]
    private GameObject _nextDialogueButton;

    private DialogueManager _dialogueManager;
    private int index;

    public void DisplayDialogues(DialogueManager story)
    {
        _dialogueManager = story;
        _nextDialogueButton.SetActive(true);
        _dialogueSystemDisplay.SetActive(true);
        _speakerName.text = story.SpeakerName;
        _speakerDialogue.text = story.CharacterDialogue[0];
        index = 0;
    }

    public void NextDialogue()
    {
        if (_dialogueManager != null)
        {
            if ((index + 1) < _dialogueManager.CharacterDialogue.Count)
            {
                index++;
                _speakerDialogue.text = _dialogueManager.CharacterDialogue[index];
            }

            else if (_dialogueManager.Choices.Count > 0)
            {
                for(int i = 0; i < _dialogueManager.Choices.Count; i++)
                {
                    foreach(var box in _choiceBoxes)
                    {
                        box.transform.parent.transform.parent.gameObject.SetActive(true);
                    }

                    _choiceBoxes[i].text = _dialogueManager.Choices[i].PlayerChoice;
                    _nextDialogueButton.SetActive(false);
                }
            }

            else
            {
                _dialogueSystemDisplay.SetActive(false);
            }
        }
    }
}
