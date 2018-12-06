using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSpawner : MonoBehaviour
{
    [SerializeField] private List<DialogueTrigger> m_dialogueTriggers;

    private void Start()
    {
        StartCoroutine(StartSpawning());
    }

    private IEnumerator StartSpawning()
    {
        foreach (DialogueTrigger trigger in m_dialogueTriggers)
        {
            trigger.Trigger();
            yield return StartCoroutine(WaitUntilTriggerEnd(trigger));
        }
    }

    private IEnumerator WaitUntilTriggerEnd(DialogueTrigger trigger)
    {
        yield return new WaitUntil(() => !trigger.IsActive());
    }
}
