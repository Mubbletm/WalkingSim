using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialoguePlayer))]
[RequireComponent(typeof(HintPlayer))]
public class NPC : MonoBehaviour
{
    public float interactionRange;
    public float viewingAngle = 10;
    public Character character;
    public ConditionedDialogueTree[] dialogue;

    private GameObject player;
    private DialoguePlayer dialoguePlayer;
    private HintPlayer hintPlayer;

    public void hideNPC()
    {
        hintPlayer.disableHint();
        gameObject.SetActive(false);
    }

    void Start()
    {
        player = GameObject.Find("Player");
        dialoguePlayer = gameObject.GetComponent<DialoguePlayer>();
        hintPlayer = gameObject.GetComponent<HintPlayer>();
    }

    void Update()
    {
        if (isInInteractionRange())
        {
            hintPlayer.enableHint("f", "Interact");
            if (hasInteracted()) startDialogue();
        }
        else hintPlayer.disableHint();
    }

    bool isInInteractionRange()
    {
        return (interactionRange >= Vector3.Distance(gameObject.transform.position, player.transform.position)) &&
            (Vector3.Angle(player.transform.forward, transform.position - player.transform.position) < viewingAngle);
    }

    bool hasInteracted()
    {
        return Input.GetButtonDown("Interact") && !hintPlayer.isForcedInactive;
    }

    void startDialogue()
    {
        for (int i = 0; i < dialogue.Length; i++)
        {
            bool fullfillsConditions = true;
            foreach (Condition condition in dialogue[i].conditions) fullfillsConditions = fullfillsConditions && condition.Invoke(0);
            if (fullfillsConditions)
            {
                dialoguePlayer.dialogue = dialogue[i].dialogue;
                dialoguePlayer.onFinish(() => { dialogue[i].effects.Invoke(); return true; });
                dialoguePlayer.Play();
                return;
            }
        }
    }
}
