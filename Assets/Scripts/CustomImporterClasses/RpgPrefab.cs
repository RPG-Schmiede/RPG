using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomImporterRPG
{
    /// <summary>
    /// This class ceate teleporter on a given object or layer
    /// </summary>
    public class RpgPrefab : ICustomImportClass
    {
        private GameObject NewObject;
        private Vector3 ObjectPosition;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="customProperties"></param>
        public RpgPrefab(GameObject gameObject, bool isObject, string fieldValue, Vector3 objectPosition, IDictionary<string, string> customProperties = null)
        {
            ObjectPosition = objectPosition;

            Handle(gameObject, isObject, fieldValue, customProperties);
        }

        /// <summary>
        /// Handle Properties
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="customProperties"></param>
        public void Handle(GameObject gameObject, bool isObject, string fieldValue, IDictionary<string, string> customProperties)
        {
            // Instanciate
            GameObject asset = Resources.Load<GameObject>(fieldValue);

            if(asset)
            {
                Renderer renderer = gameObject.GetComponentInChildren<Renderer>();
                Collider2D collider = gameObject.GetComponentInChildren<Collider2D>();

                NewObject = GameObject.Instantiate<GameObject>(asset);
                NewObject.transform.parent = gameObject.transform;

                NewObject.transform.position = gameObject.transform.position + ObjectPosition;

                // Add Components
                Renderer cRenderer = NewObject.GetComponentInChildren<Renderer>();

                if (renderer)
                {
                    cRenderer.sortingLayerName = renderer.sortingLayerName;
                    cRenderer.sortingOrder = renderer.sortingOrder;
                }
                else
                {
                    // Customize SortingLayer for Prefab if no component on parent exists
                    if (customProperties != null && customProperties.ContainsKey("rpg:sortingLayerName") && customProperties.TryGetValue("rpg:sortingLayerName", out fieldValue))
                    {
                        if (cRenderer)
                        {
                            // If renderer on object
                            cRenderer.sortingLayerName = fieldValue;
                        }
                    }

                    // -- rpg:sortingOrder --
                    if (customProperties.ContainsKey("rpg:sortingOrder") && customProperties.TryGetValue("rpg:sortingOrder", out fieldValue))
                    {
                        if (cRenderer)
                        {
                            cRenderer.sortingOrder = Convert.ToInt32(fieldValue);
                        }
                    }
                }

                Debug.Log(String.Format("rpg:prefab - {0}/{1} successful instanciated. {2}", gameObject.name, fieldValue, ObjectPosition));
            }
            else
            {
                Debug.LogError(String.Format("rpg:prefab - {0}/{1} failed instanciated. {2}", gameObject.name, fieldValue, ObjectPosition));
            }
        }

        public GameObject GetGameObjectInstance()
        {
            return NewObject;
        }
    }
}