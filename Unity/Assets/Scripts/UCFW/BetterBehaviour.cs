using UnityEngine;
using System;

namespace UCFW
{
    /// <summary>
    /// An expanded Version of the MonoBehaviour
    /// </summary>
    public abstract class BetterBehaviour : MonoBehaviour
    {
        private Transform _transform = null;
        private Rigidbody _rigidbody = null;
        private Rigidbody2D _rigidbody2D = null;

        public new Transform transform { get { return _transform; } }
        public new Rigidbody rigidbody { get { return _rigidbody; } }
        public new Rigidbody2D rigidbody2D { get { return _rigidbody2D; } }

        private void Awake()
        {
            InitBetterBehaviour();
            OnAwake();
        }

        private void OnDestroy()
        {
            OnDestroyed();
            DeInitBetterBehaviour();
        }

        /// <summary>
        /// Initializes the BetterBehaviour 
        /// </summary>
        protected void InitBetterBehaviour()
        {
            _transform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Deinitializes the BetterBeahaviour
        /// </summary>
        protected void DeInitBetterBehaviour()
        {
            // Empty for the moment but added for the sake of consistency
        }

        /// <summary>
        /// Override this method instead of using the Awake message
        /// </summary>
        protected virtual void OnAwake() { }

        /// <summary>
        /// Override this method instead of using the OnDestroy message
        /// </summary>
        protected virtual void OnDestroyed() { }


        public void RegisterForOnChangeGamestate(Action handler)
        {
            Gamemanager.instance.RegisterForOnChangeGamestate(handler);
        }

        public void RegisterForOnChangeGamestate(Action<Type> handler)
        {
            Gamemanager.instance.RegisterForOnChangeGamestate(handler);
        }

        public void RegisterForOnChangeGamestate<T>(Action handler) where T : Gamestate
        {
            Gamemanager.instance.RegisterForOnChangeGamestate<T>(handler);
        }

        public void UnregisterFromOnChangeGamestate(Action handler)
        {
            Gamemanager.instance.UnregisterFromOnChangeGamestate(handler);
        }

        public void UnregisterFromOnChangeGamestate(Action<Type> handler)
        {
            Gamemanager.instance.UnregisterFromOnChangeGamestate(handler);
        }

        public void UnregisterFromOnChangeGamestate<T>(Action handler) where T : Gamestate
        {
            Gamemanager.instance.UnregisterFromOnChangeGamestate<T>(handler);
        }
    }
}
