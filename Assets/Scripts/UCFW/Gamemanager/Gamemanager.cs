using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace UCFW
{
    public class Gamemanager : Singleton<Gamemanager>
    {
        // Members
        [HideInInspector]
        public GamestateDesc[] gamestateDescs = null;

        [HideInInspector]
        public SerializableType initialGamestateType = null;


        // Default index does not work for now
        public SceneIdentifier loadingScene = "Loading";

        private PlayerPrefBool _gameFirstInit = new PlayerPrefBool("GameFirstInit", false);

        private bool _isPaused = false;

        private Gamestate[] _gamestates = null;
        private Gamestate _current = null;
        private bool _gamestateIsReady = false;

        private List<Updateable> _updateables = new List<Updateable>();
        private Stack<Updateable> _updateablesToUnregister = new Stack<Updateable>();

        public Gamestate current
        {
            get { return _current; }
        }

        public bool isPaused
        {
            get { return _isPaused; }
        }

        // Events
        public event Action<bool> OnPauseChange;
        public event Action<Type> OnInitGamestate;

        private event Action _OnInitGamestateParamless;
        Dictionary<Type, Action> _onInitGamestateTypeDictionary = new Dictionary<Type, Action>();

        protected override void OnAwake()
        {
            if (gamestateDescs == null)
            {
                Application.Quit();
            }
            _gamestates = new Gamestate[gamestateDescs.Length];

            // Check if this is the first start of the game on a new device
            _gameFirstInit.Load(true);

#if !UNITY_EDITOR
            if(_gameFirstInit)
            {
                initialGamestateType = typeof(IngameGamestate);
            }
            else
            {
                initialGamestateType = typeof(MenuGamestate);
            }
#endif

            // Search for initial gamestate
            for (int i = 0; i < _gamestates.Length; ++i)
            {
                _gamestates[i] = gamestateDescs[i].Create();
                if (_gamestates[i].GetType() == initialGamestateType.type)
                {
                    _current = _gamestates[i];
                }
            }

            // Init initial gamestate
            LoadCurrentGamestateScene();

            OnInitGamestate += (t) => _gameFirstInit.Save(false);
        }

        private void InitCurrentGamestate()
        {
            if (_current != null && (_gamestateIsReady = _current.Init()))
            {
                SceneManager.UnloadScene(loadingScene.levelName);

                // Right scene is ready and gamestate is inited call all events

                OnInitGamestate.SafeCall(_current.GetType());
                _OnInitGamestateParamless.SafeCall();

                Action a;
                if (_onInitGamestateTypeDictionary.TryGetValue(_current.GetType(), out a))
                {
                    a.SafeCall();
                }
            }
        }

        private void LoadCurrentGamestateScene()
        {
            _gamestateIsReady = false;
            if (!_current.sceneRequired || (_current.sceneName == SceneManager.GetActiveScene().name && !_current.reloadRequired))
            {
                // Right scene is already loaded
                InitCurrentGamestate();
            }
            else
            {
                // Scene has to be loaded
                if (_current.preload)
                {
                    loadingScene.LoadAsync();
                }
                else
                {
                    SceneManager.LoadSceneAsync(_current.sceneName);
                }
            }

        }

        private void Update()
        {
            // Remove unregistered updateables from list (done like this to prevent ArgumentExceptions when updateables are removed during an update)
            while (_updateablesToUnregister.Count > 0)
            {
                Updateable u = _updateablesToUnregister.Pop();
                if (u != null)
                {
                    _updateables.Remove(u);
                }
            }

            if (_current != null && _gamestateIsReady)
            {
                _current.OnUpdate();

                if (_isPaused)
                {
                    for (int i = 0; i < _updateables.Count; ++i)
                    {
                        _updateables[i].OnUpdate();
                    }
                }
                else
                {
                    _current.Loop();

                    for (int i = 0; i < _updateables.Count; ++i)
                    {
                        _updateables[i].Loop();
                        _updateables[i].OnUpdate();
                    }

                }
            }
        }

        private void OnLevelWasLoaded(int levelIndex)
        {
            if (this != instance)
            {
                return;
            }

            if (_current != null)
            {
                if (SceneManager.GetActiveScene().name == loadingScene)
                {
                    // In preload scene, start loading the right scene
                    SceneManager.LoadSceneAsync(_current.sceneName);
                }
                else if (SceneManager.GetActiveScene().name == _current.sceneName)
                {
                    // Required Scene for gamestate is loaded, init Gamestate
                    InitCurrentGamestate();
                }
                else
                {
                    // Another scene was loaded. Warning!?
                }
            }
        }

        public void RegisterUpdateable(Updateable u)
        {
            if (!_updateables.Contains(u))
            {
                _updateables.Add(u);
            }
        }

        public void UnregisterUpdateable(Updateable u)
        {
            _updateablesToUnregister.Push(u);
        }


        public void SetPauseState(bool paused)
        {
            if (paused != _isPaused)
            {
                _isPaused = paused;
                OnPauseChange.SafeCall(_isPaused);
            }
        }

        public void Pause()
        {
            SetPauseState(true);
        }

        public void Continue()
        {
            SetPauseState(false);
        }

        public void ChangeGamestate<T>() where T : Gamestate
        {
            ChangeGamestate(typeof(T));
        }

        public void ChangeGamestate(Type gamestateType)
        {
            if (typeof(Gamestate).IsAssignableFrom(gamestateType))
            {
                if (_current != null)
                {
                    _current.DeInit();
                }

                for (int i = 0; i < _gamestates.Length; ++i)
                {
                    if (gamestateType == _gamestates[i].GetType())
                    {
                        _current = _gamestates[i];
                        break;
                    }
                }
                LoadCurrentGamestateScene();
            }
        }

        public new void RegisterForOnChangeGamestate(Action handler)
        {
            _OnInitGamestateParamless += handler;
        }

        public new void RegisterForOnChangeGamestate(Action<Type> handler)
        {
            OnInitGamestate += handler;

        }

        public new void RegisterForOnChangeGamestate<T>(Action handler) where T : Gamestate
        {
            Action a;
            if (_onInitGamestateTypeDictionary.TryGetValue(typeof(T), out a))
            {
                a += handler;
                _onInitGamestateTypeDictionary[typeof(T)] = a;
            }
            else
            {
                _onInitGamestateTypeDictionary.Add(typeof(T), handler);
            }
        }

        public new void UnregisterFromOnChangeGamestate(Action handler)
        {
            _OnInitGamestateParamless -= handler;
        }

        public new void UnregisterFromOnChangeGamestate(Action<Type> handler)
        {
            OnInitGamestate -= handler;
        }

        public new void UnregisterFromOnChangeGamestate<T>(Action handler) where T : Gamestate
        {
            if (_onInitGamestateTypeDictionary.ContainsKey(typeof(T)))
            {
                Action a = _onInitGamestateTypeDictionary[typeof(T)];

                a -= handler;

                if (a == null || a.GetInvocationList().Length == 0)
                {
                    _onInitGamestateTypeDictionary.Remove(typeof(T));
                }
                else
                {
                    _onInitGamestateTypeDictionary[typeof(T)] = a;
                }
            }
        }
    }
}
