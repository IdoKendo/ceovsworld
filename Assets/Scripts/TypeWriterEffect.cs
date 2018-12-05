using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour
{
    [Header("Timing")]
    [SerializeField] private float m_typingDelay = 0.2f;
    [SerializeField] private float m_linesDelay = 0.4f;
    [SerializeField] private int m_changeSpeakerLine = 4;
    [Header("Content")]
    [SerializeField] private List<string> m_textLines = new List<string>();
    [SerializeField] private List<Sprite> m_speakers = new List<Sprite>();
    [Header("Sounds")]
    [SerializeField] private AudioClip m_typingSound;
    [SerializeField] private float m_typingVolume = 0.5f;
    [SerializeField] private AudioClip m_changeSpeakersSound;
    [SerializeField] private float m_changeSpeakersVolume = 0.5f;

    private string m_currentText = string.Empty;
    private Text m_textBox;
    private Image m_currentSpeaker;
    private Image m_dialogueBox;
    private int m_currentLine;
    private int m_textIndex;
    private int m_speakerIndex;

    private void Start()
    {
        m_textBox = GetComponent<Text>();
        m_currentSpeaker = GetComponentInChildren<Image>();
        m_dialogueBox = GetComponentInParent<Image>();

        StartCoroutine(ShowText());
    }

    private void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            if (m_textIndex <= m_textLines[m_currentLine].Length - 3)
            {
                m_textIndex = m_textLines[m_currentLine].Length - 3;
            }
        }
    }

    private IEnumerator ShowText()
    {
        for (m_currentLine = 0; m_currentLine < m_textLines.Count; m_currentLine++)
        {
            if (m_currentLine == m_changeSpeakerLine)
            {
                AudioSource.PlayClipAtPoint(m_changeSpeakersSound, Camera.main.transform.position, m_changeSpeakersVolume);
                m_speakerIndex++;
            }

            m_currentSpeaker.sprite = m_speakers[m_speakerIndex];

            for (m_textIndex = 0; m_textIndex <= m_textLines[m_currentLine].Length; m_textIndex++)
            {
                m_currentText = m_textLines[m_currentLine].Substring(0, m_textIndex);
                m_textBox.text = m_currentText.ToUpper();

                if (m_textIndex != m_textLines[m_currentLine].Length && m_textLines[m_currentLine][m_textIndex] != ' ')
                {
                    AudioSource.PlayClipAtPoint(m_typingSound, Camera.main.transform.position, m_typingVolume);
                }

                yield return new WaitForSeconds(m_typingDelay);
            }

            yield return new WaitForSeconds(m_linesDelay);
        }

        HideDialogueBox();
    }

    private void HideDialogueBox()
    {
        Destroy(m_dialogueBox);
        Destroy(m_currentSpeaker);
        Destroy(gameObject);
    }
}
