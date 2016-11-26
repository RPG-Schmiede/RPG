using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerSaveGameController
{
    public PlayerSaveGame current { get; private set; }

    public PlayerSaveGameController()
    {
        
    }

    public void IncreaseStartupCount()
    {
        ++current.startUps;
    }


    public int GetStartUpCount()
    {
        return current.startUps;
    }

    public void SetLanguage(int languageIndex)
    {
        current.language = languageIndex;
    }

    public int GetLanguage()
    {
        return current.language;
    }

    public void SaveData()
    {
        DataStorage.SaveToFile<PlayerSaveGame>(current, StorageMethod.JSON);
    }

    public void ClearData()
    {
        current = new PlayerSaveGame();
        DataStorage.SaveToFile<PlayerSaveGame>(current, StorageMethod.JSON);
    }

    public void LoadData()
    {
        current = new PlayerSaveGame();
        current = DataStorage.LoadFromFile<PlayerSaveGame>(StorageMethod.JSON);

        if(current == null)
            current = new PlayerSaveGame();
    }

    public void EnableTutorial()
    {
        current.showTutorial = true;
    }

    public void DisableTutorial()
    {
        current.showTutorial = false;
    }

    public bool IsTutorialEnabled()
    {
        return current.showTutorial;
    }

    public void IncreaseGamesPlayed()
    {
        current.lastGamePlayed = System.DateTime.Now.Ticks;
        ++current.gamesPlayed;
    }
}