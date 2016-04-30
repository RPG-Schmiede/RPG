using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Tiled2Unity;
using CustomImporterRPG;

[Tiled2Unity.CustomTiledImporter]
public class CustomImporter_RPG : Tiled2Unity.ICustomTiledImporter
{
    private List<GameObject> listOfUnusedGameObjects = new List<GameObject>();

    /// <summary>
    /// Interprete custom properties of the MapFile 
    /// </summary>
    /// <param name="gameObject">Map-Layer</param>
    /// <param name="customProperties">Property</param>
    public void HandleCustomProperties(GameObject gameObject, IDictionary<string, string> customProperties)
    {
        bool isObject = gameObject.GetComponent<Collider2D>();
        List<GameObject> instanceObjects = new List<GameObject>();
        string fieldValue = "";

        // --- 1. Handle Properties ---
        
        // -- rpg:prefab --
        if (customProperties.ContainsKey("rpg:prefab"))
        {
            if (customProperties.TryGetValue("rpg:prefab", out fieldValue))
            {
                RpgPrefab _rpgPrefab = null; // new RpgPrefab(gameObject, isObject, fieldValue);

                if (isObject)
                {
                    // - Object -
                    Renderer renderer = gameObject.GetComponentInChildren<Renderer>();
                    Collider2D collider = gameObject.GetComponentInChildren<Collider2D>();

                    _rpgPrefab = new RpgPrefab(gameObject, isObject, fieldValue, gameObject.transform.parent.position, customProperties);
                    GameObject newObject = _rpgPrefab.GetGameObjectInstance();

                    if (newObject)
                    {
                        newObject.transform.parent = gameObject.transform.parent;
                        listOfUnusedGameObjects.Add(gameObject);
                    }

                    // 3. Disable Layer
                    if (renderer)
                        renderer.enabled = false;

                    if (collider)
                        collider.enabled = false; 
                }
                else
                {
                    // - Layer -

                    // 1. Read Layer
                    Renderer renderer = gameObject.GetComponentInChildren<Renderer>();
                    PolygonCollider2D polyCol = gameObject.GetComponentInChildren<PolygonCollider2D>();              

                    // 2. Instance new Objects
                    Vector2[] path;
                    Vector3 objectPosition;
                    string value = "";

                    if (polyCol)
                    {
                        for (int i = 0; i < polyCol.pathCount; i++)
                        {
                            // interprete position
                            path = polyCol.GetPath(i);
                            objectPosition = new Vector3(path[0].x - ((path[0].x - path[1].x) * 0.5f), 
                                                                    path[0].y + ((path[2].y - path[0].y) * 0.5f));
                            _rpgPrefab = new RpgPrefab(gameObject, isObject, fieldValue, objectPosition);
                            instanceObjects.Add(_rpgPrefab.GetGameObjectInstance());
                        }

                        // 3. Disable Layer
                        renderer.gameObject.SetActive(false);
                        polyCol.enabled = false;
                    }
                }
            }
        }

        // -- rpg:warp --
        if (customProperties.ContainsKey("rpg:warp"))
        {
            if (customProperties.TryGetValue("rpg:warp", out fieldValue))
            {
                RpgWarp _rpgWarp = null;

                if (isObject)
                {
                    // - Object -
                    _rpgWarp = new RpgWarp(gameObject, isObject, fieldValue);
                }
                else
                {
                    // - Layer -
                    if (instanceObjects.Count > 0)
                    {
                        // For every prefab instanciated object
                        for (int i = 0; i < instanceObjects.Count; i++)
                        {
                            _rpgWarp = new RpgWarp(instanceObjects[i], isObject, fieldValue);
                        }
                    }
                    else
                    {
                        // On the Collison Object of the layer
                        PolygonCollider2D polyCol = gameObject.GetComponentInChildren<PolygonCollider2D>();

                        if (polyCol)
                        {
                            _rpgWarp = new RpgWarp(polyCol.gameObject, isObject, fieldValue);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Customize the Prefab before saving
    /// </summary>
    /// <param name="prefab"></param>
    public void CustomizePrefab(GameObject prefab)
    {
        for (int i = 0; i < listOfUnusedGameObjects.Count; i++)
        {
            GameObject.DestroyImmediate(listOfUnusedGameObjects[i]);
        }
    }
}
