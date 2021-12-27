using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintPlayer : MonoBehaviour
{
    private HintController hintController;
    private GameObject hintUI;
    private string button;
    private string text;
    private bool isInQueue;
    private bool isActive;

    void Start()
    {
        hintUI = GameObject.Find("HintUI");
        hintController = hintUI.GetComponent<HintController>();
    }

    private void Update()
    {
        if (!isActive && isInQueue)
        {
            if (hintController.isVisible) return;
            hintController.setHint(button, text);
            hintController.setActive(true);
            isInQueue = false;
            isActive = true;
        }
    }

    public void enableHint(string button, string text)
    {
        if (isInQueue || isActive) return;
        this.button = button;
        this.text = text;
        isInQueue = true;
    }

    public void disableHint()
    {
        if (!isInQueue && !isActive) return;
        if (isInQueue && !isActive)
        {
            isInQueue = false;
            isActive = false;
            return;
        }
        isActive = false;
        hintController.setActive(false);
    }

    public bool isForcedInactive
    {
        get { return !hintUI.activeSelf; }
    }
}
