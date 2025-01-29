using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueOpenerBehaviour : MonoBehaviour
{
    public event Action<List<DialogueManager.Choice>> OnChoiceDisplay;

    [SerializeField]
    private GameObject _dialogueSystemDisplay;
    [SerializeField]
    private TextMeshProUGUI _speakerName;
    [SerializeField]
    private List<TextMeshProUGUI> _choiceTexts;
    [SerializeField]
    private GameObject _nextDialogueButton;

    private static DialogueManager _dialogueManager;
    private DialoguePlayerBehaviour _dialoguePlayer;
    private Coroutine _dialogueCoroutine;
    private int index;

    private void Awake()
    {
        _dialoguePlayer = GetComponent<DialoguePlayerBehaviour>();
    }

    public void DisplayDialogues(DialogueManager story)
    {
        if (story == null)
        {
            _dialogueSystemDisplay.SetActive(false);
        }

        else
        {
            _dialogueManager = story;
            _nextDialogueButton.SetActive(true);
            _dialogueSystemDisplay.SetActive(true);
            _speakerName.text = story.SpeakerName;
            if (_dialoguePlayer.CurrentState == DialoguePlayerBehaviour.DialogueState.PLAYING && _dialogueCoroutine != null)
            {
                StopCoroutine(_dialogueCoroutine);
                _dialoguePlayer.PlayDialogue(_dialogueManager.CharacterDialogue[index]);
            }

            else
            {
                _dialogueCoroutine = _dialoguePlayer.StartCoroutine(_dialoguePlayer.TypingEffectDialogue(_dialogueManager.CharacterDialogue[0]));
            }

            index = 0;
        }
    }

    public void NextDialogue()
    {
        if (_dialogueManager != null)
        {
            if (_dialoguePlayer.CurrentState == DialoguePlayerBehaviour.DialogueState.PLAYING && _dialogueCoroutine != null)
            {
                StopCoroutine(_dialogueCoroutine);
                _dialoguePlayer.PlayDialogue(_dialogueManager.CharacterDialogue[index]);
            }

            else if ((index + 1) < _dialogueManager.CharacterDialogue.Count)
            {
                index++;
                _dialogueCoroutine = _dialoguePlayer.StartCoroutine(_dialoguePlayer.TypingEffectDialogue(_dialogueManager.CharacterDialogue[index]));
            }

            else if (_dialogueManager.Choices.Count > 0)
            {
                for(int i = 0; i < _dialogueManager.Choices.Count; i++)
                {
                    _choiceTexts[i].transform.parent.transform.parent.gameObject.SetActive(true);
                    _choiceTexts[i].text = _dialogueManager.Choices[i].PlayerChoice;
                }

                _nextDialogueButton.SetActive(false);

                List<DialogueManager.Choice> paths = new();
                foreach(var choice in _dialogueManager.Choices)
                {
                    paths.Add(choice);
                }

                OnChoiceDisplay(paths);
            }

            else
            {
                _dialogueSystemDisplay.SetActive(false);
            }
        }
    }
}
