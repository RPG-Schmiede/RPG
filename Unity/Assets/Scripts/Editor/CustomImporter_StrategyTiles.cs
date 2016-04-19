using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Tiled2Unity.CustomTiledImporter]
public class CustomImporter_StrategyTiles : Tiled2Unity.ICustomTiledImporter
{
    public void HandleCustomProperties(GameObject gameObject,
        IDictionary<string, string> customProperties)
    {
        if (customProperties.ContainsKey("Terrain"))
        {
            // Add the terrain tile game object
            StrategyTile tile = gameObject.AddComponent<StrategyTile>();
            tile.TileType = customProperties["Terrain"];
            tile.TileNote = customProperties["Note"];
        }

        if(Convert.ToBoolean(customProperties.ContainsKey("warp")))
        {
            Warp warp = gameObject.AddComponent<Warp>();
            warp.warpTarget = GameObject.Find("WarpDestination").transform;
        }
    }

    public void CustomizePrefab(GameObject prefab)
    {
        // Do nothing
    }
}
