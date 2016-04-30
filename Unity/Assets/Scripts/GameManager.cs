using UnityEngine;
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
        LoadGame();

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
    [SerializeField]
    public string[] InventoryKeys;
    public int[] InventoryValues;

    public PlayerSaveGame(Vector3 mapPosition, Dictionary<string, int> inventory)
    {
        MapPosition = mapPosition;

        InventoryKeys = new string[inventory.Count];
        InventoryValues = new int[inventory.Count];

        int index = 0;

        foreach (KeyValuePair<string, int> item in inventory)
        {
            InventoryKeys[index] = item.Key;
            InventoryValues[index] = item.Value;

            ++index;
        }
    }
}
