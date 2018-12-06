using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Text m_speakerName;
    [SerializeField] private Image m_speakerImage;
    [SerializeField] private Text m_dialogueText;
    [SerializeField] private AudioClip m_typingSound;
    [SerializeField] private float m_typingVolume = 0.1f;
    [SerializeField] private Animator m_animator;

    private Queue<string> m_sentences = new Queue<string>();
    private Dialogue m_dialogue;

    private void Update()
    {
        if (Input.GetKeyDown("return") && m_animator.GetBool("IsOpen"))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue i_dialogue)
    {
        m_animator.SetBool("IsOpen", true);
        m_dialogue = i_dialogue;
        m_dialogue.Active = true;
        m_speakerImage.sprite = m_dialogue.SpeakerImage;
        m_speakerName.text = m_dialogue.SpeakerName.ToUpper();

        m_sentences.Clear();

        foreach (string sentence in m_dialogue.Sentences)
        {
            m_sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (m_sentences.Count <= 0)
        {
            m_dialogue.Active = false;
            EndDialogue();
            return;
        }

        string sentence = m_sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string i_sentence)
    {
        m_dialogueText.text = string.Empty;

        foreach (char letter in i_sentence.ToCharArray())
        {
            m_dialogueText.text += letter;
            m_dialogueText.text = m_dialogueText.text.ToUpper();

            if (letter != ' ')
            {
                AudioSource.PlayClipAtPoint(m_typingSound, Camera.main.transform.position, m_typingVolume);
            }

            yield return null;
        }
    }

    private void EndDialogue()
    {
        m_animator.SetBool("IsOpen", false);
    }
}
