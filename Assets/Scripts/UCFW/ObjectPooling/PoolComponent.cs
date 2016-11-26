using System;

namespace UCFW
{
    /// <summary>
    /// The component that enables objectpooling for a gameobject
    /// </summary>
    public class PoolComponent : BetterBehaviour
    {
        ObjectPool _pool = null;

        /// <summary>
        /// A event  that is triggered when the object is spawned
        /// </summary>
        public event Action OnSpawn;

        /// <summary>
        /// A event tha is triggered when the object is recycled
        /// </summary>
        public event Action OnRecycle;

        public ObjectPool pool { get { return _pool; } }

        protected override void OnAwake()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Initializes the PoolComponent. Is called internally
        /// </summary>
        /// <param name="pool">the pool for this game object</param>
        /// <returns></returns>
        internal bool Init(ObjectPool pool)
        {
            if(_pool == null)
            {
                _pool = pool;
                return pool != null;
            }
            else
            {
                return false;
            }
        }
            

        /// <summary>
        /// Spawn this object. Is called the objectpool. Do not call it by yourself
        /// </summary>
        /// <returns></returns>
        internal bool Spawn()
        {
            OnSpawn.SafeCall();

            return true;
        }

        /// <summary>
        /// Recycle this object
        /// </summary>
        /// <returns></returns>
        public bool Recycle()
        { 
            if (gameObject.activeSelf)
            {
                if (!_pool.Recycle(this))
                {
                    return false;
                }
            }

            OnRecycle.SafeCall();

            return true;
        }
    }
}
