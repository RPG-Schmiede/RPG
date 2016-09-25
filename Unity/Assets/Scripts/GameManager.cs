using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform player;
    public InventoryManager inventoryManager;

    public float[] moveFrequenzValues;

    // TODO: Do ObserverPattern
    public Text infoTextField;

    // SaveGame
    private PlayerSaveGame currentSaveGame;

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (PlayerPrefs.GetInt("NewGame") == 1)
        {
            PlayerPrefs.SetInt("NewGame", 0);
        }
        else
        { 
            LoadGame();
        }

        Instance = this;
    }

    #region GameLoop

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            LoadGame();
        }
    }

    #endregion

    #region SaveGame

    /// <summary>
    /// Load current SaveGame
    /// </summary>
    public void LoadGame()
    {
        string _currentSaveGameString = PlayerPrefs.GetString("SaveGame", null);

        if (!System.String.IsNullOrEmpty(_currentSaveGameString))
        {
            currentSaveGame = JsonUtility.FromJson<PlayerSaveGame>(_currentSaveGameString);

            Warp.WarpTo(player, currentSaveGame.MapPosition);

            if(currentSaveGame.InventoryKeys.Length > 0)
            {
                inventoryManager.items = new Dictionary<string, int>();

                for (int i = 0; i < currentSaveGame.InventoryKeys.Length; i++)
                {
                    inventoryManager.items.Add(currentSaveGame.InventoryKeys[i], currentSaveGame.InventoryValues[i]);
                }
            }

            infoTextField.text = "Load Game successfully!";
        }
    }

    /// <summary>
    /// Save current GameState
    /// </summary>
    public void SaveGame()
    {
        // Prepair String
        currentSaveGame = new PlayerSaveGame(player.transform.position, inventoryManager.items);

        Debug.Log(JsonUtility.ToJson(currentSaveGame));
        PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(currentSaveGame));
        infoTextField.text = "Game successfully saved!";
    }

    /// <summary>
    /// Reset SaveGame
    /// </summary>
    public void ResetGame()
    {
        PlayerPrefs.SetString("SaveGame", null);
        PlayerPrefs.Save();
        infoTextField.text = "Clear Game successfully!";

        SceneManager.LoadScene("Game");
    }

    #endregion

    /// <summary>
    /// Quit the App
    /// </summary>
    public void QuitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}

