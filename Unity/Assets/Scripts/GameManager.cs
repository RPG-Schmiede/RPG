using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class GameManager : MonoBehaviour
{
    public Transform player;

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
        LoadGame();
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
            infoTextField.text = "Load Game successfully!";
        }
    }

    /// <summary>
    /// Save current GameState
    /// </summary>
    public void SaveGame()
    {
        // Prepair String
        currentSaveGame = new PlayerSaveGame(player.transform.position);
        PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(currentSaveGame));
        infoTextField.text = "Game successfully saved!";
    }

    #endregion

    public void QuitGame()
    {
        Application.Quit();
    }
}

/// <summary>
/// PlayerSaveGame
/// </summary>
public class PlayerSaveGame
{
    public Vector3 MapPosition;

    public PlayerSaveGame(Vector3 mapPosition)
    {
        MapPosition = mapPosition;
    }
}
