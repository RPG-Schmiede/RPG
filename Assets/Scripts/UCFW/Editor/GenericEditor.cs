using UnityEngine;

namespace UCFW.Editorscripts
{
    /// <summary>
    /// An extension of the tweakable editor that additionally holds the target in the correct type 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericEditor<T> : TweakableEditor where T : Object
    {
        T _target;

        /// <summary>
        /// The target of the editor
        /// </summary>
        public T specifiedTarget
        {
            get
            {
                if (_target == null)
                {
                    _target = target as T;
                }
                return _target;
            }
        }

        // The awake is written like this for the sake of consistency
        void Awake()
        {
            OnAwake();
        }

        /// <summary>
        /// Override this method instead the Awake callback
        /// </summary>
        protected virtual void OnAwake() { }
    }
}
