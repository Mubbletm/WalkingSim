using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class DialogueTree : ScriptableObject
{
    public Character[] characters;
    public Message[] text;

    [System.Serializable]
    public class Message
    {
        public int characterIndex;
        public string message;
        public int speed = 15;
    }
}

[System.Serializable]
public class ConditionedDialogueTree
{
    public DialogueTree dialogue;
    public Condition[] conditions;
    public UnityEvent effects;
}
