using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject settingsPanel;
    [SerializeField] float sceneLoadDelay = 2f;
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", sceneLoadDelay));
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
}
