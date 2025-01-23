using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStory", menuName = "Data/Stories", order = 1)]
[System.Serializable]
public class DialogueManager : ScriptableObject
{
    public string SpeakerName;
    public List<string> CharacterDialogue;
    public List<Choice> Choices;

    [System.Serializable]
    public struct Choice
    {
        public string PlayerChoice;
        public DialogueManager NextStory;
    }
}
