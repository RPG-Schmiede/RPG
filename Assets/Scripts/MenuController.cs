using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour
{
    [Header("Buttons")]
    public Button ContinueGameButton;
    public Button NewGameButton;
    public Button SettingsButton;

    [Header("Panels")]
    public GameObject ContinueGamePanel;
    public GameObject NewGamePanel;
    public GameObject SettingsPanel;

    [Header("Settings")]
    public string wikiURL = "";

    private void Awake()
    {
        string _currentSaveGameString = PlayerPrefs.GetString("SaveGame", null);

        if (System.String.IsNullOrEmpty(_currentSaveGameString))
        {
            ContinueGameButton.interactable = false;
        }
    }

    public void ShowContinueGame()
    {
        // Open Continue-Window
        SceneManager.LoadScene("Game");
    }

    public void ShowNewGame()
    {
        // Open NewGame-Window
        PlayerPrefs.SetInt("NewGame", 1);
        SceneManager.LoadScene("Game");
    }

    public void ShowSettings()
    {
        // Open Settings-Window
        SettingsPanel.SetActive(true);
    }

    public void ShowWiki()
    {
        // Open Settings-Window
        Application.OpenURL(wikiURL);
    }

    public void QuitGame()
    {
        // Quit App
        Application.Quit();
    }
}
