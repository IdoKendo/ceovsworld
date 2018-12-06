using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue m_dialogue;
    [SerializeField] private AudioClip m_triggerSound;
    [SerializeField] private float m_triggerVolume = 0.5f;

    public void Trigger()
    {
        PlayTriggerSound();
        TriggerDialogue();
    }

    public bool IsActive()
    {
        return m_dialogue.Active;
    }

    private void PlayTriggerSound()
    {
        if (m_triggerSound != null)
        {
            AudioSource.PlayClipAtPoint(m_triggerSound, Camera.main.transform.position, m_triggerVolume);
        }
    }

    private void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(m_dialogue);
    }
}
