using System;

namespace UCFW
{ 
    [Serializable]
    public struct GamestateDesc
    {
        public string name;
        public string sceneName;
        public bool reloadRequired;
        public bool preload;
        public UnityEngine.Object sceneObject;
        public SerializableType type;
        public int buildIndex;

        public Gamestate Create()
        {
            return Activator.CreateInstance(type, this) as Gamestate;
        }
    }
}
