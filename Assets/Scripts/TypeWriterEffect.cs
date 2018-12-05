using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField] private float m_typingDelay = 0.2f;
    [SerializeField] private float m_linesDelay = 0.4f;
    [SerializeField] private List<string> m_textLines = new List<string>();

    private string m_currentText = string.Empty;
    private Text m_textBox;

    private int currentLine;
    private int textIndex;


    private void Start()
    {
        m_textBox = GetComponent<Text>();

        StartCoroutine(ShowText());
    }

    private void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            if (textIndex < m_textLines[currentLine].Length)
            {
                textIndex = m_textLines[currentLine].Length;
            }
            else if (currentLine < m_textLines.Count)
            {
                textIndex = 0;
                currentLine++;
            }
            else
            {
                HideDialogueBox();
            }
        }
    }

    private IEnumerator ShowText()
    {
        for (currentLine = 0; currentLine < m_textLines.Count; currentLine++)
        {
            for (textIndex = 0; textIndex <= m_textLines[currentLine].Length; textIndex++)
            {
                m_currentText = m_textLines[currentLine].Substring(0, textIndex);
                m_textBox.text = m_currentText.ToUpper();

                yield return new WaitForSeconds(m_typingDelay);
            }

            yield return new WaitForSeconds(m_typingDelay);
        }
    }

    private void HideDialogueBox()
    {
        Debug.Log("Hiding DialogueBox");
    }
}
