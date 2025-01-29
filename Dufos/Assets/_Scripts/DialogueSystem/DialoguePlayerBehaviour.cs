using System.Collections;
using TMPro;
using UnityEngine;

public class DialoguePlayerBehaviour : MonoBehaviour
{
    public DialogueState CurrentState { get; private set; }

    [SerializeField]
    private TextMeshProUGUI _speakerDialogue;
    [SerializeField]
    private float _textSpeed;

    public enum DialogueState
    {
        PLAYING,
        COMPLETED
    }

    private void Awake()
    {
        CurrentState = DialogueState.COMPLETED;
    }

    public void PlayDialogue(string dialogue)
    {
        _speakerDialogue.maxVisibleCharacters = dialogue.Length;
        _speakerDialogue.text = dialogue;
        CurrentState = DialogueState.COMPLETED;
    }

    public IEnumerator TypingEffectDialogue(string dialogue)
    {
        if (CurrentState == DialogueState.PLAYING)
        {
            PlayDialogue(dialogue);
        }

        _speakerDialogue.text = dialogue;
        _speakerDialogue.maxVisibleCharacters = 0;
        CurrentState = DialogueState.PLAYING;

        while (CurrentState != DialogueState.COMPLETED)
        {
            _speakerDialogue.maxVisibleCharacters += 1;
            yield return new WaitForSeconds(_textSpeed);
            if (_speakerDialogue.maxVisibleCharacters == dialogue.Length)
            {
                CurrentState = DialogueState.COMPLETED;
                break;
            }
        }
    }
}
