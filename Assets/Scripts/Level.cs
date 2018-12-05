using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private float m_timeToWait = 3.5f;

    private int m_currentSceneIdx;

    private void Start()
    {
        m_currentSceneIdx = SceneManager.GetActiveScene().buildIndex;

        if (m_currentSceneIdx == 0)
        {
            StartCoroutine(WaitForTime());
        }
    }

    private IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(m_timeToWait);

        LoadNextScene();

    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(m_currentSceneIdx + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
