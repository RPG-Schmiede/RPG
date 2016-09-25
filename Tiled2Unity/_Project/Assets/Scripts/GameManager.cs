using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class GameManager : MonoBehaviour
{
    public int mapId = 0;
    public GridMovmentController character;

    // SaveGame
    private PlayerSaveGame currentSaveGame;

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
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
            character.Teleport(currentSaveGame.MapPosition);
        }
    }

    /// <summary>
    /// Save current GameState
    /// </summary>
    public void SaveGame()
    {
        // Prepair String
        currentSaveGame = new PlayerSaveGame(mapId, character.GridPosition);
        PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(currentSaveGame));
    }

    #endregion
}

/// <summary>
/// PlayerSaveGame
/// </summary>
public class PlayerSaveGame
{
    public int MapId;
    public Vector2 MapPosition;

    public PlayerSaveGame(int mapId, Vector2 mapPosition)
    {
        MapId = mapId;
        MapPosition = mapPosition;
    }
}
