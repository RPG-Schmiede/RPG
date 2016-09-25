using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Tiled2Unity;

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
        else if(Convert.ToBoolean(customProperties.ContainsKey("rpg:collactable")))
        {
            // --- Read Layer ---
            PolygonCollider2D polyCol = gameObject.GetComponentInChildren<PolygonCollider2D>();
            Renderer renderer = gameObject.GetComponentInChildren<Renderer>();

            Vector2[] path;
            string value = "";

            // --- Instance new Objects ---
            if(polyCol && customProperties.TryGetValue("rpg:itemtype", out value))
            {
                for (int i = 0; i < polyCol.pathCount; i++)
                {
                    path = polyCol.GetPath(i);
                    GameObject newObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(value));
                    newObject.transform.parent = gameObject.transform;
                    newObject.transform.position = gameObject.transform.position + 
                        new Vector3(path[0].x - ((path[0].x - path[1].x) * 0.5f), 
                                    path[0].y + ((path[2].y - path[0].y) * 0.5f));
                    
                    // Add Components
                    SpriteRenderer spriteRenderer = newObject.GetComponent<SpriteRenderer>();
                    spriteRenderer.sortingLayerName = renderer.sortingLayerName;
                    spriteRenderer.sortingOrder = renderer.sortingOrder;
                }
            }

            // --- Disable Layer ---
            polyCol.enabled = false;
            renderer.gameObject.SetActive(false);
        }
    }

    public void CustomizePrefab(GameObject prefab)
    {
        // Do nothing
    }
}
