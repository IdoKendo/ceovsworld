using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private DialogueManager m_dialogueManager;

    // Use this for initialization
    void Start()
    {
        m_dialogueManager = FindObjectOfType<DialogueManager>();
    }
    
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (m_dialogueManager.Active)
        {
            return;
        }

        if (Input.anyKeyDown)
        {
            float x = transform.position.x + Input.GetAxisRaw("Horizontal");
            float y = transform.position.y + Input.GetAxisRaw("Vertical");

            if (x != transform.position.x || y != transform.position.y)
            {
                Debug.Log(string.Format("{0}, {1}", x, y));

                transform.position = new Vector2(Mathf.Clamp(x, 1, 8), Mathf.Clamp(y, 1, 5));
            }
        }
    }
}
