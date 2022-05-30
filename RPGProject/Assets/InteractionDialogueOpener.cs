using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDialogueOpener : MonoBehaviour
{
    private GameObject dialogueFrame, interactionPrompt;
    private bool promptIsOpen = false, dialogueIsOpen = false;

    void Start()
    {
        dialogueFrame = gameObject.transform.GetChild(1).gameObject;
        interactionPrompt = gameObject.transform.GetChild(0).gameObject;

        dialogueFrame.SetActive(false);
        interactionPrompt.SetActive(false);
    }

    public void setDialogueFrameState(bool openState)
    {
        dialogueFrame.SetActive(openState);
        dialogueIsOpen = openState;
    }

    public void setInteractionPromptState(bool openState)
    {
        interactionPrompt.SetActive(openState);
        promptIsOpen = openState;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && Time.timeScale == 1 && promptIsOpen) 
        {
            dialogueFrame.SetActive(true);
            interactionPrompt.SetActive(false);
            promptIsOpen = false;
            dialogueIsOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.X) && Time.timeScale == 1 && dialogueIsOpen)
        {
            dialogueFrame.SetActive(false);
            dialogueIsOpen = false;
        }
    }
}
