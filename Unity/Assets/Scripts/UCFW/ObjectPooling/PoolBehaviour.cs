using UnityEngine;

namespace UCFW
{
    /// <summary>
    /// An extension of the BetterBehaviour to simplifiy the handling of poolable objects.
    /// </summary>
    public abstract class PoolBehaviour : BetterBehaviour
    {
        private PoolComponent _poolComponent = null;
        public PoolComponent poolComponent { get { return _poolComponent; } }

        void Awake()
        {
            InitBetterBehaviour();

            Transform t = transform;
            while(_poolComponent == null && t != null)
            {
                _poolComponent = t.GetComponent<PoolComponent>();
                t = t.parent;
            }

            if (_poolComponent == null)
            {
                Debug.LogWarning("PoolBehaviour only makes sense on poolable objects");
            }

            _poolComponent.OnSpawn += OnSpawn;
            _poolComponent.OnRecycle += OnRecycle;

            OnAwake();
        }

        void OnDestroy()
        {
            OnRecycle();
            _poolComponent.OnRecycle -= OnRecycle;
            _poolComponent.OnSpawn -= OnSpawn;
            OnDestroyed();
            DeInitBetterBehaviour();
        }

        /// <summary>
        /// Recycle this object
        /// </summary>
        /// <returns></returns>
        public void Recycle()
        {
            _poolComponent.Recycle();
        }

        /// <summary>
        /// This method is called when the object is spawned
        /// </summary>
        protected virtual void OnSpawn() { }

        /// <summary>
        /// This methid is called when the object is recycled
        /// </summary>
        protected virtual void OnRecycle() { }
    }
}
