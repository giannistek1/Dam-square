using System;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    #region Fields
    public Dialogue dialogue;
    #endregion

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
