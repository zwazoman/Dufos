using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "NewStory", menuName = "Data/Stories", order = 1)]
[System.Serializable]
public class DialogueManager : ScriptableObject
{
    public string SpeakerName;
    public List<string> CharacterDialogue;
    public List<Choice> Choices;
    public SceneEvent Happening;

    [System.Serializable]
    public struct Choice
    {
        public string PlayerChoice;
        public DialogueManager NextStory;
    }

    [System.Serializable]
    public struct SceneEvent
    {
        public string nextSceneName;
        public void Load()
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
