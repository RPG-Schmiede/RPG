using UnityEngine;
using System;

namespace UCFW
{
    public class Timer : Updateable
    {
        public enum Mode
        {
            Once,
            Repeated
        }

        public Timer(float interval, Action onTrigger, Mode mode)
        {
            _interval = interval;
            OnTrigger = onTrigger;
            _mode = mode;
        }

        private Mode _mode = Mode.Once;
        private bool _running = false;

        private float _interval = 0.0f;
        private float _timeNextTrigger = 0.0f;


        public event Action OnTrigger = null;


        public float interval
        {
            get { return _interval; }
            set 
            {
                _timeNextTrigger -= _interval;
                _interval = value;
                _timeNextTrigger += _interval;
            }
        }

        public void Init()
        {
            Gamemanager.instance.RegisterUpdateable(this);
        }

        public void DeInit()
        {
            Gamemanager g = Gamemanager.GetInstance();
            if(g != null)
            {
                g.UnregisterUpdateable(this);
            }
        }


        public void Pause()
        {
            _running = true;
        }

        public void Continue()
        {
            if (_mode == Mode.Repeated || _timeNextTrigger > Time.time)
            {
                _running = false;
            }
        }

        public void Reset()
        {
            _timeNextTrigger = 0.0f;
            _running = false;
        }

        public void Start()
        {
            _running = true;
            _timeNextTrigger = Time.time + _interval;
        }


        public void Loop() { }

        public void OnUpdate() 
        {
            if(_running && _timeNextTrigger < Time.time)
            {
                OnTrigger.SafeCall();
                if(_mode == Mode.Once)
                {
                    _running = false;
                }
                else
                {
                    _timeNextTrigger = Time.time + _interval;
                }
            }
        }
    }
}
