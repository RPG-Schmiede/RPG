using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class UnityAnalyticsController : MonoBehaviour
{
    private void Start()
    {
        int totalPotions = 5;
        int totalCoins = 100;
        string weaponID = "Weapon_102";
        Analytics.CustomEvent("gameOver", new Dictionary<string, object>
          {
            { "potions", totalPotions },
            { "coins", totalCoins },
            { "activeWeapon", weaponID }
          });
    }

    private void OnApplicationQuit()
    {
        Analytics.CustomEvent("playTime", new Dictionary<string, object>{{ "time", Time.time }});
    }
}
