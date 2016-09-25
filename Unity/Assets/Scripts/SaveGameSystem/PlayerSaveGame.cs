using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerSaveGame
{
    [SerializeField] public uint gamesPlayed = 0;
    [SerializeField] public long lastGamePlayed = 0;

    [SerializeField] public int language = 0;
    [SerializeField] public int startUps = 0;

    [SerializeField] public bool showTutorial = true;

    [SerializeField] public Vector3 MapPosition;

    [SerializeField] public string[] InventoryKeys;
    [SerializeField] public int[] InventoryValues;

    public PlayerSaveGame()
    {

    }

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
