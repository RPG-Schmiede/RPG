using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CustomImporterRPG
{
    public interface ICustomImportClass
    {
        void Handle(GameObject gameObject, bool isObject, string fieldValue, IDictionary<string, string> customProperties);
    }
}