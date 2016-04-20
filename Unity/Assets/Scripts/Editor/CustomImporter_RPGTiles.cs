using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Tiled2Unity.CustomTiledImporter]
public class CustomImporter_RPGTiles : Tiled2Unity.ICustomTiledImporter
{
    public void HandleCustomProperties(GameObject gameObject,
        IDictionary<string, string> customProperties)
    {
        if(Convert.ToBoolean(customProperties.ContainsKey("rpg:warp")))
        {
            string value = "";

            if (customProperties.TryGetValue("rpg:warp", out value))
            {
                Warp warp = gameObject.AddComponent<Warp>();
                warp.warpTargetString = value;
            }
        }
    }

    public void CustomizePrefab(GameObject prefab)
    {
        // Do nothing
    }
}
