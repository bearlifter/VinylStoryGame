using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractable
{
    public NPC_Dialogue dialogueData;
    public GameObject dialoguePanlel;
    public TMP_Text dialogueText, nameText;
    public Image portaitImage;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    public bool CanInteract()
    {
        Debug.Log("CanInteract llamado");
        return !isDialogueActive;
    }

    public void Interact()
    {
        Debug.Log("Interact llamado");
        if (dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
        {
            Debug.LogWarning("dialogueData es NULL!");
            return;
        }
        if (isDialogueActive)
        {
            Debug.LogWarning("El juego está pausado. No puedes iniciar diálogo");
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }
    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.SetText(dialogueData.npcName);
        portaitImage.sprite = dialogueData.npcPortait;

        dialoguePanlel.SetActive(true);
        PauseController.SetPause(true);

        StartCoroutine(TypeLine());
    }
    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else
        {
            dialogueIndex++;
            if (dialogueIndex < dialogueData.dialogueLines.Length)
            {
                StartCoroutine(TypeLine());
            }
            else
            {
                EndDialogue();
            }
        }
    }
    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");

        foreach (char c in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }
        isTyping = false;

        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }
    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialoguePanlel.SetActive(false);
        PauseController.SetPause(false);
        dialogueText.SetText("");
    }
}
