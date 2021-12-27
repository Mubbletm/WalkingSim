using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObtainItemController : Controller
{
    public AudioClip soundEffect;

    private GameObject backdrop;
    private TextMeshProUGUI textMesh;
    private Image image;
    private Animator animator;
    private AudioSource audio;

    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        backdrop = findChild("Backdrop");
        animator = backdrop.GetComponent<Animator>();
        textMesh = findChild(backdrop, "NameField").GetComponent<TextMeshProUGUI>();
        image = findChild(backdrop, "ItemSprite").GetComponent<Image>();

        backdrop.SetActive(false);
    }

    public void showObtainedItem(Item item)
    {
        textMesh.text = "Got " + item.name;
        image.sprite = item.sprite;
        backdrop.SetActive(true);
        audio.clip = soundEffect;
        audio.Play();
    }

    public bool isActive()
    {
        return backdrop.activeSelf;
    }
}
