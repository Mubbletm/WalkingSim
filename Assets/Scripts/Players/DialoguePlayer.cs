using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePlayer : MonoBehaviour
{
    [Rename("DialogueTree")]
    public DialogueTree initialDialogue;
    public float defaultSpeed = 15;

    private DialogueTree _dialogue;
    private DialogueController dialogueController;
    private List<Func<bool>> finishEventListeners = new List<Func<bool>>();
    private List<Func<bool>> playEventListeners = new List<Func<bool>>();
    private bool _isPlaying = false;
    private int currentIndex = 0;
    private PlayerMovement playerMovement;

    void Start()
    {
        dialogueController = GameObject.Find("DialogueUI").GetComponent<DialogueController>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!_isPlaying) return;
        bool isTyping = dialogueController.isTyping;
        bool next = Input.GetButtonDown("Next");

        if (!isTyping)
        {
            dialogueController.setClickPromptActive(true);
            if (next)
            {
                currentIndex++;
                if (hasDialogue(currentIndex)) Play();
                else Stop();
            }
            return;
        }
        else dialogueController.setClickPromptActive(false);

        if (isTyping && next) dialogueController.FastForwardTypeWriter();
    }

    public void Play()
    {
        setDialogue(currentIndex);
        _isPlaying = true;
        playerMovement.canMove = false;
        executeEventListeners(playEventListeners);
        playEventListeners = new List<Func<bool>>();
    }

    public void Stop()
    {
        dialogueController.text = null;
        _isPlaying = false;
        playerMovement.canMove = true;
        executeEventListeners(finishEventListeners);
        finishEventListeners = new List<Func<bool>>();
    }

    bool hasDialogue(int index)
    {
        return index < _dialogue.text.Length;
    }

    void setDialogue(int index)
    {
        DialogueTree.Message content = _dialogue.text[index];
        Character character = _dialogue.characters[content.characterIndex];
        dialogueController.text = content.message;
        dialogueController.textColor = character.color;
        dialogueController.name = character.name;
        dialogueController.speed = content.speed;
        dialogueController.voice = character.voice;
    }

    void executeEventListeners(List<Func<bool>> eventListeners)
    {
        eventListeners.ForEach(e => e.Invoke());
    }

    public void onFinish(Func<bool> func)
    {
        finishEventListeners.Add(func);
    }

    public void onStart(Func<bool> func)
    {
        playEventListeners.Add(func);
    }

    public bool isPlaying
    {
        get { return _isPlaying;  }
    }

    public DialogueTree dialogue
    {
        get { return _dialogue; }
        set
        {
            _dialogue = value;
            currentIndex = 0;
        }
    }
}
