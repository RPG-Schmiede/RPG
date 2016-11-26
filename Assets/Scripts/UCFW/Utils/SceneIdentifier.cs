using UnityEngine;
using UnityEngine.SceneManagement;

namespace UCFW
{
    // Todo make string assigning work in the editor
    [System.Serializable]
    public class SceneIdentifier
    {
        [SerializeField]
        [HideInInspector]
        string _sceneName = string.Empty;

        [SerializeField]
        [HideInInspector]
        int _buildIndex = -1;

        // Is used to save the scene object in the editor
        [SerializeField]
        [HideInInspector]
        Object _sceneObject = null;


        public bool isValid
        {
            get { return _sceneName != string.Empty; }
        }

        [System.Obsolete("Does not work for now. Use levelName")]
        public int buildIndex
        {
            get { return _buildIndex; }
        }

        public string levelName
        {
            get { return _sceneName; }
        }

        public bool Load()
        {
            if (isValid)
            {
                SceneManager.LoadScene(_sceneName);
                // Useless if case to "disable" compiler warning unused member (_sceneObject is used for editor script)
                if(_sceneObject != null)
                {

                }
            }
            return isValid;
        }

        public bool LoadAsync()
        {
            if (isValid)
            {
                SceneManager.LoadSceneAsync(_sceneName);
            }
            return isValid;
        }

        public static implicit operator string (SceneIdentifier sceneName)
        {
            return sceneName._sceneName;
        }

        public static implicit operator SceneIdentifier(string sceneName)
        {
            SceneIdentifier n = new SceneIdentifier();
            n._sceneName = sceneName;
            return n;
        }
    }
}
