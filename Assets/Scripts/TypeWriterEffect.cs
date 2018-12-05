using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField] private float m_typingDelay = 0.2f;
    [SerializeField] private float m_linesDelay = 0.4f;
    [SerializeField] private List<string> m_textLines = new List<string>();
    [SerializeField] private List<Sprite> m_speakers = new List<Sprite>();
    [SerializeField] private int m_changeSpeakerLine = 4;

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
                m_speakerIndex++;
            }

            m_currentSpeaker.sprite = m_speakers[m_speakerIndex];

            for (m_textIndex = 0; m_textIndex <= m_textLines[m_currentLine].Length; m_textIndex++)
            {
                m_currentText = m_textLines[m_currentLine].Substring(0, m_textIndex);
                m_textBox.text = m_currentText.ToUpper();

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
