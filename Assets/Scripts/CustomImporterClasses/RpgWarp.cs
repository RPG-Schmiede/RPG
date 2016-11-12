using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomImporterRPG
{
    /// <summary>
    /// This class ceate teleporter on a given object or layer
    /// </summary>
    public class RpgWarp : ICustomImportClass
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="customProperties"></param>
        public RpgWarp(GameObject gameObject, bool isObject, string fieldValue, IDictionary<string, string> customProperties = null)
        {
            Handle(gameObject, isObject, fieldValue, customProperties);
        }

        /// <summary>
        /// Handle Properties
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="customProperties"></param>
        public void Handle(GameObject gameObject, bool isObject, string fieldValue, IDictionary<string, string> customProperties)
        {
            Warp warp = gameObject.AddComponent<Warp>();
            warp.warpTargetString = fieldValue;

            Debug.Log(String.Format("rpg:warp - {0} successful set.", gameObject.name));
        }
    }

}