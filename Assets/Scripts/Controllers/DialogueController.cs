using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : Controller
{
    public float speed = 15;
    public float spedUpSpeed = 40;
    public int talkFrequency = 4;
    public AudioClip[] voice;
    public int totalVisibleCharacters = 0;

    private bool _isTyping;
    private int counter = 0;
    private string _name;
    private TextMeshProUGUI nameTextMesh;
    private TextMeshProUGUI messageTextMesh;
    private GameObject nameField;
    private GameObject backdrop;
    private GameObject clickPrompt;
    private AudioSource audioSource;
    private Vector3 _initiatorPosition;

    public void setClickPromptActive(bool state)
    {
        clickPrompt.SetActive(state);
    }

    public void FastForwardTypeWriter()
    {
        speed = spedUpSpeed;
    }

    void Awake()
    {
        nameTextMesh = findChild("Name").GetComponent<TextMeshProUGUI>();
        messageTextMesh = findChild("Message").GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
        nameField = findChild("NameField");
        backdrop = findChild("Backdrop");
        clickPrompt = findChild("ClickPrompt");
        text = "";
    }

    IEnumerator Start()
    {
        clickPrompt.SetActive(false);

        while (true)
        {
            if (text != "" && text != null)
            {
                totalVisibleCharacters = messageTextMesh.textInfo.characterCount;
                int visibleCount = counter % (totalVisibleCharacters + 1);
                messageTextMesh.maxVisibleCharacters = visibleCount;
                if (visibleCount < totalVisibleCharacters)
                {
                    counter += 1;
                    onRevealCharacter();
                    _isTyping = true;
                }
                else _isTyping = false;
            }
            yield return new WaitForSeconds(1 / speed);
        }
    }

    void onRevealCharacter()
    {
        if (counter % Mathf.Floor((talkFrequency * (30 / speed))) == 0 || counter == 1) {  
            audioSource.clip = voice[Mathf.FloorToInt(Random.Range(0, voice.Length))];
            audioSource.Play();
        }
    }

    void awakeTextSetter(string text)
    {
        messageTextMesh.text = "";
        totalVisibleCharacters = 0;

    }

    void Update()
    {
        if (this.text == "" || this.text == null)
        {
            backdrop.SetActive(false);
            return;
        }
        else backdrop.SetActive(true);

        if (this.name == "" || this.name == null) nameField.SetActive(false);
        else nameField.SetActive(true);
    }

    public string text
    {
        get { return messageTextMesh.text; }
        set
        {
            counter = 0;
            messageTextMesh.maxVisibleCharacters = 99999;
            messageTextMesh.text = value;
            messageTextMesh.ForceMeshUpdate();
            messageTextMesh.maxVisibleCharacters = 0;
        }
    }

    public new string name
    {
        get { return _name; }
        set
        {
            _name = value;
            nameTextMesh.text = value;
        }
    }

    public bool isTyping
    {
        get { return (totalVisibleCharacters > messageTextMesh.maxVisibleCharacters); }
    }

    public bool isActive
    {
        get { return backdrop.activeSelf; }
    }

    public Color nameColor
    {
        get { return nameTextMesh.color; }
        set { nameTextMesh.color = value; }
    }

    public Vector3 initiatorPosition
    {
        get { return initiatorPosition; }
        set { initiatorPosition = value; }
    }

    public Color textColor
    {
        get { return messageTextMesh.color; }
        set { messageTextMesh.color = value; }
    }
}
